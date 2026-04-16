import { useEffect, useState } from "react";
import { ContestService } from "../../../shared/api/contestService/contestService";
import type { editNomination, Nomination } from "../../../shared/api/contestService/interfaces";

export const useNomination = () => {
    const [nominations, setNominations] = useState<Nomination []>([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const fetchNominations = async() =>{
        setIsLoading(true);
        try{
            const response = await ContestService.nominations.getAll();
            response ? setNominations(response) : setNominations([]);
        }
        catch(error: any){
            setError(error);
        }
        finally{
            setIsLoading(false);
        }
    };

    const deleteNomination = async(id: string) =>{
        try{
            await ContestService.nominations.delete(id);
            setNominations(prev => prev.filter(n => n.id !== id));
        }
        catch(error: any) {
            console.error("Error while deleting:", error);
            setError(error);
        }
    };

    const createNomination = async (item: editNomination) => {
        try {
            const response = await ContestService.nominations.create(item);
                if (response) {
                    setNominations(prev => ([...prev, response]));
                }
            } catch (error: any) {
                console.error("Error while creating:", error);
                setError(error);
        }
    };

    const editNomination = async(id: string, item: editNomination) => {
        try{
            const response = await ContestService.nominations.update(id, item);
                if(response){
                    setNominations(prev => prev.map(n => n.id === response.id ? response : n));
                }
            } catch (error: any) {
                console.error("Error while editing:", error);
                setError(error);
        }
    };

    useEffect(() => {
        fetchNominations();
    }, []);

    return{nominations, isLoading, error, createNomination, editNomination, deleteNomination};
}