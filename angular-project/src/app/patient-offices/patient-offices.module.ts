import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatientOfficesComponent } from './patient-offices.component';
import { SharedModule } from '../shared/shared.module';
import { PatientOfficesRoutingModule } from './patient-offices-routing.module';
import { PatientOfficeDetailComponent } from './patient-office-detail/patient-office-detail.component';
import { DoctorDetailComponent } from './doctor-detail/doctor-detail.component';
import { OfficeInfoComponent } from './office-info/office-info.component';
import { PatientOfficesTestComponent } from './patient-offices-test/patient-offices-test.component';



@NgModule({
  declarations: [
    PatientOfficesComponent,
    PatientOfficeDetailComponent,
    DoctorDetailComponent,
    OfficeInfoComponent,
    PatientOfficesTestComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    PatientOfficesRoutingModule
  ]
})
export class PatientOfficesModule { }
