import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AccountService } from '../account/account.service';
import { INewAppointmentToCreateOrEdit } from '../shared/models/appointment';
import { IMedicalrecord, INewMedicalrecordToCreate } from '../shared/models/medicalrecord';
import { MyParams } from '../shared/models/myparams';
import { IOffice } from '../shared/models/office';
import { IPaginationForAppointments, IPaginationForMedicalRecords, IPaginationForPatients } from '../shared/models/pagination';
import { IPatient } from '../shared/models/patient';
import { User } from '../shared/models/user';
import { UserParams } from '../shared/models/userParams';

@Injectable({
  providedIn: 'root'
})
export class AppointmentsService {
  baseUrl = environment.apiUrl;
  formData: INewAppointmentToCreateOrEdit = new INewAppointmentToCreateOrEdit();
  formData1: INewMedicalrecordToCreate = new INewMedicalrecordToCreate();
  user: User;
  userParams: UserParams;

  constructor(private http: HttpClient,
              private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take (1)).subscribe(user => {
      this.user = user;
      this.userParams = new UserParams(user);
    });
  }

  getUserParams() {
    return this.userParams;
  }

  setUserParams(params: UserParams) {
    this.userParams = params;
  }

  resetUserParams() {
    this.userParams = new UserParams(this.user);
    return this.userParams;
  }

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

  getDoctorPatients(userParams: UserParams) {
    let params = new HttpParams();
    if (userParams.query) {
      params = params.append('query', userParams.query);
    }
    params = params.append('sort', userParams.sort);
    params = params.append('page', userParams.page.toString());
    params = params.append('pageCount', userParams.pageCount.toString());
    return this.http.get<IPaginationForPatients>(this.baseUrl + 'patients1/yahoo', {observe: 'response', params})
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
