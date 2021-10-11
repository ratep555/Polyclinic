import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { PatientOfficesComponent } from './patient-offices.component';

const routes: Routes = [
  {path: '', component: PatientOfficesComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class PatientOfficesRoutingModule { }
