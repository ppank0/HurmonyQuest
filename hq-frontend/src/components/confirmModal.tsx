import { Box, Button, Modal, Typography, Paper, IconButton } from "@mui/material";
import CloseIcon from '@mui/icons-material/Close';

export function ConfirmModal({open, onClose, onConfirm} : {
    open: boolean,
    onClose: () => void,
    onConfirm: (isConfirmed: boolean) => void
}){
    function handleConfirm(isConfirmed: boolean){
        onConfirm(isConfirmed);
        onClose();
    }
    return(
        <Modal open={open}  aria-labelledby="modal-title" aria-describedby="modal-description"
                sx={{display: "flex", flexDirection: "column", gap: "3vh", justifyContent: "center", alignItems: "center"}}>
                <Paper elevation={3} sx={{padding: "2rem", width: "40vw"}}>
                    <Box sx={{display: "flex", justifyContent: "space-between", alignItems: "center", marginBottom:"1rem"}}>
                        <Typography>Are you sure you want to delete?</Typography>
                        <IconButton onClick={onClose}>
                            <CloseIcon sx={{color: "primary.dark"}}/>
                        </IconButton>
                    </Box>
                    <Box>
                        <Box sx={{display: "flex", justifyContent:'space-evenly'}}>
                            <Button onClick={() => handleConfirm(true)} variant="contained" color="success">Yes</Button>
                            <Button onClick={() => handleConfirm(false)} variant="contained" color="error">No</Button>
                        </Box>
                    </Box>
                </Paper>
        </Modal>
    )
}