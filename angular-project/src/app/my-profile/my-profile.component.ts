import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { take } from 'rxjs/operators';
import { AccountService } from '../account/account.service';
import { PatientOfficesService } from '../patient-offices/patient-offices.service';
import { IDoctor, IDoctorWithQualificationsAndOffices } from '../shared/models/doctor';
import { IOffice } from '../shared/models/office';
import { IProfessionalAssociation } from '../shared/models/professionalAssociation';
import { IPublication } from '../shared/models/publication';
import { ISpecialization } from '../shared/models/specialization';
import { ISubspecialization } from '../shared/models/subspecialization';
import { User } from '../shared/models/user';
import { MyProfileService } from './my-profile.service';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.scss']
})
export class MyProfileComponent implements OnInit {
  user: User;
  doctor: IDoctor;
  specializations: ISpecialization[];
  subspecializations: ISubspecialization[];
  associations: IProfessionalAssociation[];
  publications: IPublication[];
  offices: IOffice[];
  doctorWithQualificationsAndOffices: IDoctorWithQualificationsAndOffices;

  constructor(private accountService: AccountService,
              private myprofileService: MyProfileService,
              private patientOfficesService: PatientOfficesService,
              private router: Router)
  { this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user); }

  ngOnInit(): void {
     this.loadDoctor1();
     this.loadSpecializations();
     this.loadSubspecializations();
     this.loadProfessionalAssociations();
     this.loadPublications();
     this.loadOffices();
   // this.loadDoctorWithQualificatiopnsAndOffices();
  }

  loadDoctor1() {
    return this.patientOfficesService.getDoctor1(this.user.userId)
    .subscribe(response => {
    this.doctor = response;
    }, error => {
    console.log(error);
    });
    }

    loadSpecializations() {
      return this.myprofileService.getSpecializationsForDoctor(this.user.userId).subscribe(response => {
      this.specializations = response;
      }, error => {
      console.log(error);
      });
      }

    loadSubspecializations() {
      return this.myprofileService.getSubspecializationsForDoctor
      (this.user.userId).subscribe(response => {
      this.subspecializations = response;
      }, error => {
      console.log(error);
      });
      }

    loadProfessionalAssociations() {
      return this.myprofileService.getProfessionalAssociationsForDoctor
      (this.user.userId).subscribe(response => {
      this.associations = response;
      }, error => {
      console.log(error);
      });
      }

    loadPublications() {
      return this.myprofileService.getPublicationsForDoctor
      (this.user.userId).subscribe(response => {
      this.publications = response;
      }, error => {
      console.log(error);
      });
      }

    loadOffices() {
      return this.myprofileService.getOfficesForDoctor
      (this.user.userId).subscribe(response => {
      this.offices = response;
      }, error => {
      console.log(error);
      });
      }



  /* loadDoctorWithQualificatiopnsAndOffices() {
    return this.myprofileService.getDoctorWithQualificationsAndOffices(this.user.userId)
    .subscribe(response => {
    this.doctorWithQualificationsAndOffices = response;
    }, error => {
    console.log(error);
    });
    }
 */



}
