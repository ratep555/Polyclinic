import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { INewOfficeToCreateOrEdit } from 'src/app/shared/models/office';
import { OfficesService } from '../offices.service';

@Component({
  selector: 'app-office-add',
  templateUrl: './office-add.component.html',
  styleUrls: ['./office-add.component.scss']
})
export class OfficeAddComponent implements OnInit {
  officeForm: FormGroup;
  errors: string[] = [];

  constructor(private officesService: OfficesService,
              private router: Router,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.createOfficeForm();
  }

  createOfficeForm() {
    this.officeForm = this.fb.group({
      street: ['', [Validators.required]],
      city: ['', [Validators.required]],
      country: ['', [Validators.required]],
      initialExaminationFee: ['', [Validators.required]],
      followUpExaminationFee: ['', [Validators.required]]
    });
  }

  onSubmit() {
    this.officesService.createOffice(this.officeForm.value).subscribe(() => {
      this.resetForm(this.officeForm);
      this.router.navigateByUrl('offices');
    },
    error => {
      console.log(error);
      this.errors = error.errors;
    });
  }

  resetForm(form: FormGroup) {
    form.reset();
    this.officesService.formData = new INewOfficeToCreateOrEdit();
  }

}
