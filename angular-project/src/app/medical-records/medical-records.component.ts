import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IMedicalrecord } from 'src/app/shared/models/medicalrecord';
import { MyParams } from 'src/app/shared/models/myparams';
import { IPatient } from 'src/app/shared/models/patient';
import { AppointmentsService } from '../appointments/appointments.service';
import { IOffice } from '../shared/models/office';
import { MedicalRecordsService } from './medical-records.service';

@Component({
  selector: 'app-medical-records',
  templateUrl: './medical-records.component.html',
  styleUrls: ['./medical-records.component.scss']
})
export class MedicalRecordsComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  @ViewChild('filter', {static: false}) filterTerm: ElementRef;
  medicalrecords: IMedicalrecord[];
  myParams = new MyParams();
  totalCount: number;
  offices: IOffice[];

  sortOptions = [
    {name: 'Latest', value: 'dateDesc'},
    {name: 'Earliest', value: 'dateAsc'},
  ];

  constructor(private medicalrecordService: MedicalRecordsService,
              private  router: Router) { }

  ngOnInit(): void {
    this.getMedicalRecords();
    this.getOffices();
  }

  getMedicalRecords() {
    this.medicalrecordService.getMedicalRecordsForAllDoctorPatients(this.myParams)
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

  getOffices() {
    this.medicalrecordService.getOfficesForDoctor().subscribe(response => {
    this.offices = response;
    }, error => {
    console.log(error);
    });
    }

  onOfficeSelected(officeId: number) {
    this.myParams.officeId = officeId;
    this.getMedicalRecords();
    }


  onSearch() {
    this.myParams.query = this.searchTerm.nativeElement.value;
    this.getMedicalRecords();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getMedicalRecords();
  }

  onReset1() {
    this.filterTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getMedicalRecords();
  }

  onSortSelected(sort: string) {
    this.myParams.sort = sort;
    this.getMedicalRecords();
  }

  onPageChanged(event: any) {
    if (this.myParams.page !== event) {
      this.myParams.page = event;
      this.getMedicalRecords();
    }
}


}
