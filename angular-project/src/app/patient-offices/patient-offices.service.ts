import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IDoctor } from '../shared/models/doctor';
import { MyParams } from '../shared/models/myparams';
import { IOffice } from '../shared/models/office';
import { IPaginationForAppointments, IPaginationForOffices } from '../shared/models/pagination';
import { IProfessionalAssociation } from '../shared/models/professionalAssociation';
import { IPublication } from '../shared/models/publication';
import { ISpecialization } from '../shared/models/specialization';
import { ISubspecialization } from '../shared/models/subspecialization';

@Injectable({
  providedIn: 'root'
})
export class PatientOfficesService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getOfficesForPatientView(myparams: MyParams) {
    let params = new HttpParams();

    if (myparams.specializationId !== 0) {
      params = params.append('specializationId', myparams.specializationId.toString());
    }

    if (myparams.query) {
      params = params.append('query', myparams.query);
    }
    params = params.append('sort', myparams.sort);
    params = params.append('page', myparams.page.toString());
    params = params.append('pageCount', myparams.pageCount.toString());
    return this.http.get<IPaginationForOffices>(this.baseUrl + 'patients1/offices', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  getAvailableAppointmentsForOfficeForPatient(id: number, myparams: MyParams) {
    let params = new HttpParams();

    if (myparams.query) {
      params = params.append('query', myparams.query);
    }
    params = params.append('sort', myparams.sort);
    params = params.append('page', myparams.page.toString());
    params = params.append('pageCount', myparams.pageCount.toString());
    return this.http.get<IPaginationForAppointments>(this.baseUrl + 'appointments/officeappointments/' + id, {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }
  getSpecializations() {
    return this.http.get<ISpecialization[]>(this.baseUrl + 'patients1/specializations');
  }

  getSpecializationsForDoctor(id: number) {
    return this.http.get<ISpecialization[]>(this.baseUrl + 'doctors1/specializations/' + id);
  }

  getSubspecializationsForDoctor(id: number) {
    return this.http.get<ISubspecialization[]>(this.baseUrl + 'doctors1/subspecializations/' + id);
  }

  getPublicationsForDoctor(id: number) {
    return this.http.get<IPublication[]>(this.baseUrl + 'doctors1/publications/' + id);
  }

  getProfessionalAssociationsForDoctor(id: number) {
    return this.http.get<IProfessionalAssociation[]>(this.baseUrl + 'doctors1/associations/' + id);
  }

  getOfficesForDoctor(id: number) {
    return this.http.get<IOffice[]>(this.baseUrl + 'doctors1/offices/' + id);
  }

  getOffice(id: number) {
    return this.http.get<IOffice>(this.baseUrl + 'appointments/first/' + id);
  }

  getOffice1(id: number) {
    return this.http.get<IOffice>(this.baseUrl + 'offices/' + id);
  }

  getDoctor(id: number) {
    return this.http.get<IDoctor>(this.baseUrl + 'doctors1/' + id);
  }

  public rate(doctorId: number, rating: number){
    return this.http.post(this.baseUrl + 'ratings', {doctorId, rating});
  }

}






