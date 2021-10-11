import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { INewAppointmentToCreateOrEdit } from '../shared/models/appointment';
import { MyParams } from '../shared/models/myparams';
import { IOffice } from '../shared/models/office';
import { IPaginationForAppointments } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class AppointmentsService {
  baseUrl = environment.apiUrl;
  formData: INewAppointmentToCreateOrEdit = new INewAppointmentToCreateOrEdit();

  constructor(private http: HttpClient) { }

  getAppointments(myparams: MyParams) {
    let params = new HttpParams();
    if (myparams.query) {
      params = params.append('query', myparams.query);
    }
    params = params.append('page', myparams.page.toString());
    params = params.append('pageCount', myparams.pageCount.toString());
    return this.http.get<IPaginationForAppointments>(this.baseUrl + 'appointments', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  getOffices() {
    return this.http.get<IOffice[]>(this.baseUrl + 'appointments/offices');
  }


  createAppointment(formData) {
    return this.http.post(this.baseUrl + 'appointments', formData);
  }

}
