import { paths } from "../../shared/constants/routes"
import { Home } from "../../pages/Home"
import { About } from "../../pages/About"
import { NotFound } from "../../pages/NotFound"
import { AdminPanel } from "../../pages/AdminPanel"

export const routerConfig =  [
    {
        path: paths.Home,
        element: <Home/>,
        name: "Home"
    },
    {
        path: paths.About,
        element: <About/>,
        name: "About"
    },
    {
        path: paths.AdminPanel,
        element: <AdminPanel/>,
        name: "Admin Panel"
    },
    {
        path: "*",
        element: <NotFound/>
    }
]
