import { DataTable } from "../crud/DataTable"
import { NominationItem } from "../../../types/types";
import { Box, Button } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { ROUTES } from "../../../shared/config/routes";
import { getNominations } from "./api";

export const GetAllNominations = () =>{
    const navigate = useNavigate();

    return(
        <Box sx={{display: "flex", flexDirection: "column", alignItems: "center"}}>
            <DataTable<NominationItem>
                fetchData = {getNominations}
                columns={[
                    { header: "Id", field: "id"},
                    { header: "Nomination", field: "name"}
                ]}
            />

            <Button 
                onClick={() => navigate(ROUTES.CREATE_NOMINATION)}
                sx={{width: "100%", borderRadius: '0'}}
            >
                Create Nomination
            </Button>

               
        </Box>
    ) 
}