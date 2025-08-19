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