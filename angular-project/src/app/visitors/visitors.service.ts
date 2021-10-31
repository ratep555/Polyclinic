import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AccountService } from '../account/account.service';
import { IDoctor } from '../shared/models/doctor';
import { MyParams } from '../shared/models/myparams';
import { IPaginationForAppointments, IPaginationForDoctors, IPaginationForOffices } from '../shared/models/pagination';
import { ISpecialization } from '../shared/models/specialization';
import { User } from '../shared/models/user';
import { UserParams } from '../shared/models/userParams';

@Injectable({
  providedIn: 'root'
})
export class VisitorsService {
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

  getAllAvailableAppointmentsForAllVisitors(myparams: MyParams) {
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
    return this.http.get<IPaginationForAppointments>(this.baseUrl + 'appointments/allvisitors', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  getAllDoctors(userParams: UserParams) {
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
    return this.http.get<IPaginationForDoctors>(this.baseUrl + 'doctors1/alldoctors', {observe: 'response', params})
    .pipe(
      map(response  => {
        this.memberCache.set(Object.values(userParams).join('-'), response.body);
        return response.body;
      })
    );
  }


  public filter(values: any): Observable<any>{
    const params = new HttpParams({fromObject: values});
    return this.http.get<IDoctor[]>(`${this.baseUrl}doctors1/filter`, {params, observe: 'response'});
  }

  getSpecializations() {
    return this.http.get<ISpecialization[]>(this.baseUrl + 'patients1/specializations');
  }
}










