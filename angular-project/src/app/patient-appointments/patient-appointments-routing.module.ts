import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { PatientAppointmentsComponent } from './patient-appointments.component';
import { PatientAppointmentEditComponent } from './patient-appointment-edit/patient-appointment-edit.component';

const routes: Routes = [
  {path: '', component: PatientAppointmentsComponent},
  {path: 'editappointment/:id', component: PatientAppointmentEditComponent}

];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class PatientAppointmentsRoutingModule { }
