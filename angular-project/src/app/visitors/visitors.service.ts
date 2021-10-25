import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MyParams } from '../shared/models/myparams';
import { IPaginationForAppointments } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class VisitorsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

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
}
