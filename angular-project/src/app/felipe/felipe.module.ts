import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FelipeSearchComponent } from './felipe-search/felipe-search.component';
import { SharedModule } from '../shared/shared.module';
import { FelipeRoutingModule } from './felipe-routing.module';
import { FelipeListComponent } from './felipe-list/felipe-list.component';



@NgModule({
  declarations: [
    FelipeSearchComponent,
    FelipeListComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FelipeRoutingModule
  ]
})
export class FelipeModule { }
