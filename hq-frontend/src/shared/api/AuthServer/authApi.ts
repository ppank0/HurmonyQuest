import axios from "axios";
import { userManager } from "../../../app/auth/oidcConfig";

const authApi = axios.create({
    baseURL: import.meta.env.VITE_AUTH_SERVICE_URL
})

authApi.interceptors.request.use((config) => {
    const user = JSON.parse(localStorage.getItem(`oidc.user:${userManager.settings.authority}:${userManager.settings.client_id}`)|| "{}");
    const token = user?.access_token;
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
    return config;
}, (error:Error) => {
    console.log("FROM INTERCEPTOR: "+ error.message)
    return Promise.reject(error);
})

export default authApi;