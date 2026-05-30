import { Box, Button, CircularProgress, Stack, Typography } from "@mui/material"
import { NavBar } from "./NavBar"
import { useAuthStore } from "../../shared/stores/AuthStore";

export const Header = () => {
    const login = useAuthStore((state) => state.login)
    const logout = useAuthStore((state) => state.logout)
    const isAuthenticated = useAuthStore((state) => state.isAuthenticated)
    const isLoading = useAuthStore((state) => state.isLoading)
    const role = useAuthStore((state) => state.role)
    
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
