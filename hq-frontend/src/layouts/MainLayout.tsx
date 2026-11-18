import { Box } from "@mui/material"
import { Header } from "./Header/Header"
import { Footer } from "./Footer/Footer"
import { Outlet } from "react-router"

export const MainLayout = () => {
    return(
        <Box sx={{display: 'flex', flexDirection: 'column', minHeight: '100vh'}}>
            <Header/>
                <Box sx={{flexGrow: 1}}>
                    <Outlet/>
                </Box>
            <Footer/>
        </Box>
    )
}