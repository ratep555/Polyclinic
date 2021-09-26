import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    NavBarComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    CollapseModule.forRoot()
  ],
  exports: [
    NavBarComponent
  ]
})
export class CoreModule { }
