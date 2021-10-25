import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { SpecialtiesComponent } from './specialties/specialties.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'specialties',
  loadChildren: () => import('./specialties/specialties.module').then(mod => mod.SpecialtiesModule)},
  {path: 'offices', canActivate: [AuthGuard],
  loadChildren: () => import('./offices/offices.module').then(mod => mod.OfficesModule)},
  {path: 'appointments', canActivate: [AuthGuard],
  loadChildren: () => import('./appointments/appointments.module').then(mod => mod.AppointmentsModule)},
  {path: 'medicalrecords', canActivate: [AuthGuard],
  loadChildren: () => import('./medical-records/medical-records.module').then(mod => mod.MedicalRecordsModule)},
  {path: 'patient-offices', canActivate: [AuthGuard],
  loadChildren: () => import('./patient-offices/patient-offices.module').then(mod => mod.PatientOfficesModule)},
  {path: 'patient-appointments', canActivate: [AuthGuard],
  loadChildren: () => import('./patient-appointments/patient-appointments.module').then(mod => mod.PatientAppointmentsModule)},
  {path: 'visitors', loadChildren: () => import('./visitors/visitors.module').then(mod => mod.VisitorsModule)},
  {path: 'account', loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule)},
  {path: '**', redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
