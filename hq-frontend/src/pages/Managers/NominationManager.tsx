import { useState } from "react";
import { type Column } from "../../components/interfaces/tableInterfaces";
import { CustomTable } from "../../components/CustomTable";
import { type editNomination, type Nomination } from "../../shared/api/contestService/interfaces";
import { Modal, Box, Button, Paper, Typography, IconButton, CircularProgress} from "@mui/material";
import CloseIcon from '@mui/icons-material/Close';
import AddIcon from '@mui/icons-material/Add';
import { NominationForm } from "../../components/nominationForm";
import { ConfirmModal } from "../../components/confirmModal";
import type { Action } from "../../components/interfaces/formInterfaces";
import { useNomination } from "./hooks/useNomination";

export const NominationManager = () =>{

    const column: Column<Nomination>[] = [
        {key: "name", header: "Nomination Name"}
    ];
    const { nominations, isLoading, createNomination, editNomination, deleteNomination } = useNomination();
    const [open, setOpen] = useState(false);
    const [action, setAction] = useState<Action>();
    const [modifyNomination, setModifyNomination] = useState<Nomination>();

    const handleOpen = (action: Action) =>{
        setOpen(true);
        setAction(action);
    }
    const handleClose = () =>{
        setOpen(false);
        setAction(undefined);
        setModifyNomination(undefined);
    }

    function handlePreparetionForEdit(item: Nomination){
        setModifyNomination(item);
        handleOpen("edit")
    }

    const handleEdit = async(item: editNomination) => {
        if(modifyNomination?.id !== undefined){
            editNomination(modifyNomination.id, item);
            handleClose();
        }
    };

    const handleCreate = async (item: editNomination) => {
        createNomination(item);
        handleClose();
    };

    function handlePreparationForDelete(item: Nomination){
        setAction("delete")
        setModifyNomination(item);
    }

    function onConfirm(isConfirmed: boolean){
        if(isConfirmed && modifyNomination?.id !== undefined){
            deleteNomination(modifyNomination.id);
            handleClose();
        }
    }
    
    return (
        <Box sx={{display: "flex", flexDirection: "column", margin: "2vh 4vw", gap: "1rem"}}>
            
            <Button variant="outlined" onClick={() => handleOpen("create")}><AddIcon/> Create New</Button>
            {isLoading ? <CircularProgress aria-label="Loading…" /> :
                <CustomTable<Nomination> data={nominations} column={column} renderActions = {true} onEdit={handlePreparetionForEdit}
                    onDelete={handlePreparationForDelete}/>
            }
            <Modal open={open}  aria-labelledby="modal-title" aria-describedby="modal-description"
                sx={{display: "flex", flexDirection: "column", gap: "3vh", justifyContent: "center", alignItems: "center"}}>
                <Paper elevation={3} sx={{padding: "2rem", width: "60vw"}}>
                    <Box sx={{display: "flex", justifyContent: "space-between", alignItems: "center"}}>
                        {action ==="create" ? <Typography variant="subtitle1" id="modal-title">Create New</Typography> :
                        <Typography variant="subtitle1" id="modal-title">Edit</Typography>}
                        <IconButton onClick={handleClose}>
                            <CloseIcon sx={{color: "primary.dark"}}/>
                        </IconButton>
                    </Box>
                    <Box>
                        <NominationForm data={modifyNomination} action={action} onEdit={handleEdit} onCreate={handleCreate}/>
                    </Box>
                </Paper>
            </Modal>
            {action === "delete" &&
                <ConfirmModal open={true} onClose={handleClose} onConfirm={onConfirm}/>
            }
        </Box>
    )
}