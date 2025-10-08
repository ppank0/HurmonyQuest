import { ParticipantItem } from "../../../types/types";
import {$host} from '../../../https/index'

export const getParticipants = async() =>{
    try{
        var response = await $host.get<ParticipantItem[]>(`/api/participants`);
        return response.data;
    }
    catch(e){
        console.error(e);
        return [];
    }
}