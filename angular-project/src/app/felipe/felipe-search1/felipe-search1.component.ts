import { HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IDoctor } from 'src/app/shared/models/doctor';
import { ISpecialization } from 'src/app/shared/models/specialization';
import { VisitorsService } from 'src/app/visitors/visitors.service';
import { FelipeService } from '../felipe.service';
import {Location} from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-felipe-search1',
  templateUrl: './felipe-search1.component.html',
  styleUrls: ['./felipe-search1.component.scss']
})
export class FelipeSearch1Component implements OnInit {
  form: FormGroup;

  specializations: ISpecialization[];
  doctors: IDoctor[];
  initialFormValues: any;

  constructor(private formBuilder: FormBuilder,
              private felipeService: FelipeService,
              private visitorsService: VisitorsService,
              private activatedRoute: ActivatedRoute,
              private location: Location) { }


  ngOnInit(): void {

    this.form = this.formBuilder.group({
      name: ''
    });

    this.initialFormValues = this.form.value;
    this.readParametersFromURL();

    this.visitorsService.getSpecializations
    ().subscribe(specializations => {
      this.specializations = specializations;

      this.filterDoctors(this.form.value);

      this.form.valueChanges
      .subscribe(values => {
        this.filterDoctors(values);
        this.writeParametersInURL();
      },
      error => {
        console.log(error);
      });
    });

  }

  filterDoctors(values: any){
    this.felipeService.filter1(values).subscribe((response: HttpResponse<IDoctor[]>) => {
      this.doctors = response.body;
    },
    error => {
      console.log(error);
    });
  }

  clearForm(){
this.form.patchValue(this.initialFormValues);  }

private writeParametersInURL(){
  const queryStrings = [];
  const formValues = this.form.value;

  if (formValues.name){
    queryStrings.push(`name=${formValues.name}`);
  }

  this.location.replaceState('felipe/piki', queryStrings.join('&'));
}

private readParametersFromURL(){
  // hover over queryparams
  this.activatedRoute.queryParams.subscribe(params => {
    const obj: any = {};
   // if there is a title in querystring
    if (params.name){
      obj.name = params.name;
    }

    this.form.patchValue(obj);
  });
}


}
