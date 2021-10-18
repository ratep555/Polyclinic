import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { INewOfficeToCreate, INewOfficeToEdit } from 'src/app/shared/models/office';

@Component({
  selector: 'app-form-office',
  templateUrl: './form-office.component.html',
  styleUrls: ['./form-office.component.scss']
})
export class FormOfficeComponent implements OnInit {
  @Output() SaveChangesy = new EventEmitter<INewOfficeToCreate>();
  @Input() model: INewOfficeToEdit;
  form: FormGroup;
  errors: string[] = [];

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      street: ['', [Validators.required]],
      city: ['', [Validators.required]],
      country: ['', [Validators.required]],
      initialExaminationFee: ['', [Validators.required]],
      followUpExaminationFee: ['', [Validators.required]]
    });
  }

  saveChanges() {
    this.SaveChangesy.emit(this.form.value);
     }

}





