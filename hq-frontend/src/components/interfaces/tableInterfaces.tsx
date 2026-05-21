export interface Column<T>{
    key: keyof T,
    header: string,
}

export interface CustomTableProps<T>{
    column: Column<T>[],
    data: T[],
    renderActions: boolean,
    onEdit?: (Item: T) => void,
    onDelete?: (Item: T) => void
}