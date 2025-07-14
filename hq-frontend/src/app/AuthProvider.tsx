import { Auth0Provider } from '@auth0/auth0-react';

const domainValue = process.env.REACT_APP_AUTH0_DOMAIN!;
const clientIdValue = process.env.REACT_APP_AUTH0_CLIENT_ID!;

export const AuthProvider = ({ children }: { children: React.ReactNode }) => (
  <Auth0Provider
    domain={domainValue}
    clientId={clientIdValue}
    authorizationParams={{
      redirect_uri: window.location.origin,
    }}
  >
    {children}
  </Auth0Provider>
);
