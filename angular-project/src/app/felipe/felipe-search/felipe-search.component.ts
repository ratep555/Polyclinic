import { HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IDoctor } from 'src/app/shared/models/doctor';
import { ISpecialization } from 'src/app/shared/models/specialization';
import { VisitorsService } from 'src/app/visitors/visitors.service';
import { FelipeService } from '../felipe.service';

@Component({
  selector: 'app-felipe-search',
  templateUrl: './felipe-search.component.html',
  styleUrls: ['./felipe-search.component.scss']
})
export class FelipeSearchComponent implements OnInit {

  form: FormGroup;

  specializations: ISpecialization[];
  doctors: IDoctor[];
  initialFormValues: any;

  constructor(private formBuilder: FormBuilder,
              private felipeService: FelipeService,
              private visitorsService: VisitorsService) { }


  ngOnInit(): void {

    this.form = this.formBuilder.group({
      query: '',
      specializationId: 0,
    });

    this.initialFormValues = this.form.value;

    this.visitorsService.getSpecializations
    ().subscribe(specializations => {
      this.specializations = specializations;

      this.filterDoctors(this.form.value);

      this.form.valueChanges
      .subscribe(values => {
        this.filterDoctors(values);
      });
    });

  }

  filterDoctors(values: any){
    this.felipeService.filter(values).subscribe((response: HttpResponse<IDoctor[]>) => {
      this.doctors = response.body;
    });
  }

  clearForm(){
this.form.patchValue(this.initialFormValues);  }


}
