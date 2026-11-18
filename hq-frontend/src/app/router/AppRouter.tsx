import {useRoutes} from "react-router";
import {routerConfig} from "./routerConfig";

export const AppRouter = () => {
    const routes = useRoutes(routerConfig);
    return routes;
}