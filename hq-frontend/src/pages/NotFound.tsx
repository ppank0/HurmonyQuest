import { Box, Typography } from '@mui/material'

export const NotFound = () =>{
    return(
        <Box sx={{display: "flex", minHeight: '80vh', flexDirection: "column", alignItems: "center", justifyContent: "center"}}>
            <Typography variant='h1' color='secondary.main'>404</Typography>
            <Typography variant='subtitle1' color='primary.main'>Error - Page is not found</Typography>
        </Box>
    )
}