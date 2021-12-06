import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IMedicalrecord, IMedicalrecord1 } from 'src/app/shared/models/medicalrecord';
import { MyParams } from 'src/app/shared/models/myparams';
import { IPatient } from 'src/app/shared/models/patient';
import { AppointmentsService } from '../appointments.service';

@Component({
  selector: 'app-patient-detail',
  templateUrl: './patient-detail.component.html',
  styleUrls: ['./patient-detail.component.scss']
})
export class PatientDetailComponent implements OnInit {
  patient: IPatient;
  medicalrecords: IMedicalrecord[];
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  myParams = new MyParams();
  totalCount: number;

  sortOptions = [
    {name: 'Latest', value: 'dateDesc'},
    {name: 'Earliest', value: 'dateAsc'},
  ];

  constructor(private appointmentsService: AppointmentsService,
              private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadPatient();
    this.getMedicalrecords();
  }

  loadPatient() {
    return this.appointmentsService.getPatient(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
    this.patient = response;
    }, error => {
    console.log(error);
    });
    }

    getMedicalrecords() {
    this.appointmentsService
    .getMedicalrecordsForPatient(+this.activatedRoute.snapshot.paramMap.get('id'), this.myParams)
    .subscribe(response => {
      this.medicalrecords = response.data;
      this.myParams.page = response.page;
      this.myParams.pageCount = response.pageCount;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
    }
    );
  }

 onSearch() {
    this.myParams.query = this.searchTerm.nativeElement.value;
    this.getMedicalrecords();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getMedicalrecords();
  }

  onSortSelected(sort: string) {
    this.myParams.sort = sort;
    this.getMedicalrecords();
  }

  onPageChanged(event: any) {
    if (this.myParams.page !== event) {
      this.myParams.page = event;
      this.getMedicalrecords();
    }
}

}








