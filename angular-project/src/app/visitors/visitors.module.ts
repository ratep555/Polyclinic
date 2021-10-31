import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AllAppointmentsComponent } from './all-appointments/all-appointments.component';
import { AllDoctorsComponent } from './all-doctors/all-doctors.component';
import { SharedModule } from '../shared/shared.module';
import { VisitorsRoutingModule } from './visitors-routing.module';
import { AllDoctorsFelipeComponent } from './all-doctors-felipe/all-doctors-felipe.component';



@NgModule({
  declarations: [
    AllAppointmentsComponent,
    AllDoctorsComponent,
    AllDoctorsFelipeComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    VisitorsRoutingModule
  ]
})
export class VisitorsModule { }
