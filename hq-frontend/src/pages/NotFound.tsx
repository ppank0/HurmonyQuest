import { Box, Typography } from '@mui/material'

export const NotFound = ({codeStatus, title} : {codeStatus:number|null, title:string|null}) =>{
    return(
        <Box sx={{display: "flex", minHeight: '80vh', flexDirection: "column", alignItems: "center", justifyContent: "center"}}>
            <Typography variant='h1' color='secondary.main'>{codeStatus??404}</Typography>
            <Typography variant='subtitle1' color='primary.main'>{title??<span>Error - Page is not found</span>}</Typography>
        </Box>
    )
}