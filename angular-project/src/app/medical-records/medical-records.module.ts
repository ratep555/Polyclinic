import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MedicalRecordsComponent } from './medical-records.component';
import { MedicallRecordAddComponent } from './medicall-record-add/medicall-record-add.component';
import { SharedModule } from '../shared/shared.module';
import { MedicalRecordsRoutingModule } from './medical-records-routing.module';
import { MedicalRecordPatientComponent } from './medical-record-patient/medical-record-patient.component';
import { MedicalRecord1AddComponent } from './medical-record1-add/medical-record1-add.component';
import { MedicalRecord1EditComponent } from './medical-record1-edit/medical-record1-edit.component';



@NgModule({
  declarations: [
    MedicalRecordsComponent,
    MedicallRecordAddComponent,
    MedicalRecordPatientComponent,
    MedicalRecord1AddComponent,
    MedicalRecord1EditComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    MedicalRecordsRoutingModule
  ]
})
export class MedicalRecordsModule { }
