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
