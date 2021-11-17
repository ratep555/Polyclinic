import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppointmentsComponent } from './appointments.component';
import { AppointmentAddComponent } from './appointment-add/appointment-add.component';
import { SharedModule } from '../shared/shared.module';
import { AppointmentsRoutingModule } from './appointments-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import { AppointmentEditComponent } from './appointment-edit/appointment-edit.component';
import { PatientDetailComponent } from './patient-detail/patient-detail.component';
import { MedicalRecordDetailComponent } from './medical-record-detail/medical-record-detail.component';
import { MedicalRecordAddComponent } from './medical-record-add/medical-record-add.component';
import { ListPatientDoctorComponent } from './list-patient-doctor/list-patient-doctor.component';


@NgModule({
  declarations: [
    AppointmentsComponent,
    AppointmentAddComponent,
    AppointmentEditComponent,
    PatientDetailComponent,
    MedicalRecordDetailComponent,
    MedicalRecordAddComponent,
    ListPatientDoctorComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    AppointmentsRoutingModule
  ]
})
export class AppointmentsModule { }
