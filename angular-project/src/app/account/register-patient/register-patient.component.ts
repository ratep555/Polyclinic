import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register-patient',
  templateUrl: './register-patient.component.html',
  styleUrls: ['./register-patient.component.scss']
})
export class RegisterPatientComponent implements OnInit {
  patientForms: FormArray = this.fb.array([]);
  genderList = [];


  constructor(private accountService: AccountService,
              private router: Router,
              private fb: FormBuilder,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.accountService.getGenders()
    .subscribe(res => this.genderList = res as []);
    this.addStockForm();
  }

  addStockForm() {
    this.patientForms.push(this.fb.group({
      id: [0],
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      username: [null, [Validators.required]],
      street: [null, [Validators.required]],
      city: [null, [Validators.required]],
      country: [null, [Validators.required]],
      mBO: [null],
      dateOfBirth: ['', Validators.required],
      phoneNumber: [null, Validators.required],
      email: [null,
        [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
      ],
      password: ['', [Validators.required,
        Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', [Validators.required, this.compareValues('password')]],
      genderId: [0, Validators.min(1)],
    }));
  }

  compareValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value
        ? null : {isEqual: true};
    };
  }


  recordSubmit(fg: FormGroup) {
      this.accountService.registerPatient(fg.value).subscribe(
        (res: any) => {
          this.toastr.success('Confirmation link has been sent to your mail');
        }, error => {
            console.log(error);
          });
        }
}
