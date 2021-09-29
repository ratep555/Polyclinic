import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { SpecialtiesComponent } from './specialties/specialties.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'specialties',
  loadChildren: () => import('./specialties/specialties.module').then(mod => mod.SpecialtiesModule)},
  {path: 'account', loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule)},
  {path: '**', redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
