export interface Fields<T>{
    type: string,
    lable: string,
    name: keyof T
}

export type Action = "create" | "edit" | "delete"

export interface CustomFormProps<T>{
    data?: T,
    action?: Action,
    onCreate?: (item: T) => void,
    onEdit?: (Item: T) => void
}