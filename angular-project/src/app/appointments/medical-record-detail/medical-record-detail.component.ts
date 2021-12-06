import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IMedicalrecord, IMedicalrecord1 } from 'src/app/shared/models/medicalrecord';
import { AppointmentsService } from '../appointments.service';

@Component({
  selector: 'app-medical-record-detail',
  templateUrl: './medical-record-detail.component.html',
  styleUrls: ['./medical-record-detail.component.scss']
})
export class MedicalRecordDetailComponent implements OnInit {
  medicalrecord: IMedicalrecord;

  constructor(private appointmentsService: AppointmentsService,
              private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMedicalRecord();
  }

  loadMedicalRecord() {
    return this.appointmentsService.getMedicalRecord(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
    this.medicalrecord = response;
    }, error => {
    console.log(error);
    });
    }


}
