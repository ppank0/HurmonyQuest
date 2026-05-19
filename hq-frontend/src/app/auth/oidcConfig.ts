import { UserManager, WebStorageStateStore } from "oidc-client-ts";

export const userManager = new UserManager({
    authority: 'https://localhost:5001',
    client_id: 'react_app',
    response_type: 'code',
    redirect_uri: 'http://localhost:5173/signin-oidc',
    post_logout_redirect_uri: 'http://localhost:5173/signout-callback-oidc',
    scope: 'openid profile roles offline_access roles',
    userStore: new WebStorageStateStore({store: localStorage}),
    automaticSilentRenew: true
});