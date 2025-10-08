import { useState, useEffect } from 'react'
import { $host } from '../../../https/index'
import { CircularProgress, Table, TableBody, TableCell, TableHead, TableContainer, TableRow, Paper, Box, Typography } from '@mui/material'

type Column<T> = {
  header: string;   
  field: keyof T;  
};

type DataTableProps<T> = {
  fetchData: () => Promise<T[]>;
  columns: Column<T>[];
};

export function DataTable<T extends object>({ fetchData, columns }: DataTableProps<T>) {
  const [items, setItems] = useState<T[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const load = async () => {
      try {
        const data = await fetchData();
        setItems(data);
      } catch (err) {
        console.error("Ошибка при запросе:", err);
      } finally {
        setLoading(false);
      }
    };
    load();
  }, [fetchData]);

  return (
    <Box sx={{ m: '15vh 15vw' }}>
      {loading ? (
        <CircularProgress sx={{}} />
      ) : ( items.length == 0 ? (<Typography>There is no any data</Typography>) :
            (<TableContainer component={Paper} elevation={3} sx={{ maxHeight: '50vh' }}>
              <Table stickyHeader>
                <TableHead>
                  <TableRow>
                    <TableCell>№</TableCell>
                    {columns.map((col, index) => (
                      <TableCell key={index}>{col.header}</TableCell>
                    ))}
                  </TableRow>
                </TableHead>
                <TableBody>
                  {items.map((item, rowIndex) => (
                    <TableRow key={rowIndex}>
                      <TableCell>{rowIndex + 1}</TableCell>
                      {columns.map((col, colIndex) => (
                        <TableCell key={colIndex}>{String(item[col.field])}</TableCell>
                      ))}
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>)
      )}
    </Box>
  );
}
