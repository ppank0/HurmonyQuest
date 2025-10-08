import { Guid } from 'guid-typescript';
import {$host} from '../../../https/index'
import { NominationItem, CreateNominationItem } from "../../../types/types";

export const getNominations = async() =>{
    try{
        var response = await $host.get<NominationItem[]>(`/api/nominations`);
        return response.data;
    }
    catch(e){
        console.error(e);
        return [];
    }
}

// export const getJury = async(id: Guid) =>{
//     try{
//         var response = $host.get<NominationItem[]>(`/api/nominations${id}`);
//         return response
//     }
//     catch(e){
//         console.error(e);
//     }
// }

export const createNomination = async(nominationData: CreateNominationItem) =>{
    try {
        const response = await $host.post<NominationItem>("/api/nominations", nominationData);
        return response.data;
  } catch (e) {
        console.error(e);
        throw e;
  }
}