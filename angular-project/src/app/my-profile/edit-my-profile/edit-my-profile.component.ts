import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first, take } from 'rxjs/operators';
import { AccountService } from 'src/app/account/account.service';
import { PatientOfficesService } from 'src/app/patient-offices/patient-offices.service';
import { IDoctor, IRegisterDoctorDto } from 'src/app/shared/models/doctor';
import { MultipleSelectorModel } from 'src/app/shared/models/multiple-selector.model';
import { User } from 'src/app/shared/models/user';
import { MyProfileService } from '../my-profile.service';

@Component({
  selector: 'app-edit-my-profile',
  templateUrl: './edit-my-profile.component.html',
  styleUrls: ['./edit-my-profile.component.scss']
})
export class EditMyProfileComponent implements OnInit {
  user: User;
  model: IDoctor;
  registerForm: FormGroup;
  errors: string[] = [];
  nonSelectedSpecializations: MultipleSelectorModel[] = [];
  selectedSpecializations: MultipleSelectorModel[] = [];
  id: number;

  constructor(private fb: FormBuilder,
              private accountService: AccountService,
              private activatedRoute: ActivatedRoute,
              private myprofileService: MyProfileService,
              private patientOfficesService: PatientOfficesService,
              private router: Router)
              { this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user); }

  ngOnInit(): void {
    this.id = this.activatedRoute.snapshot.params['id'];

    this.activatedRoute.params.subscribe(params => {
    this.myprofileService.putGetDoctor(params.id).subscribe(putGetDTO => {
      this.model = putGetDTO.doctor;

      this.selectedSpecializations = putGetDTO.selectedSpecializations.map(specialization => {
        return {key: specialization.id, value: specialization.specializationName} as MultipleSelectorModel;
      });

      this.nonSelectedSpecializations = putGetDTO.nonSelectedSpecializations.map(specialization => {
        return {key: specialization.id, value: specialization.specializationName} as MultipleSelectorModel;
      });
    });
  });


    this.createRegisterForm();

    this.patientOfficesService.getDoctor(+this.activatedRoute.snapshot.paramMap.get('id'))
    .pipe(first())
    .subscribe(x => this.registerForm.patchValue(x));

}

createRegisterForm() {
  this.registerForm = this.fb.group({
    id: [this.id],
    applicationUserId: [this.user.userId],
    resume: [null],
    specializationsIds: [null],
    picture: ''
  });
}

onSubmit() {
  const specializationsIds = this.selectedSpecializations.map(value => value.key);
  this.registerForm.get('specializationsIds').setValue(specializationsIds);

  this.myprofileService.editDoctor1(this.id, this.registerForm.value).subscribe(() => {
    this.router.navigateByUrl('/myprofile');
  }, error => {
    console.log(error);
    this.errors = error.errors;
  });
}

onImageSelected(image){
  this.registerForm.get('picture').setValue(image);
}

}







