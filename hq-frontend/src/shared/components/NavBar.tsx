import { Button, Typography, Box, Link } from "@mui/material"
import LoginButton from "../../features/auth/components/LoginButton"
import { useNavigate } from 'react-router-dom';
import LogoutButton from "../../features/auth/components/LogoutButton";
import { useAuth0 } from "@auth0/auth0-react";
import { ROUTES } from "../config/routes";

export default function NavBar(){
    const navigate = useNavigate();
    const { isAuthenticated, isLoading } = useAuth0();

    if (isLoading) return null;
    // const menuItems = [
    //     { text: 'Catalog', route: CATALOG_ROUTE },
    //     { text: 'Offers', route: OFFERS_ROUTE },
    //     { text: 'About us', route: ABOUT_ROUTE },
    // ];
    return(
        <Box sx={{ display: 'flex', gap: 5, alignItems:"center"}}>
            <Link href={ROUTES.ABOUT} variant="body1" underline="none">about us</Link>
            <Link href="#" variant="body1" underline="none">rules</Link>
            
            {isAuthenticated ? <>
                <Link href={ROUTES.PROFILE} variant="body1" underline="none">my profile</Link>
                <LogoutButton />
            </> : <LoginButton />}
        </Box>
    )
}
 