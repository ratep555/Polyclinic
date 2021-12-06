import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CoordinatesMap } from 'src/app/shared/models/coordinate';
import { IOffice } from 'src/app/shared/models/office';
import { OfficesService } from '../offices.service';

@Component({
  selector: 'app-add-office1',
  templateUrl: './add-office1.component.html',
  styleUrls: ['./add-office1.component.scss']
})
export class AddOffice1Component implements OnInit {
  officeForm: FormGroup;
  hospitalList = [];
  initialCoordinates: CoordinatesMap[] = [];
  model: IOffice;

  constructor(public officesService: OfficesService,
              private router: Router,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.officesService.getHospitals()
    .subscribe(res => this.hospitalList = res as []);

    this.createOfficeForm();
  }

  createOfficeForm() {
    this.officeForm = this.fb.group({
      street: ['', [Validators.required]],
      city: ['', [Validators.required]],
      country: ['', [Validators.required]],
     /*  initialExaminationFee: [''],
      followUpExaminationFee: [''], */
      description: ['', [Validators.required]],
      longitude: ['', [Validators.required]],
      latitude: ['', [Validators.required]],
      hospitalAffiliationId: [null],
      picture: ''
    });
  }

  onSubmit() {
    this.officesService.createOffice2(this.officeForm.value).subscribe(() => {
      this.router.navigateByUrl('offices');
    },
    error => {
      console.log(error);
    });
  }

  onImageSelected(image){
    this.officeForm.get('picture').setValue(image);
  }

  onSelectedLocation(coordinates: CoordinatesMap) {
    this.officeForm.patchValue(coordinates);

 }

}
