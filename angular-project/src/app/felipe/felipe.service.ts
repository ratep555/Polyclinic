import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IDoctor } from '../shared/models/doctor';
import { MyParams } from '../shared/models/myparams';
import { IPaginationForDoctors } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class FelipeService {

  constructor(private http: HttpClient) { }
  baseUrl = environment.apiUrl;

  public filter(values: any): Observable<any>{
    const params = new HttpParams({fromObject: values});
    return this.http.get<IDoctor[]>(`${this.baseUrl}doctors1/filter`, {params, observe: 'response'});
  }

  public filter1(values: any): Observable<any>{
    const params = new HttpParams({fromObject: values});
    return this.http.get<IDoctor[]>(`${this.baseUrl}doctors1/filter2`, {params, observe: 'response'});
  }

  getDoctors(values: any): Observable<any>{
    const params = new HttpParams({fromObject: values});
    return this.http.get<IPaginationForDoctors>(this.baseUrl + 'doctors1/filter3', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }
}
