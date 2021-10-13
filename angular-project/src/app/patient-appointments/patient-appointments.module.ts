import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatientAppointmentsComponent } from './patient-appointments.component';
import { SharedModule } from '../shared/shared.module';
import { PatientOfficesRoutingModule } from '../patient-offices/patient-offices-routing.module';
import { PatientAppointmentsRoutingModule } from './patient-appointments-routing.module';
import { PatientAppointmentEditComponent } from './patient-appointment-edit/patient-appointment-edit.component';



@NgModule({
  declarations: [
    PatientAppointmentsComponent,
    PatientAppointmentEditComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    PatientAppointmentsRoutingModule
  ]
})
export class PatientAppointmentsModule { }
