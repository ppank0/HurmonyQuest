import { Home } from "../../pages/Home"
import { About } from "../../pages/About"
import { NotFound } from "../../pages/NotFound"
import { AdminPanel } from "../../pages/AdminPanel"
import { SigninOidc } from "../../pages/SigninOidc"
import { SignoutOidc } from "../../pages/SignoutOidc"
import { MainLayout } from "../../layouts/MainLayout"
import { NominationManager } from "../../pages/Managers/NominationManager"
import { ProtectedRoute } from "../../shared/filters/ProtectedRoute"
import { paths } from "../../shared/constants/routes"

export const routerConfig = [
    {
        path: paths.Home,
        element: <MainLayout/>,
        children: [
            {
                index: true,
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
                element: 
                    <ProtectedRoute allowedRole="admin">
                        <AdminPanel/>
                    </ProtectedRoute>,
                role: 'admin',
                name: "Admin",
                children: [
                    {
                        path: paths.Nomination,
                        element: <NominationManager/>
                    }
                ]
            },
            {
                path: paths.Forbidden,
                element: <NotFound codeStatus={403} title={"Error - Forbidden"}/>
            },
            {
                path: paths.NotFound,
                element: <NotFound codeStatus={null} title={null}/>
            }
        ]
    },
    {
        path: "/signin-oidc",
        element: <SigninOidc/>
    },
    {
        path: "/signout-callback-oidc",
        element: <SignoutOidc/>,
        name: "signOut"
    }
]
