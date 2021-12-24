import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { SharedModule } from '../shared/shared.module';
import { AccountRoutingModule } from './account-routing.module';
import { RegisterDoctorComponent } from './register-doctor/register-doctor.component';
import { RegisterPatientComponent } from './register-patient/register-patient.component';
import { EmailConfirmationComponent } from './email-confirmation/email-confirmation.component';
import { EmailNotificationComponent } from './email-notification/email-notification.component';



@NgModule({
  declarations: [
    RegisterComponent,
    LoginComponent,
    RegisterDoctorComponent,
    RegisterPatientComponent,
    EmailConfirmationComponent,
    EmailNotificationComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    AccountRoutingModule
  ]
})
export class AccountModule { }
