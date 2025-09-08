import { ComponentType } from 'react';
import { Guid } from 'guid-typescript';

export interface RouteConfig {
    path: string;
    Component: ComponentType;
}

export interface JuryItem {
    id: Guid;
    name: string;
    surname: string;
    birthday: string;
}

export interface ParticipantItem {
    id: Guid;
    name: string;
    surname: string;
    birthday: string;
    musicalInstrumentId: Guid;
    musicalInstrumentName: string
    nominationId: Guid;
}

export interface NominationItem {
    id: Guid;
    name: string;
    musicalInstrumentId: Guid;
    musicalInstrumentName: string
}

export interface CreateNominationItem {
    name: string;
}

export interface User{
    id: Guid,
    email: string,
    userPictureUrl: string,
    authId: string,
}