import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AccountService } from '../account/account.service';
import { IDoctor } from '../shared/models/doctor';
import { MyParams } from '../shared/models/myparams';
import { IOffice } from '../shared/models/office';
import { IPaginationForAppointments, IPaginationForOffices } from '../shared/models/pagination';
import { IProfessionalAssociation } from '../shared/models/professionalAssociation';
import { IPublication } from '../shared/models/publication';
import { ISpecialization } from '../shared/models/specialization';
import { ISubspecialization } from '../shared/models/subspecialization';
import { User } from '../shared/models/user';
import { UserParams } from '../shared/models/userParams';

@Injectable({
  providedIn: 'root'
})
export class PatientOfficesService {
  baseUrl = environment.apiUrl;
  memberCache = new Map();
  user: User;
  userParams: UserParams;

  constructor(private http: HttpClient, private accountService: AccountService) {
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

  getOfficesForPatientView1(userParams: UserParams) {
    const response = this.memberCache.get(Object.values(userParams).join('-'));
    if (response) {
      return of(response);
    }
    let params = new HttpParams();

    if (userParams.specializationId !== 0) {
      params = params.append('specializationId', userParams.specializationId.toString());
    }

    if (userParams.query) {
      params = params.append('query', userParams.query);
    }
    params = params.append('sort', userParams.sort);
    params = params.append('page', userParams.page.toString());
    params = params.append('pageCount', userParams.pageCount.toString());
    return this.http.get<IPaginationForOffices>(this.baseUrl + 'patients1/offices', {observe: 'response', params})
    .pipe(
      map(response  => {
        this.memberCache.set(Object.values(userParams).join('-'), response.body);
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

  getDoctor1(id: number) {
    const member = [...this.memberCache.values()]
    // we need single array that we can use to find user, we will use reduce, imaš ga u skinet
    // reduce ima previous i current value, hover over reduce
    // previous value je array, we will aply callback to each element of the array
    // we will concatenate array,[] is initial value
    // as we call this function on each element of the array, we get result that contains x-number of members and
    // then we will concatenate that on the array we have and which has initial value of []
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((member: IDoctor) => member.applicationUserId === id);

    if (member) {
      return of(member);
    }
    return this.http.get<IDoctor>(this.baseUrl + 'doctors1/appuserid/' + id);
  }

  public rate(doctorId: number, rating: number){
    return this.http.post(this.baseUrl + 'ratings', {doctorId, rating});
  }

}






