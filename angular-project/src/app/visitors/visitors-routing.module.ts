import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AllAppointmentsComponent } from './all-appointments/all-appointments.component';
import { AllDoctorsComponent } from './all-doctors/all-doctors.component';
import { AllDoctorsFelipeComponent } from './all-doctors-felipe/all-doctors-felipe.component';



const routes: Routes = [
  {path: 'allapointments', component: AllAppointmentsComponent},
  {path: 'alldoctors', component: AllDoctorsComponent},
  {path: 'felipe', component: AllDoctorsFelipeComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class VisitorsRoutingModule { }
