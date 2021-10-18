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
    longitude: number;
}

export class INewOfficeToCreateOrEdit {
    id: number;
    initialExaminationFee: number;
    followUpExaminationFee: number;
    street: string;
    city: string;
    country: string;
    latitude: number;
    longitude: number;
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
