import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MyParams } from '../shared/models/myparams';
import { IPaginationForOffices } from '../shared/models/pagination';
import { ISpecialization } from '../shared/models/specialization';

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

  getSpecializations() {
    return this.http.get<ISpecialization[]>(this.baseUrl + 'patients1/specializations');
  }

}
