import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { AccountService } from 'src/app/account/account.service';
import { MyProfileService } from 'src/app/my-profile/my-profile.service';
import { PatientOfficesService } from 'src/app/patient-offices/patient-offices.service';
import { IDoctor } from 'src/app/shared/models/doctor';
import { ISpecialty } from 'src/app/shared/models/specialty';
import { User } from 'src/app/shared/models/user';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  currentUser$: Observable<User>;
  doctor: IDoctor;
  user: User;

  isCollapsed = true;


  constructor(public accountService: AccountService,
              private patientofficeservice: PatientOfficesService,
              private router: Router)
  {      this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
   // this.loadDoctor1();

}

loadDoctor() {
  return this.patientofficeservice.getDoctor(this.user.userId)
  .subscribe(response => {
  this.doctor = response;
  }, error => {
  console.log(error);
  });
  }

loadDoctor1() {
  return this.patientofficeservice.getDoctor1(this.user.userId)
  .subscribe(response => {
  this.doctor = response;
  }, error => {
  console.log(error);
  });
  }

logout() {
  this.accountService.logout();
}
}
