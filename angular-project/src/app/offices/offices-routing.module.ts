import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OfficesComponent } from './offices.component';
import { OfficeAddComponent } from './office-add/office-add.component';
import { OfficeEditComponent } from './office-edit/office-edit.component';
import { AddOfficeComponent } from './add-office/add-office.component';
import { EditOfficeComponent } from './edit-office/edit-office.component';

const routes: Routes = [
  {path: '', component: OfficesComponent},
  {path: 'addoffice', component: OfficeAddComponent},
  {path: 'addoffice1', component: AddOfficeComponent},
  {path: 'editoffice/:id', component: OfficeEditComponent},
  {path: 'editoffice1/:id', component: EditOfficeComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class OfficesRoutingModule { }
