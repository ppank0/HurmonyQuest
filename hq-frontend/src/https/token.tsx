import React, { useEffect, useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";

export default function TokenViewer() {
  const { isAuthenticated, getAccessTokenSilently, loginWithRedirect } = useAuth0();
  const [token, setToken] = useState<string | null>(null);

  useEffect(() => {
    if (!isAuthenticated) return;

    (async () => {
      try {
        const accessToken = await getAccessTokenSilently({
          authorizationParams: {
            audience: process.env.REACT_APP_AUTH0_AUDIENCE,
          },
        });
        setToken(accessToken);
        console.log("Access Token:", accessToken); // токен в консоли
      } catch (e) {
        console.error("Ошибка получения токена:", e);
      }
    })();
  }, [isAuthenticated, getAccessTokenSilently]);

  if (!isAuthenticated) {
    return <button onClick={() => loginWithRedirect()}>Войти</button>;
  }

  return (
    <div>
      <h2>Токен успешно получен!</h2>
      <textarea
        style={{ width: "100%", height: "200px" }}
        readOnly
        value={token || "Токен ещё не загружен..."}
      />
    </div>
  );
}
