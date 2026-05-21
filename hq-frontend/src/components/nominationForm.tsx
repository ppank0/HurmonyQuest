import { Box, TextField, Button} from "@mui/material";
import { useForm, type SubmitHandler} from "react-hook-form";
import type { editNomination} from "../shared/api/contestService/interfaces";
import type { CustomFormProps } from "./interfaces/formInterfaces";

export function NominationForm(formData: CustomFormProps<editNomination>){
        const { register, handleSubmit, formState: { errors } } = useForm({
            defaultValues: {name: formData.data?.name || ""},
            mode: "onBlur"
        });

    const onSubmit: SubmitHandler<editNomination> = async(data) => {
        try{
            if (formData.action == "create") {
                formData.onCreate?.(data);
            } else {
                formData.onEdit?.(data);
            }
        }catch(e:any){
            console.log(e)
        }
    };  

    return(
            <Box component={"form"}
                onSubmit={handleSubmit(onSubmit)}
                sx={{display:"flex", flexDirection:'column', gap: "2rem", alignItems: "center"}}>
                    <TextField label="name"
                    {...register("name", {required: "Name is required",
                        validate: (value) => value.trim() !== "" || "Name cannot be empty or just spaces"
                    })}
                    error={!!errors.name}
                    helperText = {errors.name?.message} />
                
                <Button type="submit" variant="outlined">{formData.action == "create" ? "Create" : "Edit"}</Button>
            </Box>
    );
}