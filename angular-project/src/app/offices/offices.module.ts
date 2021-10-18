import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OfficesComponent } from './offices.component';
import { OfficeAddComponent } from './office-add/office-add.component';
import { OfficeEditComponent } from './office-edit/office-edit.component';
import { SharedModule } from '../shared/shared.module';
import { OfficesRoutingModule } from './offices-routing.module';
import { FormOfficeComponent } from './form-office/form-office.component';
import { AddOfficeComponent } from './add-office/add-office.component';
import { EditOfficeComponent } from './edit-office/edit-office.component';



@NgModule({
  declarations: [
    OfficesComponent,
    OfficeAddComponent,
    OfficeEditComponent,
    FormOfficeComponent,
    AddOfficeComponent,
    EditOfficeComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    OfficesRoutingModule
  ]
})
export class OfficesModule { }
