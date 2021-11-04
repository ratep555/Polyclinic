import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { PatientOfficesService } from 'src/app/patient-offices/patient-offices.service';
import { IDoctor } from 'src/app/shared/models/doctor';

@Injectable({
  providedIn: 'root'
})
export class DoctorDetailResolver implements Resolve<IDoctor> {
  constructor(private patientofficeService: PatientOfficesService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<IDoctor> {
    return this.patientofficeService.getDoctor1(+route.paramMap.get('id'));
  }
}
