import { useAuth0 } from '@auth0/auth0-react';
import { Container } from '@mui/material';

export function Profile() {
  const { user } = useAuth0();

  return (
        <Container>
            <div>Hello {user?.name}</div>
            <div>{user?.birthdate}</div>
            <div>{user?.nickname}</div>
            <div>{user?.email}</div>
            <div><img src={user?.picture}></img></div>
        </Container>
  )
}

export default Profile;
