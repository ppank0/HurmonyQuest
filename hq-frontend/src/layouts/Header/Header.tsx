import { Link } from "react-router"
import { Box, Button, Stack } from "@mui/material"
import { routerConfig } from "../../app/router/routerConfig"

export const Header = () => {
    return(
        <Box className="header" sx={{backgroundColor: 'primary.dark', display: 'flex',
             justifyContent: 'space-between', alignItems: 'center', p: '3vh 5vw'}}>
            <Box sx={{color: 'primary.light', fontSize: '1.5rem', fontWeight: 'bold'}}>HQ</Box>
            <Stack className="nav" direction="row" alignItems={"center"} spacing={4} sx={{color: 'primary.contrastText'}}>
                {routerConfig.map((x) => (
                    <Link key={x.name||x.path} to={x.path} style={{textDecoration: "none", color: "#ffff"}}>{x.name}</Link>
                ))}
                <Button variant="outlined">LogIn</Button>
            </Stack>
        </Box>
    )
}
