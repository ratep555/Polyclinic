import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyProfileComponent } from './my-profile.component';
import { EditMyProfileComponent } from './edit-my-profile/edit-my-profile.component';
import { SharedModule } from '../shared/shared.module';
import { MyProfileRoutingModule } from './my-profile-routing.module';



@NgModule({
  declarations: [
    MyProfileComponent,
    EditMyProfileComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    MyProfileRoutingModule
  ]
})
export class MyProfileModule { }
