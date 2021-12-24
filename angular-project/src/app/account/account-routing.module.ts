import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { RegisterDoctorComponent } from './register-doctor/register-doctor.component';
import { RegisterPatientComponent } from './register-patient/register-patient.component';
import { EmailConfirmationComponent } from './email-confirmation/email-confirmation.component';
import { EmailNotificationComponent } from './email-notification/email-notification.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'register-doctor', component: RegisterDoctorComponent},
  {path: 'register-patient', component: RegisterPatientComponent},
  {path: 'email-confirmation', component: EmailConfirmationComponent},
  {path: 'email-notification', component: EmailNotificationComponent}
];


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
