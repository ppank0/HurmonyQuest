import { Box} from "@mui/material"
import { routerConfig } from "../../app/router/routerConfig";
import { Link } from "react-router";

interface NavBarProps{
    isAuth: boolean,
    role: string
}
export function NavBar({isAuth, role} :NavBarProps){
    const routesToFilter = routerConfig[0].children;
    const visibleRoutes = routesToFilter?.filter(route => {
        if(!route.name) return false;
        if((route.role == role) && isAuth) return true;
        return !route.role
    })
    
    return(
        <Box sx={{display: "flex", gap: 2}}>
            {visibleRoutes?.map((x) => (
                <Link key={x.name||x.path} to={x.path || '/'} style={{textDecoration: "none", color: "#ffff"}}>{x.name}</Link>
            ))}
        </Box>
    )
}