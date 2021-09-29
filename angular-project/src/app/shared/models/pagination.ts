import { ISpecialty } from './specialty';

export interface IPaginationForSpecialties {
    page: number;
    pageCount: number;
    count: number;
    data: ISpecialty[];
  }
