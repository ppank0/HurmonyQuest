import { AppRouter} from './app/router/AppRouter';
import { ThemeProvider } from '@mui/material/styles';
import { theme } from './shared/theme/theme';
import CssBaseline from '@mui/material/CssBaseline';
import { AuthProvider } from './shared/contexts/AuthContext';

function App() {
  return (
    <>
      <ThemeProvider theme={theme}>
        <AuthProvider>
        <CssBaseline />
          <AppRouter/>
        </AuthProvider>
      </ThemeProvider>
    </>
  )
}

export default App
