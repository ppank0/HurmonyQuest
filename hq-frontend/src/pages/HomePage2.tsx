import { Container, Typography, Divider, Box, Collapse } from '@mui/material'
import girls from '../assets/girls.png'
import frame from '../assets/patterned_frame.png'
import main_img from '../assets/скрипка.png'

export const HomePage2 = () => {
  return (
    <Container sx={{display: 'flex', flexDirection:'column', justifyContent:'end', alignItems:'center'}}>

      <Box sx={{
        position: "relative",
        height: "90vh",
        width: '99vw',
        backgroundImage: `url(${main_img})`,
        backgroundSize: "cover",
        backgroundPosition: "center",
        display: 'flex',
        alignItems: 'center'
       }}>
        <Box sx={{
          color: '#ffff',
          display: 'flex',
          flexDirection: 'column',
          ml: "5vw"
          }}>
          <Typography variant='h1' align='left' mb='1rem' 
          sx={{WebkitTextStroke: '.01px #5E5C5C', fontSize: "5rem"}}>
              “Hurmony Quest” International<br/>Music Competition
          </Typography> 
          <Divider sx={{color: 'secondary.dark', border: 'solid', width: '70vw'}}/>
          <Typography variant='subtitle1' align='left' sx={{color: "#a2a2a2"}}>Delicate Music, Strict Discipline, Eternal Inspiration.</Typography>
        </Box>
      </Box>

      <Box sx={{display: 'flex', gap: '15vw',  mt:'25vh', mb: "5vh"}}>
        <Box sx={{textAlign: 'center', ml: '2vw'}}>
          <img  src={frame}/>
          <Typography width={'40vw'} sx={{color: 'secondary.dark'}}>“Hurmony Quest” is an annual competition of classical performance,
              bringing together virtuosos, sensitive artists, and true connoisseurs.
               Our mission is to maintain the highest level of artistry,
               breathe new life into masterpieces of world classical music,
               and discover emerging talents.
          </Typography>
          <img src={frame} style={{ transform: "rotate(180deg)"}} />
        </Box>

        <Box sx={{borderLeft: '0.8vw solid', pl: '5vw', borderColor: 'secondary.dark'}}>
            <img  src={girls}/>
        </Box>
      </Box>
      
    </Container>
  )
}