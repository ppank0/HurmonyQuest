import { Typography } from "@mui/material";
import { useEffect } from "react";
import { useNavigate } from "react-router";
import { userManager } from "../app/auth/oidcConfig";

export function SignoutOidc(){
    const navigate = useNavigate();

    useEffect(() => {
            userManager.signoutRedirectCallback()
            .then(() => navigate("/"))
            .catch((error:Error) => {
                console.log("Sign out error: " + error.message)
                navigate("/")
            })
        
    }, [navigate])
    return (
        <Typography variant="subtitle1">Processing logout...</Typography>
    )
}