import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { IAppointment } from '../shared/models/appointment';
import { MyParams } from '../shared/models/myparams';
import { PatientAppointmentsService } from './patient-appointments.service';
import Swal from 'sweetalert2/dist/sweetalert2.js';

@Component({
  selector: 'app-patient-appointments',
  templateUrl: './patient-appointments.component.html',
  styleUrls: ['./patient-appointments.component.scss']
})
export class PatientAppointmentsComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  appointments: IAppointment[];
  myParams = new MyParams();
  totalCount: number;

  sortOptions = [
    {name: 'Sort by Start Date Ascending', value: 'dateAsc'},
    {name: 'Sort by Start Date Descending', value: 'dateDesc'},
    {name: 'Sort by End Date Ascending', value: 'dateAscEnd'},
    {name: 'Sort by End Date Descending', value: 'dateDescEnd'}
  ];

  constructor(private patientappointmentsService: PatientAppointmentsService,
              private  router: Router) { }

  ngOnInit(): void {
    this.getAppointments();
  }

  getAppointments() {
    this.patientappointmentsService.getAppointmentsForPatientView(this.myParams)
    .subscribe(response => {
      this.appointments = response.data;
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
    this.getAppointments();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getAppointments();
  }

  onSortSelected(sort: string) {
    this.myParams.sort = sort;
    this.getAppointments();
  }

  onPageChanged(event: any) {
    if (this.myParams.page !== event) {
      this.myParams.page = event;
      this.getAppointments();
    }
}

bookAppointment(id: number) {
  Swal.fire({
    title: 'Are you sure want to book this appointment?',
    text: 'You can cancell it until it is confirmed by the practicing doctor.',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Yes, book it!',
    confirmButtonColor: '#DD6B55',
    cancelButtonText: 'No, forget about it'
  }).then((result) => {
    if (result.value) {
    this.patientappointmentsService.bookAppointment(id)
      .subscribe(
        res => {
          this.getAppointments();
        },
        err => { console.log(err);
        });
      }
    });
    }
}
