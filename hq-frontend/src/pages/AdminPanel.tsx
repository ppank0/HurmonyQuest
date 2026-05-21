import { Box, Tab, Tabs, Typography } from "@mui/material"
import { useState } from "react";
import { NominationManager } from "./Managers/NominationManager";

export const AdminPanel = () =>{
    const [value, setValue] = useState(0);

  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };
    return(
        <Box sx={{display: "flex", flexDirection: "column"}}>
            <Typography sx={{textAlign: "center"}} variant="subtitle1">Admin Panel</Typography>
            <Tabs
                value={value}
                onChange={handleChange}
                variant="scrollable"
                scrollButtons
                allowScrollButtonsMobile
                aria-label="scrollable force tabs example"
            >
                <Tab label="Nominations" />
            </Tabs>
            <Box sx={{display: 'flex', flexDirection: "column", alignItems: "center"}}>
                {value === 0 && <NominationManager/>}
                
            </Box>
        </Box>
    )
}