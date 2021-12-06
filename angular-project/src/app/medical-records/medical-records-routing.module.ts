import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MedicalRecordsComponent } from './medical-records.component';
import { MedicallRecordAddComponent } from './medicall-record-add/medicall-record-add.component';
import { MedicalRecordPatientComponent } from './medical-record-patient/medical-record-patient.component';
import { MedicalRecord1AddComponent } from './medical-record1-add/medical-record1-add.component';
import { MedicalRecord1EditComponent } from './medical-record1-edit/medical-record1-edit.component';

const routes: Routes = [
  {path: '', component: MedicalRecordsComponent},
  {path: 'patientrecords', component: MedicalRecordPatientComponent},
  {path: 'addrecord/:id', component: MedicallRecordAddComponent},
  {path: 'addrecord1/:id', component: MedicalRecord1AddComponent},
  {path: 'editrecord1/:id', component: MedicalRecord1EditComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class MedicalRecordsRoutingModule { }
