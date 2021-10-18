import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IAppointment, IAppointmentSingle, INewAppointmentToCreateOrEdit } from '../shared/models/appointment';
import { MyParams } from '../shared/models/myparams';
import { IOffice } from '../shared/models/office';
import { IPaginationForAppointments} from '../shared/models/pagination';
import { ISpecialization } from '../shared/models/specialization';

@Injectable({
  providedIn: 'root'
})
export class PatientAppointmentsService {
  baseUrl = environment.apiUrl;
  formData: INewAppointmentToCreateOrEdit = new INewAppointmentToCreateOrEdit();

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

  cancelAppointment(id: number) {
    return this.http.put(`${this.baseUrl}appointments/updatissimocancel/${id}`, {});
}

editAppointment(formData) {
  return this.http.put(environment.apiUrl + 'appointments/' + formData.id, formData);
}

getApointmentForEdit(id: number) {
  return this.http.get<INewAppointmentToCreateOrEdit>(this.baseUrl + 'appointments/' + id);
}

getAppointmentById(id: number) {
  return this.http.get<IAppointmentSingle>(this.baseUrl + 'appointments/' + id);
}

getOffices() {
  return this.http.get<IOffice[]>(this.baseUrl + 'appointments/offices');
}

}



