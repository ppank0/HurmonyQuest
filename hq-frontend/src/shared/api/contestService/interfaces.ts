export interface Nomination{
    id: string,
    name: string
}
export interface editNomination{
    name: string
}

export interface MusicalInstrument{
    id:string,
    name: string,
    nominationId: string,
}
export interface editMusicalInstrument{
    name: string,
    nominationId: string,
}
export interface MusicalInstrumentExtended{
    id:string,
    name: string,
    nominationId: string,
    NominationName: string
}

export interface Participant{
    Id: string,
    Name: string,
    Surname: string,
    Birthday: Date,
    MusicalInstrumentId: string,
    NominationId: string,
    UserId: string
}
export interface editParticipant{
    Name: string,
    Surname: string,
    Birthday: Date,
    MusicalInstrumentId: string,
    NominationId: string,
    UserId: string
}
export interface ParticipantExtended{
    Id: string,
    Name: string,
    Surname: string,
    Birthday: Date,
    MusicalInstrumentId: string,
    MusicalInstrumentName: string,
    NominationId: string,
    NominationName: string,
    UserId: string
}

export interface Stage{
    Id: string,
    Name: string,
    StartDate: Date,
    EndDate: Date
}
export interface editStage{
    Name: string,
    StartDate: Date,
    EndDate: Date
}

export interface Jury{
    Id: string,
    Name: string,
    Surname: string,
    Birthday: Date,
    UserId: string
}
export interface editJury{
    Name: string,
    Surname: string,
    Birthday: Date,
    UserId: string
}

