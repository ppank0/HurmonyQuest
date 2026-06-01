import { Typography } from "@mui/material";
import { useEffect } from "react";
import { useNavigate } from "react-router";
import { userManager } from "../app/auth/oidcConfig";

export function SigninOidc(){
    const navigate = useNavigate();

    useEffect(() => {
        const handleSignInOidc = async() => {
            try{
                await userManager.signinRedirectCallback();
                navigate("/")
            }
            catch(error: any){
                console.log("Sign in error: " + error.message)
                navigate("/")
            }
        }
        handleSignInOidc()
    }, [navigate])
    return (
        <Typography variant="subtitle1">Processing login...</Typography>
    )
}