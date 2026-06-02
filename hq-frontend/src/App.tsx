import { AppRouter} from './app/router/AppRouter';
import { ThemeProvider } from '@mui/material/styles';
import { theme } from './shared/theme/theme';
import CssBaseline from '@mui/material/CssBaseline';
import { AuthInitializer } from './app/auth/AuthInitializer';

function App() {
  return (
    <>
      <ThemeProvider theme={theme}>
        <AuthInitializer>
        <CssBaseline />
          <AppRouter/>
        </AuthInitializer>
      </ThemeProvider>
    </>
  )
}

export default App
