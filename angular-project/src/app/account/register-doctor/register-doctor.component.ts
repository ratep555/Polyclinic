import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MultipleSelectorModel } from 'src/app/shared/models/multiple-selector.model';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register-doctor',
  templateUrl: './register-doctor.component.html',
  styleUrls: ['./register-doctor.component.scss']
})
export class RegisterDoctorComponent implements OnInit {
  registerForm: FormGroup;
  errors: string[] = [];
  nonSelectedSpecializations: MultipleSelectorModel[] = [];
  selectedSpecializations: MultipleSelectorModel[] = [];

  nonSelectedGenres: MultipleSelectorModel[] = [
    {key: 1, value: 'Drama'},
    {key: 2, value: 'Action'},
    {key: 3, value: 'Comedy'},
  ];

  selectedGenres: MultipleSelectorModel[] = [];

  nonSelectedMovieTheaters: MultipleSelectorModel[] = [
    {key: 1, value: 'Agora'},
    {key: 2, value: 'Sambil'},
    {key: 3, value: 'Megacentro'},
  ];

  selectedMovieTheaters: MultipleSelectorModel[] = [];


  constructor(private fb: FormBuilder, private accountService: AccountService, private router: Router) { }

  ngOnInit() {
    this.accountService.getSpecializations().subscribe(response => {
      this.nonSelectedSpecializations = response.map(specialization => {
        return  {key: specialization.id, value: specialization.specializationName} as MultipleSelectorModel;
      });
    });

    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      username: [null, [Validators.required]],
      startedPracticing: [null],
      resume: [null],
      specializationsIds: [null],
      email: [null,
        [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
      ],
      password: ['', [Validators.required,
        Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', [Validators.required, this.compareValues('password')]]
    });
  }

  compareValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value
        ? null : {isEqual: true};
    };
  }

  onSubmit() {
    const specializationsIds = this.selectedSpecializations.map(value => value.key);
    this.registerForm.get('specializationsIds').setValue(specializationsIds);

    this.accountService.registerDoctor2(this.registerForm.value).subscribe(response => {
      this.router.navigateByUrl('/');
    }, error => {
      console.log(error);
      this.errors = error.errors;
    });
  }

}
