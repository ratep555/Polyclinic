import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FelipeSearchComponent } from './felipe-search/felipe-search.component';
import { FelipeSearch1Component } from './felipe-search1/felipe-search1.component';
import { FelipeSearch2Component } from './felipe-search2/felipe-search2.component';

const routes: Routes = [
  {path: '', component: FelipeSearchComponent},
  {path: 'piki', component: FelipeSearch1Component},
  {path: 'piki1', component: FelipeSearch2Component}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class FelipeRoutingModule { }
