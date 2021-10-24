export interface IMedicalrecord {
    id: number;
    anamnesisDiagnosisTherapy: string;
    created: Date;
    doctor: string;
    patient: string;
    office: string;
}

export class INewMedicalrecordToCreate {
    id: number;
    anamnesisDiagnosisTherapy: string;
    created: Date;
    patient1Id: number;
    office1Id: number;
}
