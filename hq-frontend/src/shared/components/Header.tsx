import { AppBar, Toolbar, Typography, Button, Box, Link } from '@mui/material';
import NavBar from './NavBar';

export default function Header() {
  return (
       <AppBar position="static" color="transparent" elevation={0}>
      <Toolbar sx={{ justifyContent: 'space-between', m:  ".5rem 3rem"}}>
        <Link href='/' variant='body1' sx={{fontWeight:"bold", fontSize:'1.5rem', color: 'black'}}>
          HurmonyQuest
        </Link>
        <NavBar/>
      </Toolbar>
    </AppBar>
  );
}
