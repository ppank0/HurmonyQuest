import { Box, Typography, Divider } from "@mui/material";

export const Footer = () => {
    return(
        <Box sx={{backgroundColor: 'primary.dark', mt: '5vh'}}>
        <Box sx={{width:'90vw', m:'auto'}}>
            <Divider sx={{color: 'secondary.main', margin: "auto", border: 'solid', width: '70vw'}}/>
            <Typography variant='body1' color="primary.main" sx={{pb:'1vh', textAlign:'center', p: "2vh"}}>HurmonyQuest 2026</Typography>
        </Box>
        </Box>
    )
}