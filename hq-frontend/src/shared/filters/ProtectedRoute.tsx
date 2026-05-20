import type { ReactNode } from "react";
import { useAuth } from "../contexts/AuthContext";
import { Navigate} from "react-router";

export function ProtectedRoute({children, allowedRole} : {children: ReactNode, allowedRole:string}){
    const{isAuthenticated, role} = useAuth();

    if(!isAuthenticated || role !== allowedRole){
        return <Navigate to={"/forbidden"}/>
    }
    return(
        <>{children}</>
    )
}