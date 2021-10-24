import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MedicalRecordsComponent } from './medical-records.component';
import { MedicallRecordAddComponent } from './medicall-record-add/medicall-record-add.component';
import { MedicalRecordPatientComponent } from './medical-record-patient/medical-record-patient.component';

const routes: Routes = [
  {path: '', component: MedicalRecordsComponent},
  {path: 'patientrecords', component: MedicalRecordPatientComponent},
  {path: 'addrecord/:id', component: MedicallRecordAddComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class MedicalRecordsRoutingModule { }
