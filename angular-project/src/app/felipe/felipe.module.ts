import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FelipeSearchComponent } from './felipe-search/felipe-search.component';
import { SharedModule } from '../shared/shared.module';
import { FelipeRoutingModule } from './felipe-routing.module';
import { FelipeListComponent } from './felipe-list/felipe-list.component';
import { GenderAddEditComponent } from './gender-add-edit/gender-add-edit.component';
import { GenderListComponent } from './gender-list/gender-list.component';
import { FelipeSearch1Component } from './felipe-search1/felipe-search1.component';
import { FelipeSearch2Component } from './felipe-search2/felipe-search2.component';



@NgModule({
  declarations: [
    FelipeSearchComponent,
    FelipeListComponent,
    GenderAddEditComponent,
    GenderListComponent,
    FelipeSearch1Component,
    FelipeSearch2Component
  ],
  imports: [
    CommonModule,
    SharedModule,
    FelipeRoutingModule
  ]
})
export class FelipeModule { }
