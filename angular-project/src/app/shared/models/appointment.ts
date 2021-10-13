export interface IAppointment {
    id: number;
    doctor: string;
    patient: string;
    officeAddress: string;
    city: string;
    startDateAndTimeOfAppointment: Date;
    endDateAndTimeOfAppointment: Date;
    status: boolean;
    remarks: string;
}

export interface IAppointmentSingle {
    id: number;
    doctor: string;
    patient: string;
    patientId: number;
    officeId: number;
    office: string;
    city: string;
    country: string;
    startDateAndTimeOfAppointment: Date;
    endDateAndTimeOfAppointment: Date;
    status: boolean;
    remarks: string;
}

export class INewAppointmentToCreateOrEdit {
        id: number;
        office1Id: number;
        patient1Id: number;
        startDateAndTimeOfAppointment: Date;
        endDateAndTimeOfAppointment: Date;
        remarks: string;
        status: string;
}
