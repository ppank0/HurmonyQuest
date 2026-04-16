import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Tooltip, IconButton, Typography} from "@mui/material"
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import type { CustomTableProps } from "./interfaces/tableInterfaces";

export function CustomTable<T>({data, column, renderActions, onEdit, onDelete}: CustomTableProps<T>){
    return(
        <TableContainer sx={{width: "70vw", borderColor: "primary.dark", borderStyle: "dashed"}}>
            <Table>
                <TableHead>
                    <TableRow>
                        {column.map((c) => (
                            <TableCell align="center" key={c.key as string}>
                                <Typography variant="body1">{c.header}</Typography>
                            </TableCell>
                        ))}
                        {renderActions && <TableCell align="center">
                            <Typography variant="body1">Actions</Typography>
                        </TableCell>}
                    </TableRow>
                </TableHead>
                <TableBody>
                    {data.map((item) => (
                        <TableRow>
                            {column.map((col)=> (
                                <TableCell align="center" key={col.key as string}> 
                                    <Typography sx={{fontSize: "1rem"}}>{String(item[col.key])}</Typography>
                                    
                                </TableCell>     
                            ))}
                            {renderActions && <TableCell align="center" key={Date.now.toString()}>
                                    <Tooltip title="Delete">
                                        <IconButton onClick={() => onDelete?.(item)}>
                                            <DeleteIcon />
                                        </IconButton>
                                    </Tooltip>
                                    <Tooltip title="Edit" >
                                        <IconButton onClick={() => onEdit?.(item)}>
                                            <EditIcon/>
                                        </IconButton>
                                    </Tooltip>
                                </TableCell>
                            }
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    )
}