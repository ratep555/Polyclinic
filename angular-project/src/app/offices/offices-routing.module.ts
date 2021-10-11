import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OfficesComponent } from './offices.component';
import { OfficeAddComponent } from './office-add/office-add.component';
import { OfficeEditComponent } from './office-edit/office-edit.component';

const routes: Routes = [
  {path: '', component: OfficesComponent},
  {path: 'addoffice', component: OfficeAddComponent},
  {path: 'editoffice/:id', component: OfficeEditComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class OfficesRoutingModule { }
