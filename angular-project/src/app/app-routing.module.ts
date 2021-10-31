import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { IsAdminGuard } from './core/guards/is-admin.guard';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { HomeComponent } from './home/home.component';
import { SpecialtiesComponent } from './specialties/specialties.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'specialties',
  loadChildren: () => import('./specialties/specialties.module').then(mod => mod.SpecialtiesModule)},
  {path: 'myprofile', canActivate: [AuthGuard],
  loadChildren: () => import('./my-profile/my-profile.module').then(mod => mod.MyProfileModule)},
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
  {path: 'visitors', canActivate: [AuthGuard],
   loadChildren: () => import('./visitors/visitors.module').then(mod => mod.VisitorsModule),
  data: {roleName: 'Admin'}},
  {path: 'felipe',
   loadChildren: () => import('./felipe/felipe.module').then(mod => mod.FelipeModule)},
  {path: 'account', loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule)},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
