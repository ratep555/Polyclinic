import { IOffice } from './office';
import { IProfessionalAssociation } from './professionalAssociation';
import { IPublication } from './publication';
import { ISpecialization } from './specialization';
import { ISubspecialization } from './subspecialization';

export interface IDoctor {
    id: number;
    applicationUserId: number;
    name: string;
    resume: string;
    averageVote: number;
    userVote: number;
    count: number;
    startedPracticing: Date;
}

export interface IDoctorWithQualificationsAndOffices {
    doctor: IDoctor;
    specializations: ISpecialization[];
    subspecializations: ISubspecialization[];
    professionalAssociations: IProfessionalAssociation[];
    publications: IPublication[];
    offices: IOffice[];
}

export interface IDoctorPutGetDto {
    doctor: IDoctor;
    selectedSpecializations: ISpecialization[];
    nonSelectedSpecializations: ISpecialization[];

}

export interface IRegisterDoctorDto {
    firstName: string;
    lastName: string;
    username: string;
    email: string;
    resume: string;
    startedPracticing: Date;
    specializationsIds: number[];
}

export interface IEditDoctorDto {
    id: number;
    applicationUserId: number;
    resume: string;
    specializationsIds: number[];
}



