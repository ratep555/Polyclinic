import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { PatientOfficesComponent } from './patient-offices.component';
import { PatientOfficeDetailComponent } from './patient-office-detail/patient-office-detail.component';
import { DoctorDetailComponent } from './doctor-detail/doctor-detail.component';
import { OfficeInfoComponent } from './office-info/office-info.component';
import { PatientOfficesTestComponent } from './patient-offices-test/patient-offices-test.component';

const routes: Routes = [
  {path: '', component: PatientOfficesComponent},
  {path: 'test', component: PatientOfficesTestComponent},
  {path: ':id', component: PatientOfficeDetailComponent},
  {path: 'doctor/:id', component: DoctorDetailComponent},
  {path: 'office/:id', component: OfficeInfoComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class PatientOfficesRoutingModule { }
