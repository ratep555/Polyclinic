import { HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IDoctor } from 'src/app/shared/models/doctor';
import { ISpecialization } from 'src/app/shared/models/specialization';
import { VisitorsService } from 'src/app/visitors/visitors.service';
import { FelipeService } from '../felipe.service';
import {Location} from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { IPaginationForDoctors } from 'src/app/shared/models/pagination';
import { MyParams } from 'src/app/shared/models/myparams';

@Component({
  selector: 'app-felipe-search2',
  templateUrl: './felipe-search2.component.html',
  styleUrls: ['./felipe-search2.component.scss']
})
export class FelipeSearch2Component implements OnInit {
  form: FormGroup;

  specializations: ISpecialization[];
  doctors: IDoctor[];
  initialFormValues: any;
  myParams = new MyParams();
  totalCount: number;
  page: number;
  pageCount = 2;
  count: number;

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
    this.felipeService.getDoctors(values)
    .subscribe(response => {
      this.doctors = response.data;
      this.page = response.page;
      // this.count = response.pageCount;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
    }
    );
  }

  clearForm(){
this.form.patchValue(this.initialFormValues);  }

private writeParametersInURL(){
  const queryStrings = [];
  const formValues = this.form.value;

  if (formValues.name){
    queryStrings.push(`name=${formValues.name}`);
  }
  queryStrings.push(`page=${this.page}`);
  queryStrings.push(`pageCount=${this.pageCount}`);


  this.location.replaceState('felipe/piki1', queryStrings.join('&'));
}

private readParametersFromURL(){
  this.activatedRoute.queryParams.subscribe(params => {
    const obj: any = {};
    if (params.name){
      obj.name = params.name;
    }
    this.form.patchValue(obj);
  });
}

onPageChanged(event: any) {
    this.page = event;
    this.writeParametersInURL();
    this.filterDoctors(this.form.value);
  }
}



