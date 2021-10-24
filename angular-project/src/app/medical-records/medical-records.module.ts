import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MedicalRecordsComponent } from './medical-records.component';
import { MedicallRecordAddComponent } from './medicall-record-add/medicall-record-add.component';
import { SharedModule } from '../shared/shared.module';
import { MedicalRecordsRoutingModule } from './medical-records-routing.module';
import { MedicalRecordPatientComponent } from './medical-record-patient/medical-record-patient.component';



@NgModule({
  declarations: [
    MedicalRecordsComponent,
    MedicallRecordAddComponent,
    MedicalRecordPatientComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    MedicalRecordsRoutingModule
  ]
})
export class MedicalRecordsModule { }
