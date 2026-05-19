import { Container, Typography, Divider, Box} from '@mui/material'
import girls from '../assets/girls.png'
import frame from '../assets/patterned_frame.png'
import main_img from '../assets/violin.png'

export const Home = () => {
  return (
    <Container sx={{display: 'flex', flexDirection:'column', justifyContent:'end', alignItems:'center'}}>

      <Box sx={{
        position: "relative",
        height: "90vh",
        width: '100vw',
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
          sx={{WebkitTextStroke: '1px #5E5C5C', textShadow:"-4px 2px 3px #000"}}>
              “Hurmony Quest” International<br/>Music Competition
          </Typography> 
          <Divider sx={{color: 'secondary.main', border: 'solid', width: '70vw'}}/>
          <Typography variant='subtitle1' align='left' sx={{color: "primary.light"}}>Delicate Music, Strict Discipline, Eternal Inspiration.</Typography>
        </Box>
      </Box>

      <Box sx={{display: 'flex', gap: '15vw',  mt:'25vh', mb: "5vh"}}>
        <Box sx={{textAlign: 'center', ml: '2vw'}}>
          <img  src={frame}/>
          <Typography width={'40vw'} variant='body1'>“Hurmony Quest” is an annual competition of classical performance,
              bringing together virtuosos, sensitive artists, and true connoisseurs.
               Our mission is to maintain the highest level of artistry,
               breathe new life into masterpieces of world classical music,
               and discover emerging talents.
          </Typography>
          <img src={frame} style={{ transform: "rotate(180deg)"}} />
        </Box>

        <Box sx={{borderLeft: '0.8vw solid', pl: '5vw', borderColor: 'secondary.main'}}>
            <img  src={girls}/>
        </Box>
      </Box>
      
    </Container>
  )
}