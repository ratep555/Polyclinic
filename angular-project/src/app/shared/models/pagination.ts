import { IAppointment } from './appointment';
import { IOffice } from './office';
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
