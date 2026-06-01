import { Typography } from "@mui/material";
import { useEffect } from "react";
import { useNavigate } from "react-router";
import { userManager } from "../app/auth/oidcConfig";

export function SignoutOidc(){
    const navigate = useNavigate();

    useEffect(() => {
        const handkeSighOutOidc = async() => {
            try{
                await userManager.signoutRedirectCallback()
                navigate("/")
            }
            catch(error: any){
                console.log("Sign out error: " + error.message)
                navigate("/")
            }
        }
        handkeSighOutOidc()
    }, [navigate])
    return (
        <Typography variant="subtitle1">Processing logout...</Typography>
    )
}