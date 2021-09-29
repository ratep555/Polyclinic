import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SpecialtiesComponent } from './specialties.component';
import { SpecialtiesAddComponent } from './specialties-add/specialties-add.component';
import { SpecialtiesEditComponent } from './specialties-edit/specialties-edit.component';

const routes: Routes = [
  {path: '', component: SpecialtiesComponent},
  {path: 'addspecialty', component: SpecialtiesAddComponent},
  {path: 'editspecialty/:id', component: SpecialtiesEditComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class SpecialtiesRoutingModule { }
