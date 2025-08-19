import { Container, Typography, Divider, Box, Collapse } from '@mui/material'
import piano from "../assets/piano.png"
import frame from '../assets/patterned_frame.png'

export const HomePage = () => {
  return (
    <Container sx={{display: 'flex', flexDirection:'column', justifyContent:'end', alignItems:'center', m:'12vh auto'}}>
      <Box sx={{m:"6rem 0rem"}}>
        <Typography variant='h1' align='left' mb='1rem'>“Hurmony Quest” International Music Competition</Typography> 
        <Divider />
        <Typography variant='subtitle1' align='left'>Delicate Music, Strict Discipline, Eternal Inspiration.</Typography>
      </Box>
      <Box sx={{display: 'flex', gap: '10vw',  mt:'25vh'}}>

        <Box sx={{textAlign: 'center', rowGap: '2'}}>
          <img  src={frame}/>
          <Typography width={'40vw'} >“Hurmony Quest” is an annual competition of classical performance,
              bringing together virtuosos, sensitive artists, and true connoisseurs.
               Our mission is to maintain the highest level of artistry,
               breathe new life into masterpieces of world classical music,
               and discover emerging talents.
          </Typography>
          <img src={frame} style={{ transform: "rotate(180deg)"}} />
        </Box>

        <Collapse in={true} timeout={10100} sx={{ml:'auto'}}>
            <img  src={piano}/>
        </Collapse>
      </Box>
    </Container>
  )
}
