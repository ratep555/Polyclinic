import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { MyParams } from '../shared/models/myparams';
import { IPaginationForOffices } from '../shared/models/pagination';
import { map } from 'rxjs/operators';
import { INewOfficeToCreate, INewOfficeToCreateOrEdit, IOffice } from '../shared/models/office';

@Injectable({
  providedIn: 'root'
})
export class OfficesService {
  baseUrl = environment.apiUrl;
  formData: INewOfficeToCreateOrEdit = new INewOfficeToCreateOrEdit();

  constructor(private http: HttpClient) { }

  getOffices(myparams: MyParams) {
    let params = new HttpParams();
    if (myparams.query) {
      params = params.append('query', myparams.query);
    }
    params = params.append('page', myparams.page.toString());
    params = params.append('pageCount', myparams.pageCount.toString());
    return this.http.get<IPaginationForOffices>(this.baseUrl + 'offices', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  createOffice(formData) {
    return this.http.post(this.baseUrl + 'offices', formData);
  }

  createOffice1(office: INewOfficeToCreate) {
    return this.http.post(this.baseUrl + 'offices', office);
  }

  updateOffice(id: number, params: any) {
    return this.http.put(`${this.baseUrl}offices/${id}`, params);
  }

  updateOffice1(id: number, office: INewOfficeToCreate) {
    return this.http.put(`${this.baseUrl}offices/first/${id}`, office);
  }

  getOfficeById(id: number) {
    return this.http.get<IOffice>(`${this.baseUrl}offices/${id}`);
  }

}







