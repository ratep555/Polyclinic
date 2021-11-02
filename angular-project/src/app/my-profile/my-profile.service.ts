import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IDoctor, IDoctorPutGetDto, IEditDoctorDto, IRegisterDoctorDto } from '../shared/models/doctor';
import { IOffice } from '../shared/models/office';
import { IProfessionalAssociation } from '../shared/models/professionalAssociation';
import { IPublication } from '../shared/models/publication';
import { ISpecialization } from '../shared/models/specialization';
import { ISubspecialization } from '../shared/models/subspecialization';

@Injectable({
  providedIn: 'root'
})
export class MyProfileService {
  baseUrl = environment.apiUrl;
  memberCache = new Map();

  constructor(private http: HttpClient) { }

  getDoctors() {
    return this.http.get<IDoctor[]>(this.baseUrl + 'doctors1/doctorlist');
  }

  getDoctorWithQualificationsAndOffices(id: number) {
    /* const doctor = [...this.memberCache.values()]
    // we need single array that we can use to find user, we will use reduce, imaš ga u skinet
    // reduce ima previous i current value, hover over reduce
    // previous value je array, we will aply callback to each element of the array
    // we will concatenate array,[] is initial value
    // as we call this function on each element of the array, we get result that contains x-number of members and
    // then we will concatenate that on the array we have and which has initial value of []
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((doctor: IDoctorWithQualificationsAndOffices) => doctor.doctor.applicationUserId === id);

    if (doctor) {
      return of(doctor);
    } */
    return this.http.get<IDoctor>(this.baseUrl + 'doctors1/appuserid/' + id);
  }

  getSpecializationsForDoctor(id: number) {
    return this.http.get<ISpecialization[]>(this.baseUrl + 'doctors1/specializations1/' + id);
  }

  getSubspecializationsForDoctor(id: number) {
    return this.http.get<ISubspecialization[]>(this.baseUrl + 'doctors1/subspecializations1/' + id);
  }

  getPublicationsForDoctor(id: number) {
    return this.http.get<IPublication[]>(this.baseUrl + 'doctors1/publications1/' + id);
  }

  getProfessionalAssociationsForDoctor(id: number) {
    return this.http.get<IProfessionalAssociation[]>(this.baseUrl + 'doctors1/associations1/' + id);
  }

  getOfficesForDoctor(id: number) {
    return this.http.get<IOffice[]>(this.baseUrl + 'doctors1/offices1/' + id);
  }

  putGetDoctor(id: number): Observable<IDoctorPutGetDto>{
    return this.http.get<IDoctorPutGetDto>(this.baseUrl + `doctors1/appuserid1/` + id);
  }

  editDoctor(id: number, editDoctorDTO: IEditDoctorDto){
    return this.http.put(this.baseUrl + `doctors1/editingdoctor/` + id, editDoctorDTO);
  }
}









