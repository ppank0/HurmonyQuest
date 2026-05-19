import { Box, Button, CircularProgress, Stack, Typography } from "@mui/material"
import { useAuth } from "../../shared/contexts/AuthContext"
import { NavBar } from "./NavBar"

export const Header = () => {
    const {role, login, logout, isAuthenticated, isLoading} = useAuth();
    return ( 
        <>
            {isLoading ? <Box className="header" sx={{backgroundColor: 'primary.dark', display: 'flex',
                justifyContent: 'space-between', alignItems: 'center', p: '3vh 5vw'}}>
                <Box sx={{color: 'primary.light', fontSize: '1.5rem', fontWeight: 'bold'}}>HQ</Box>
                <Stack className="nav" direction="row" alignItems={"center"} spacing={4} sx={{color: 'primary.contrastText'}}>
                    <NavBar isAuth={isAuthenticated} role={role!}/>
                    <Button variant="outlined" onClick={isAuthenticated? logout : login}>
                        {isAuthenticated? <span>LOGOUT</span> : <span>LOGIN</span>}
                    </Button>
                </Stack>
            </Box> :
            <Typography><CircularProgress/> Loading..</Typography>
            }
        </>
    )
}
