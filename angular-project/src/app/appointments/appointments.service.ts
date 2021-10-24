import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { INewAppointmentToCreateOrEdit } from '../shared/models/appointment';
import { IMedicalrecord, INewMedicalrecordToCreate } from '../shared/models/medicalrecord';
import { MyParams } from '../shared/models/myparams';
import { IOffice } from '../shared/models/office';
import { IPaginationForAppointments, IPaginationForMedicalRecords } from '../shared/models/pagination';
import { IPatient } from '../shared/models/patient';

@Injectable({
  providedIn: 'root'
})
export class AppointmentsService {
  baseUrl = environment.apiUrl;
  formData: INewAppointmentToCreateOrEdit = new INewAppointmentToCreateOrEdit();
  formData1: INewMedicalrecordToCreate = new INewMedicalrecordToCreate();

  constructor(private http: HttpClient) { }

  getAppointments(myparams: MyParams) {
    let params = new HttpParams();
    if (myparams.query) {
      params = params.append('query', myparams.query);
    }

    params = params.append('sort', myparams.sort);
    params = params.append('page', myparams.page.toString());
    params = params.append('pageCount', myparams.pageCount.toString());
    return this.http.get<IPaginationForAppointments>(this.baseUrl + 'appointments', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  getMedicalrecordsForPatient(id: number, myparams: MyParams) {
    let params = new HttpParams();

    if (myparams.query) {
      params = params.append('query', myparams.query);
    }
    params = params.append('sort', myparams.sort);
    params = params.append('page', myparams.page.toString());
    params = params.append('pageCount', myparams.pageCount.toString());
    return this.http.get<IPaginationForMedicalRecords>
    (this.baseUrl + 'medicalRecords/records/' + id, {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  getOffices() {
    return this.http.get<IOffice[]>(this.baseUrl + 'appointments/offices');
  }

  getPatient(id: number) {
    return this.http.get<IPatient>(this.baseUrl + 'medicalRecords/' + id);
  }

  getMedicalRecord(id: number) {
    return this.http.get<IMedicalrecord>(this.baseUrl + 'medicalRecords/singlerecord/' + id);
  }

  createMedicalRecord(values: any) {
    return this.http.post(`${this.baseUrl}medicalRecords/${this.formData1.patient1Id}`, values);
  }

  createAppointment(formData) {
    return this.http.post(this.baseUrl + 'appointments', formData);
  }

  editAppointment(formData) {
    return this.http.put(environment.apiUrl + 'appointments/doc/' + formData.id, formData);
  }

}
