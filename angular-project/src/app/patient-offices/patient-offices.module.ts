import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatientOfficesComponent } from './patient-offices.component';
import { SharedModule } from '../shared/shared.module';
import { PatientOfficesRoutingModule } from './patient-offices-routing.module';



@NgModule({
  declarations: [
    PatientOfficesComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    PatientOfficesRoutingModule
  ]
})
export class PatientOfficesModule { }
