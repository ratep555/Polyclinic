import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatientAppointmentsComponent } from './patient-appointments.component';
import { SharedModule } from '../shared/shared.module';
import { PatientOfficesRoutingModule } from '../patient-offices/patient-offices-routing.module';
import { PatientAppointmentsRoutingModule } from './patient-appointments-routing.module';



@NgModule({
  declarations: [
    PatientAppointmentsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    PatientAppointmentsRoutingModule
  ]
})
export class PatientAppointmentsModule { }
