export interface IOffice {
    id: number;
    doctor: string;
    doctorId: number;
    initialExaminationFee: number;
    followUpExaminationFee: number;
    street: string;
    city: string;
    country: string;
    latitude: number;
    description: string;
    longitude: number;
    picture: string;
    photo: string;
}

export class INewOfficeToCreateOrEdit {
    id: number;
    initialExaminationFee: number;
    followUpExaminationFee: number;
    street: string;
    city: string;
    country: string;
    description: string;
    latitude: number;
    longitude: number;
    hospitalAffiliationId: number;
}

export interface INewOfficeToCreate {
    initialExaminationFee: number;
    followUpExaminationFee: number;
    street: string;
    city: string;
    country: string;
    latitude: number;
    longitude: number;
}

export interface INewOfficeToCreate1 {
    id: number;
    initialExaminationFee: number;
    followUpExaminationFee: number;
    street: string;
    city: string;
    country: string;
    description: string;
    latitude: number;
    longitude: number;
    picture: File;
}

export interface INewOfficeToEdit {
    id: number;
    initialExaminationFee: number;
    followUpExaminationFee: number;
    street: string;
    city: string;
    country: string;
    latitude: number;
    longitude: number;
}
