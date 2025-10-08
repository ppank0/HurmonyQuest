import {$host, $authHost} from '../../../https/index'
import { JuryItem } from "../../../types/types";

export const getJuries = async() =>{
    try{
        var response = await $authHost.get<JuryItem[]>(`/api/juries`);
        return response.data;
    }
    catch(e){
        console.error(e);
        return [];
    }
}