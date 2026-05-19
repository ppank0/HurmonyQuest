import { Typography } from "@mui/material";
import { useEffect } from "react";
import { useNavigate } from "react-router";
import { userManager } from "../app/auth/oidcConfig";

export function SigninOidc(){
    const navigate = useNavigate();

    useEffect(() => {
            userManager.signinRedirectCallback()
            .then(() => navigate("/"))
            .catch((error:Error) => {
                console.log("Sign in error: " + error.message)
                navigate("/")
            })
        
    }, [navigate])
    return (
        <Typography variant="subtitle1">Processing login...</Typography>
    )
}