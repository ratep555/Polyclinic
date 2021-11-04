import { HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { IDoctor } from 'src/app/shared/models/doctor';
import { ISpecialization } from 'src/app/shared/models/specialization';
import { VisitorsService } from '../visitors.service';

@Component({
  selector: 'app-all-doctors-felipe',
  templateUrl: './all-doctors-felipe.component.html',
  styleUrls: ['./all-doctors-felipe.component.scss']
})
export class AllDoctorsFelipeComponent implements OnInit {
  form: FormGroup;
  doctors: IDoctor[];
  specializations: ISpecialization[];
  initialFormValues: any;


  constructor(private formBuilder: FormBuilder,
              private visitorsService: VisitorsService,
              private activatedRoute: ActivatedRoute) { }

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

    this.filterDoctors(this.form.value);

    this.form.valueChanges
      .subscribe(values => {
        this.filterDoctors(values);
      });
  }

  filterDoctors(values: any){
    this.visitorsService.filter(values).subscribe((response: HttpResponse<IDoctor[]>) => {
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







