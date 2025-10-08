import {useAuth0 } from "@auth0/auth0-react";
import { $authHost } from ".";
import { useEffect, useState } from "react";

const { user, getAccessTokenSilently } = useAuth0();

const token = getAccessTokenSilently();

// useUserStore.getState().setUser({
//   id: "", // пока нет, получим от бэка
//   authId: user?.sub,
//   name: user?.name,
//   email: user?.email,
//   userPictureUrl: user?.picture,
//   token,
// });

$authHost.post("/users", {
    authId: user?.sub,
    name: user?.name,
    email: user?.email,
    userPictureUrl: user?.picture,
});

//   useEffect(() => {
//     (async () => {
//       try {
//         const token = await getAccessTokenSilently({
//           authorizationParams: {
//             audience: 'https://api.example.com/', 
//           }
//         });
//         const response = await fetch('https://api.example.com/posts', {
//           headers: {
//             Authorization: `Bearer ${token}`,
//           },
//         });
//         setPosts(await response.json());
//       } catch (e) {
//         console.error(e);
//       }
//     })();
//   }, [getAccessTokenSilently]);



// export const postUser = async (userData: UserService): Promise<User | null> => {
//     try {
//         const { data } = await $host.post<User>(`users`, userData);
//         return data;
//     } catch (error) {
//         return null;
//     }
// };


// export const getUser = async (authId: string | undefined): Promise<UserService> => {
//     const { data } = await $host.get<UserService>(`user/auth0/${authId}`);
//     const token = await getAccessTokenSilently();
//     console.log("ACCESS TOKEN:", token);
//     return data;
// };