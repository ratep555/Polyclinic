import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AppointmentsComponent } from './appointments.component';
import { AppointmentAddComponent } from './appointment-add/appointment-add.component';
import { AppointmentEditComponent } from './appointment-edit/appointment-edit.component';
import { PatientDetailComponent } from './patient-detail/patient-detail.component';
import { MedicalRecordDetailComponent } from './medical-record-detail/medical-record-detail.component';
import { MedicalRecordAddComponent } from './medical-record-add/medical-record-add.component';
import { ListPatientDoctorComponent } from './list-patient-doctor/list-patient-doctor.component';

const routes: Routes = [
  {path: '', component: AppointmentsComponent},
  {path: 'addappointment', component: AppointmentAddComponent},
  {path: 'listpatient', component: ListPatientDoctorComponent},
  {path: 'editappointment/:id', component: AppointmentEditComponent},
  {path: 'patient/:id', component: PatientDetailComponent},
  {path: 'record/:id', component: MedicalRecordDetailComponent},
  {path: 'recordadd/:id', component: MedicalRecordAddComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class AppointmentsRoutingModule { }
