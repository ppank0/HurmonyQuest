import '@fontsource/playfair-display/400.css';
import '@fontsource/quattrocento'
import {colors, createTheme} from '@mui/material'

export const theme = createTheme({
    typography:{
        body1:{
            fontFamily: 'Quattrocento',
            fontSize: '1.2rem',
            textDecoration: 'none',
            color: 'black'
        },
        h1: {
            fontFamily: 'Playfair Display, serif',
            fontWeight: 'bold',
            fontSize: '4rem', 
        },
        subtitle1:{
            fontFamily: 'Playfair Display, serif',
            fontStyle: 'italic',
            fontSize: '2rem',
        },
        button: {
            fontStyle: 'italic',
            fontWeight: 'bold',
            color: 'white'
        },
    },
    palette: {
        primary: {
            light: '#b5b5b5',
            main: '#787878',
            dark: '#000',
            contrastText: '#fff',
        },
        secondary: {
        light: '#556b2f',
        main: '#122d48ff', 
        dark: '#620000ff',
        contrastText: '#fff',
    },
   },
   components: {
        MuiButton: {
            styleOverrides: {
                root: {
                backgroundColor: '#787878',
                color: 'white',
                borderRadius: '20px',
                padding: '.5rem 1.5rem',
                fontStyle: 'italic',
                fontWeight: 'bold',
                '&:hover': {
                    backgroundColor: '#202020',
                    },
                },
            },
        },
        MuiLink:{
            styleOverrides:{
                root:{
                    color: '#787878',
                    '&:hover': {
                        color: '#202020'
                    }
                },
            },
        },
    }
})