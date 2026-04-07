import { contestApi } from "./contestApi"
import { type Nomination, type MusicalInstrument, type editNomination, type MusicalInstrumentExtended, type editMusicalInstrument, type ParticipantExtended, type Participant, type editParticipant, type Stage, type editStage, type Jury, type editJury } from "./interfaces";

export const ContestService = {
    nominations: {
        async getAll(){
            const response = await contestApi.get<Nomination[] | null>("/nominations");
            return response.data;
        },
        async getById(id: string){
            const response = await contestApi.post<Nomination>(`/nominations/${id}`)
            return response;
        },
        async create(newNominations: editNomination){
            const response = await contestApi.post<editNomination>("/nominations", newNominations)
            return response.data;
        },
        async update(id: string, toUpdate: editNomination){
            const response = await contestApi.patch<editNomination>(`/nomination/${id}`, toUpdate)
            return response.data;
        },
        async delete(id: string){
            const response = await contestApi.delete<Nomination>(`/nominations/${id}`)
            return response;
        }
    },
    musicalInstruments:{
        async getAll(){
            const response = await contestApi.get<MusicalInstrumentExtended[]>("/instruments")
            return response.data;
        },
        async getById(id: string){
            const response = await contestApi.post<MusicalInstrumentExtended>(`/instruments/${id}`)
            return response;
        },
        async create(newInstrument: editMusicalInstrument){
            const response = await contestApi.post<MusicalInstrument>("/instruments", newInstrument)
            return response.data;
        },
        async update(id: string, toUpdate: editMusicalInstrument){
            const response = await contestApi.patch<MusicalInstrument>(`/instrumnets/${id}`, toUpdate)
            return response.data;
        },
        async delete(id: string){
            const response = await contestApi.delete<MusicalInstrument>(`/instruments/${id}`)
            return response;
        }
    },
    participants:{
        async getAll(){
            const response = await contestApi.get<ParticipantExtended[]>("/participants")
            return response.data;
        },
        async getById(id: string){
            const response = await contestApi.post<ParticipantExtended>(`/participants/${id}`)
            return response;
        },
        async create(newParticipant: editParticipant){
            const response = await contestApi.post<Participant>("/participants", newParticipant)
            return response.data;
        },
        async update(id: string, toUpdate: editParticipant){
            const response = await contestApi.patch<Participant>(`/participants/${id}`, toUpdate)
            return response.data;
        },
        async delete(id: string){
            const response = await contestApi.delete<Participant>(`/participants/${id}`)
            return response;
        }
    },
    stages:{
        async getAll(){
            const response = await contestApi.get<Stage[]>("/stages")
            return response.data;
        },
        async getById(id: string){
            const response = await contestApi.post<Stage>(`/stages/${id}`)
            return response;
        },
        async create(newStage: editStage){
            const response = await contestApi.post<Stage>("/stages", newStage)
            return response.data;
        },
        async update(id: string, toUpdate: editStage){
            const response = await contestApi.patch<Stage>(`/stages/${id}`, toUpdate)
            return response.data;
        },
        async delete(id: string){
            const response = await contestApi.delete<Stage>(`/stages/${id}`)
            return response;
        }
    },
    juries:{
        async getAll(){
            const response = await contestApi.get<Jury[]>("/juries")
            return response.data;
        },
        async getById(id: string){
            const response = await contestApi.post<Jury>(`/juries/${id}`)
            return response;
        },
        async create(newJury: editJury){
            const response = await contestApi.post<Stage>("/juries", newJury)
            return response.data;
        },
        async update(id: string, toUpdate: editJury){
            const response = await contestApi.patch<Jury>(`/juries/${id}`, toUpdate)
            return response.data;
        },
        async delete(id: string){
            const response = await contestApi.delete<Jury>(`/juries/${id}`)
            return response;
        }
    }
}