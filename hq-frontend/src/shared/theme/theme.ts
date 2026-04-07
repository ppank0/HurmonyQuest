import { createTheme } from "@mui/material";

export const theme = createTheme({
    palette: {
        primary: { main: '#3A3A3A', dark: '#000000' , light: '#a7a7a7', contrastText: '#ffffff' },
        secondary: { main: '#780B0B', dark: '#ffffff', light: '#ffffff' },
    },
    typography: {
        fontFamily: '"Playfair Display", serif',
        h1: {
            fontSize: '5rem',
            fontWeight: 'bold', 
        },
        subtitle1: {
            fontSize: '2rem',
            fontStyle: "italic",
        },
        body1:{
            fontFamily: 'Quattrocento',
            fontSize: '1.2rem'
        },
    },
    components: {
        MuiButton:{
            variants:[{
                props: { variant: 'outlined'},
                style:{
                    borderRadius: '1.8rem',
                    color: '#ffff',
                    backgroundColor: "#3A3A3A",
                    padding: '0.5rem 2rem',
                    '&:hover':{
                        backgroundColor: "#780B0B"
                    }
                },
            },]
        }
    }
});

