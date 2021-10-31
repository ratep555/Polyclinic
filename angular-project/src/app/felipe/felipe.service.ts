import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IDoctor } from '../shared/models/doctor';

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
}
