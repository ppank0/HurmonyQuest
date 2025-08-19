import {useState, useEffect} from  'react'
import { JuryItem } from "../../../types/types";
import {$host} from '../../../https/index'
import {CircularProgress, Table, TableBody, TableCell, TableHead, TableContainer, TableRow, Paper, Box} from '@mui/material'

export const GetAllJuries = () => {

    const [juryItems, setJuryItems] = useState<JuryItem[]>([]);

    useEffect(() => {
        console.log("useEffect запущен");
        $host.get<JuryItem[]>(`/api/juries`)
            .then((results) => {
                setJuryItems(results.data); 
            })
            .catch((err) => {
                console.error("Ошибка при запросе жюри:", err);
            })
    }, [])

    return (
        <Box sx={{m:'15vh 15vw'}}>
            {juryItems.length > 0 
                    ? (<TableContainer component={Paper} elevation={3} sx={{ maxHeight: '50vh'}}>
                        <Table stickyHeader>
                            <TableHead>
                                <TableRow>
                                    <TableCell>№</TableCell>
                                    <TableCell>Name</TableCell>
                                    <TableCell>Surname</TableCell>
                                    <TableCell>Birthday</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {juryItems.map((item, index) => (
                                    <TableRow key={index}>
                                    <TableCell>{index + 1}</TableCell>
                                    <TableCell>{item.name}</TableCell>
                                    <TableCell>{item.surname}</TableCell>
                                    <TableCell>{item.birthday}</TableCell>
                                    </TableRow>))}
                            </TableBody>
                        </Table> 
                    </TableContainer>)
                    : <CircularProgress sx={{mt: '25vh'}} />
                }
        </Box>
    ) 
}