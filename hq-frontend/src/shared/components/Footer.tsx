import { Typography, Box, Divider} from '@mui/material';

export default function Footer() {
  return (
    <Box sx={{width:'90vw', m:'auto'}}>
        <hr/>
        <Typography variant='body1' color="primary.main" sx={{pb:'1vh', textAlign:'center'}}>HurmonyQuest 2025</Typography>
    </Box>
  );
}
