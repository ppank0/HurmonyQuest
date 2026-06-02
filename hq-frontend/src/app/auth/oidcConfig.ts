import { UserManager, WebStorageStateStore } from "oidc-client-ts";

export const userManager = new UserManager({
    authority: import.meta.env.VITE_AUTH_SERVICE_URL,
    client_id: import.meta.env.VITE_AUTH_CLIENT_ID,
    response_type: 'code',
    redirect_uri: import.meta.env.VITE_AUTH_REDIRECT_URL,
    post_logout_redirect_uri: import.meta.env.VITE_AUTH_LOGOUT_REDIRECT_URL,
    scope: 'openid profile roles offline_access',
    userStore: new WebStorageStateStore({store: localStorage}),
    automaticSilentRenew: true
});