import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MyParams } from '../shared/models/myparams';
import { IPaginationForAppointments} from '../shared/models/pagination';
import { ISpecialization } from '../shared/models/specialization';

@Injectable({
  providedIn: 'root'
})
export class PatientAppointmentsService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAppointmentsForPatientView(myparams: MyParams) {
    let params = new HttpParams();

    if (myparams.query) {
      params = params.append('query', myparams.query);
    }
    params = params.append('sort', myparams.sort);
    params = params.append('page', myparams.page.toString());
    params = params.append('pageCount', myparams.pageCount.toString());
    return this.http.get<IPaginationForAppointments>(this.baseUrl + 'appointments/allpatients', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  bookAppointment(id: number) {
    return this.http.put(`${this.baseUrl}appointments/updatissimo/${id}`, {});
}
}
