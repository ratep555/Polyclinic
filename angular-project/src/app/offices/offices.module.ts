import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OfficesComponent } from './offices.component';
import { OfficeAddComponent } from './office-add/office-add.component';
import { OfficeEditComponent } from './office-edit/office-edit.component';
import { SharedModule } from '../shared/shared.module';
import { OfficesRoutingModule } from './offices-routing.module';



@NgModule({
  declarations: [
    OfficesComponent,
    OfficeAddComponent,
    OfficeEditComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    OfficesRoutingModule
  ]
})
export class OfficesModule { }
