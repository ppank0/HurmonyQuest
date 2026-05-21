import {Route, Routes} from "react-router";
import {routerConfig} from "./routerConfig";

export const AppRouter = () => {
    return(
        <Routes>
          {routerConfig.map((route) =>
          <Route key={route.path} path={route.path} element={route.element} />
          )}
        </Routes> 
    )
}