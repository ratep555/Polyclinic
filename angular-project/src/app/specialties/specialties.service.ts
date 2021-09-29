import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { MyParams } from '../shared/models/myparams';
import { IPaginationForSpecialties } from '../shared/models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SpecialtiesService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getSpecialties(myparams: MyParams) {
    let params = new HttpParams();
    if (myparams.query) {
      params = params.append('query', myparams.query);
    }
    params = params.append('page', myparams.page.toString());
    params = params.append('pageCount', myparams.pageCount.toString());
    return this.http.get<IPaginationForSpecialties>(this.baseUrl + 'specialties', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }
}
