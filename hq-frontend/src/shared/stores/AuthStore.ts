import { User } from "oidc-client-ts";
import { userManager } from "../../app/auth/oidcConfig";
import { create } from "zustand";

const InitialState = {
    user: null,
    token: null,
    role: null,
    isAuthenticated: false,
    isLoading: true
};

interface AuthProps{
    user: User| null,
    token: string | null,
    role: string | null,
    isAuthenticated: boolean,
    isLoading: boolean,
    login: () => void,
    logout: () => void,
    resetState: () => void,
}

export const useAuthStore = create<AuthProps>((set) => ({
    ...InitialState,
    login: () => {
        userManager.signinRedirect();
    },
    logout: () => {
        userManager.signoutRedirect();
    },
    resetState: () => {set(InitialState)}
}))
