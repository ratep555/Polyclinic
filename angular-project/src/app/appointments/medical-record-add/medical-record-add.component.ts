import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { INewMedicalrecordToCreate } from 'src/app/shared/models/medicalrecord';
import { IPatient } from 'src/app/shared/models/patient';
import { AppointmentsService } from '../appointments.service';

@Component({
  selector: 'app-medical-record-add',
  templateUrl: './medical-record-add.component.html',
  styleUrls: ['./medical-record-add.component.scss']
})
export class MedicalRecordAddComponent implements OnInit {
  patient: IPatient;
  officeList = [];
  medicalrecordForm: FormGroup;
  errors: string[] = [];

  constructor(private appointmentsService: AppointmentsService,
              private router: Router,
              private fb: FormBuilder,
              private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadPatient();
    this.appointmentsService.getOffices()
    .subscribe(res => this.officeList = res as []);
    this.createMedicalRecordForm();
  }

  loadPatient() {
    return this.appointmentsService.getPatient(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
    this.patient = response;
    }, error => {
    console.log(error);
    });
    }

    createMedicalRecordForm() {
      this.medicalrecordForm = this.fb.group({
      anamnesisDiagnosisTherapy: ['', [Validators.required]],
      office1Id: [0, Validators.min(1)],
      created: ['', Validators.required]
    });
  }

  onSubmit() {
    this.appointmentsService.formData1.patient1Id = this.patient.id;
    this.appointmentsService.createMedicalRecord(this.medicalrecordForm.value).subscribe(() => {
      this.resetForm(this.medicalrecordForm);
      this.router.navigateByUrl('appointments');
    },
    error => {
      console.log(error);
      this.errors = error.errors;
    });
  }

  resetForm(form: FormGroup) {
    form.reset();
    this.appointmentsService.formData1 = new INewMedicalrecordToCreate();
  }
  }







