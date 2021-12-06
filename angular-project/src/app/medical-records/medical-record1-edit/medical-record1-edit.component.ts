import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AppointmentsService } from 'src/app/appointments/appointments.service';
import { IMedicalrecord, INewMedicalrecordToCreate, INewMedicalrecordToCreate1 } from 'src/app/shared/models/medicalrecord';
import { MedicalRecordsService } from '../medical-records.service';

@Component({
  selector: 'app-medical-record1-edit',
  templateUrl: './medical-record1-edit.component.html',
  styleUrls: ['./medical-record1-edit.component.scss']
})
export class MedicalRecord1EditComponent implements OnInit {
  recordForms: FormArray = this.fb.array([]);
  medicalrecord: IMedicalrecord;
  id: number;

  constructor(private fb: FormBuilder,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private medicalrecordservice: MedicalRecordsService,
              private appointmentsService: AppointmentsService) { }

  ngOnInit(): void {
    this.id = this.activatedRoute.snapshot.params['id'];

    this.loadMedicalRecord();

    this.appointmentsService.getMedicalRecordForEditing(this.id).subscribe(
    (record: INewMedicalrecordToCreate1) => {
    this.recordForms.push(this.fb.group({
      appointment1Id: [this.id],
      anamnesisDiagnosisTherapy: [record.anamnesisDiagnosisTherapy, [Validators.required]],
      created: [new Date(record.created), Validators.required],
      }));
    });
  }

  loadMedicalRecord() {
    return this.appointmentsService.getMedicalRecord(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
    this.medicalrecord = response;
    }, error => {
    console.log(error);
    });
    }

  recordSubmit(fg: FormGroup) {
    this.medicalrecordservice.formData1.appointment1Id = this.medicalrecord.appointment1Id;
    this.medicalrecordservice.updateMedicalRecord(fg.value).subscribe(
      (res: any) => {
        this.router.navigateByUrl('medicalrecords');
      }, error => {
          console.log(error);
        });
      }

}
