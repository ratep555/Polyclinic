import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AllAppointmentsComponent } from './all-appointments/all-appointments.component';
import { AllDoctorsComponent } from './all-doctors/all-doctors.component';
import { SharedModule } from '../shared/shared.module';
import { VisitorsRoutingModule } from './visitors-routing.module';
import { AllDoctorsFelipeComponent } from './all-doctors-felipe/all-doctors-felipe.component';
import { AllDoctorsFelipe1Component } from './all-doctors-felipe1/all-doctors-felipe1.component';



@NgModule({
  declarations: [
    AllAppointmentsComponent,
    AllDoctorsComponent,
    AllDoctorsFelipeComponent,
    AllDoctorsFelipe1Component
  ],
  imports: [
    CommonModule,
    SharedModule,
    VisitorsRoutingModule
  ]
})
export class VisitorsModule { }
