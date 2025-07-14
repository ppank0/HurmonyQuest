import { Auth0Provider } from '@auth0/auth0-react';
const domainValue = process.env.AUTH0_DOMAIN as string
const clientIdValue = process.env.AUTH0_CLIENT_ID as string
const audienceValue = process.env.AUTH0_AUDIENCE as string

export const AuthProvider = ({ children }: { children: React.ReactNode }) => (
  <Auth0Provider
    domain={domainValue}
    clientId={clientIdValue}
    authorizationParams={{
      redirect_uri: window.location.origin,
      audience: audienceValue,
    }}
  >
    {children}
  </Auth0Provider>
);
