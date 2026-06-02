import {type ReactNode} from "react";
import { useEffect } from "react";
import { userManager } from "./oidcConfig";
import type { User } from "oidc-client-ts";
import { useAuthStore } from "../../shared/stores/AuthStore";
import { AuthServer } from "../../shared/api/AuthServer/authServer";

export const AuthInitializer = ({children} : {children: ReactNode})=>{
const resetState = useAuthStore((state) => state.resetState)

async function getInfo(user: User){
        if(user.profile?.email && useAuthStore.getState().role){
            return user;
        }
        try {
            const userData = await AuthServer.getUserInfo();
            if (userData?.role) {
                useAuthStore.setState({role: userData.role})
            }
            user.profile.email = userData.email
            await userManager.storeUser(user);
            return user;
        } catch (error) {
            console.error("Failed to get user role/email:", error);
        }
    }

useEffect(() => {
        const loadUser = async () => {
            useAuthStore.setState({isLoading: true})
            const response = await userManager.getUser();
            
            if(response){
                const userData = await getInfo(response);
                useAuthStore.setState({user: userData!, token: userData!.access_token, isAuthenticated: true})
            }
        };
        useAuthStore.setState({isLoading: false})
        loadUser();
        
    }, [])

    useEffect(() => {
        const onUserLoaded = async (loadedUser: User) => { 
            if(loadedUser){
               const response = await getInfo(loadedUser);
               useAuthStore.setState({user: response!, token: loadedUser.access_token, isAuthenticated: true})
            }
        };
        const onUserUnloaded = () => {
            resetState();
        };
        userManager.events.addUserLoaded(onUserLoaded);
        userManager.events.addUserUnloaded(onUserUnloaded);

        return () => {
            userManager.events.removeUserLoaded(onUserLoaded);
            userManager.events.removeUserUnloaded(onUserUnloaded);
        };
    }, []);

    return(
        <>{children}</>
    )
}