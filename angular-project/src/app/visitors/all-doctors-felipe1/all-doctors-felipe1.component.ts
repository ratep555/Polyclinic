import { HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { IDoctor } from 'src/app/shared/models/doctor';
import { ISpecialization } from 'src/app/shared/models/specialization';
import { UserParams } from 'src/app/shared/models/userParams';
import { VisitorsService } from '../visitors.service';

@Component({
  selector: 'app-all-doctors-felipe1',
  templateUrl: './all-doctors-felipe1.component.html',
  styleUrls: ['./all-doctors-felipe1.component.scss']
})
export class AllDoctorsFelipe1Component implements OnInit {
  doctors: IDoctor[];
  userParams: UserParams;
  totalCount: number;
  specializations: ISpecialization[];
  form: FormGroup;
  initialFormValues: any;

  constructor(private formBuilder: FormBuilder,
              private visitorsService: VisitorsService,
              private  router: Router) {
    this.userParams = this.visitorsService.getUserParams();
     }

     ngOnInit(): void {
      this.form = this.formBuilder.group({
        query: '',
        specializationId: 0
      });

      this.initialFormValues = this.form.value;

      this.visitorsService.getSpecializations
      ().subscribe(specializations => {
        this.specializations = specializations;
      });

      this.getDoctors(this.form.value);

      this.form.valueChanges
        .subscribe(values => {
          this.getDoctors(values);
        });
    }

  getDoctors(values: any) {
    this.visitorsService.setUserParams(this.userParams);
    this.visitorsService.filter1(values)
    .subscribe((response: HttpResponse<IDoctor[]>) => {
      this.doctors = response.body;

  }, error => {
      console.log(error);
    }
    );
  }

  clearForm(){
    this.form.patchValue(this.initialFormValues);
    }


}










