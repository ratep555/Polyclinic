import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientAppointmentsService } from 'src/app/patient-appointments/patient-appointments.service';
import { PatientOfficesService } from 'src/app/patient-offices/patient-offices.service';
import { IAppointmentSingle } from 'src/app/shared/models/appointment';
import { MedicalRecordsService } from '../medical-records.service';

@Component({
  selector: 'app-medical-record1-add',
  templateUrl: './medical-record1-add.component.html',
  styleUrls: ['./medical-record1-add.component.scss']
})
export class MedicalRecord1AddComponent implements OnInit {
  appointment: IAppointmentSingle;
  medicalrecordForm: FormGroup;
  errors: string[] = [];

  constructor(private patientappointmentservice: PatientAppointmentsService,
              private medicalrecordservice: MedicalRecordsService,
              private router: Router,
              private fb: FormBuilder,
              private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadAppointment();

    this.createMedicalRecordForm();
  }

  loadAppointment() {
    return this.patientappointmentservice.getAppointmentById(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
    this.appointment = response;
    }, error => {
    console.log(error);
    });
    }

    createMedicalRecordForm() {
      this.medicalrecordForm = this.fb.group({
      anamnesisDiagnosisTherapy: ['', [Validators.required]],
      created: ['', Validators.required]
    });
  }

  onSubmit() {
    this.medicalrecordservice.formData1.appointment1Id = this.appointment.id;
    this.medicalrecordservice.createMedicalRecord1(this.medicalrecordForm.value).subscribe(() => {
      this.router.navigateByUrl('appointments');
    },
    error => {
      console.log(error);
      this.errors = error.errors;
    });
  }

  }
