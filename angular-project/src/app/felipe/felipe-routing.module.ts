import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FelipeSearchComponent } from './felipe-search/felipe-search.component';

const routes: Routes = [
  {path: '', component: FelipeSearchComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class FelipeRoutingModule { }
