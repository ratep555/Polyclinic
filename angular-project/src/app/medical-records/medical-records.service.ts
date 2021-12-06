import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IAppointment } from '../shared/models/appointment';
import { INewMedicalrecordToCreate1 } from '../shared/models/medicalrecord';
import { MyParams } from '../shared/models/myparams';
import { IOffice } from '../shared/models/office';
import { IPaginationForMedicalRecords } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class MedicalRecordsService {
  baseUrl = environment.apiUrl;
  formData1: INewMedicalrecordToCreate1 = new INewMedicalrecordToCreate1();


  constructor(private http: HttpClient) { }

  getMedicalRecordsForAllDoctorPatients(myparams: MyParams) {
    let params = new HttpParams();

    if (myparams.officeId !== 0) {
      params = params.append('officeId', myparams.officeId.toString());
    }

    if (myparams.query) {
      params = params.append('query', myparams.query);
    }
    params = params.append('sort', myparams.sort);
    params = params.append('page', myparams.page.toString());
    params = params.append('pageCount', myparams.pageCount.toString());
    return this.http.get<IPaginationForMedicalRecords>(this.baseUrl + 'medicalRecords/allrecords', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  getMedicalRecordsForAllPatientDoctors(myparams: MyParams) {
    let params = new HttpParams();

    if (myparams.officeId !== 0) {
      params = params.append('officeId', myparams.officeId.toString());
    }

    if (myparams.query) {
      params = params.append('query', myparams.query);
    }
    params = params.append('sort', myparams.sort);
    params = params.append('page', myparams.page.toString());
    params = params.append('pageCount', myparams.pageCount.toString());
    return this.http.get<IPaginationForMedicalRecords>(this.baseUrl + 'medicalRecords/allrecords1', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  getOfficesForDoctor() {
    return this.http.get<IOffice[]>(this.baseUrl + 'medicalRecords/offices');
  }

  getAllOffices() {
    return this.http.get<IOffice[]>(this.baseUrl + 'medicalRecords/offices1');
  }

  createMedicalRecord1(values: any) {
    return this.http.post(this.baseUrl + 'medicalrecords/record1/' + this.formData1.appointment1Id, values);
  }

  updateMedicalRecord(formData1) {
    return this.http.put(this.baseUrl + 'medicalrecords/record1/' + formData1.appointment1Id, formData1);
  }

  getAppointment(id: number) {
    return this.http.get<IAppointment>(this.baseUrl + 'medicalRecords/' + id);
  }
}












