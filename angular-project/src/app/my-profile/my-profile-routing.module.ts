import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MyProfileComponent } from './my-profile.component';
import { EditMyProfileComponent } from './edit-my-profile/edit-my-profile.component';

const routes: Routes = [
  {path: '', component: MyProfileComponent},
  {path: 'edit/:id', component: EditMyProfileComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class MyProfileRoutingModule { }
