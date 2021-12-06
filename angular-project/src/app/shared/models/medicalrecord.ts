export interface IMedicalrecord {
    id: number;
    anamnesisDiagnosisTherapy: string;
    created: Date;
    doctor: string;
    patient: string;
    office: string;
    appointment1Id: number;

}

export class INewMedicalrecordToCreate {
    id: number;
    anamnesisDiagnosisTherapy: string;
    created: Date;
    patient1Id: number;
    office1Id: number;
}

export interface IMedicalrecord1 {
    id: number;
    anamnesisDiagnosisTherapy: string;
    created: Date;
    patient: string;
    doctor: string;
    office: string;
    appointment1Id: number;
}

export class INewMedicalrecordToCreate1 {
    anamnesisDiagnosisTherapy: string;
    created: Date;
    appointment1Id: number;
}
