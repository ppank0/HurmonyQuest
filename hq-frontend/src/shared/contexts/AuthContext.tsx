import { createContext, useContext, useEffect, useState, type ReactNode } from "react"
import { AuthServer } from "../api/AuthServer/authServer";
import { User } from "oidc-client-ts";
import { userManager } from "../../app/auth/oidcConfig";

interface AuthProps{
    user: User| null,
    token: string | null,
    role: string | null,
    isAuthenticated: boolean
    login: () => void,
    logout: () => void,
    isLoading: boolean
}
const AuthContext = createContext<AuthProps | null>(null);

export function AuthProvider({children} : {children: ReactNode}){
    const [user, setUser] = useState<User| null>(null);
    const [role, setRole] = useState<string | null>(null);
    const [token, setToken] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(true)
    const isAuthenticated = !!user;

    useEffect(() => {
        const loadUser = async () => {
            setIsLoading(true);
            const response = await userManager.getUser();
            
            if(response){
                const userData = await getInfo(response);
                setUser(userData!),
                setToken(userData!.access_token)
            }
        };
        setIsLoading(false);
        loadUser();
        
    }, [])

    useEffect(() => {
        const onUserLoaded = async (loadedUser: User) => { 
            if(loadedUser){
               const response = await getInfo(loadedUser);
               setUser(response!); 
               setToken(loadedUser.access_token);
            }
        };
        const onUserUnloaded = () => {
            setUser(null);
            setToken(null);
            setRole(null);
        };
        userManager.events.addUserLoaded(onUserLoaded);
        userManager.events.addUserUnloaded(onUserUnloaded);

        return () => {
            userManager.events.removeUserLoaded(onUserLoaded);
            userManager.events.removeUserUnloaded(onUserUnloaded);
        };
    }, []);

    async function getInfo(user: User){
        if(user.profile?.email && role){
            return user;
        }
        try {
            const userData = await AuthServer.getUserInfo();
            if (userData?.role) {
                setRole(userData.role);
            }
            user.profile.email = userData.email
            await userManager.storeUser(user);
            return user;
        } catch (error) {
            console.error("Failed to get user role/email:", error);
        }
    }

    function login(){
        userManager.signinRedirect();
    }

    function logout(){
        userManager.signoutRedirect();
    }

    return(
            <AuthContext.Provider value={{user, role, token, isAuthenticated, login, isLoading, logout}}>
                {children}
            </AuthContext.Provider>
    )
}

export const useAuth = () => {
    const context = useContext(AuthContext);

    if(!context)
    {
        throw new Error("useAuth must be used within AuthProvider");
    }
    return context;
}