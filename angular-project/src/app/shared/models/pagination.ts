import { IAppointment } from './appointment';
import { IDoctor } from './doctor';
import { IMedicalrecord, IMedicalrecord1 } from './medicalrecord';
import { IOffice } from './office';
import { IPatient } from './patient';
import { ISpecialty } from './specialty';

export interface IPaginationForSpecialties {
    page: number;
    pageCount: number;
    count: number;
    data: ISpecialty[];
  }

export interface IPaginationForOffices {
    page: number;
    pageCount: number;
    count: number;
    data: IOffice[];
  }

export interface IPaginationForAppointments {
    page: number;
    pageCount: number;
    count: number;
    data: IAppointment[];
  }

export interface IPaginationForMedicalRecords {
    page: number;
    pageCount: number;
    count: number;
    data: IMedicalrecord[];
  }

export interface IPaginationForMedicalRecords1 {
    page: number;
    pageCount: number;
    count: number;
    data: IMedicalrecord1[];
  }

export interface IPaginationForDoctors {
    page: number;
    pageCount: number;
    count: number;
    data: IDoctor[];
  }

export interface IPaginationForPatients {
    page: number;
    pageCount: number;
    count: number;
    data: IPatient[];
  }
