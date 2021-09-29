import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SpecialtiesComponent } from './specialties.component';
import { SpecialtiesAddComponent } from './specialties-add/specialties-add.component';
import { SpecialtiesEditComponent } from './specialties-edit/specialties-edit.component';
import { SharedModule } from '../shared/shared.module';
import { SpecialtiesRoutingModule } from './specialties-routing.module';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  declarations: [
    SpecialtiesComponent,
    SpecialtiesAddComponent,
    SpecialtiesEditComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    SpecialtiesRoutingModule
  ]
})
export class SpecialtiesModule { }
