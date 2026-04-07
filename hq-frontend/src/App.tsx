import { AppRouter} from './app/router/AppRouter';
import { Header } from './layouts/Header/Header';
import { Footer } from './layouts/Footer/Footer';
import { ThemeProvider } from '@mui/material/styles';
import { theme } from './shared/theme/theme';
import CssBaseline from '@mui/material/CssBaseline';
import { Box } from '@mui/material';

function App() {
  return (
    <>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <Box sx={{display: 'flex', flexDirection: 'column', minHeight: '100vh'}}>
          <Header/>
            <Box sx={{flexGrow: 1}}>
              <AppRouter/>
            </Box>
          <Footer/>
        </Box>
      </ThemeProvider>
    </>
  )
}

export default App
