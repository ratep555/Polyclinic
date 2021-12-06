import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { IAppointment } from '../shared/models/appointment';
import { MyParams } from '../shared/models/myparams';
import { AppointmentsService } from './appointments.service';

@Component({
  selector: 'app-appointments',
  templateUrl: './appointments.component.html',
  styleUrls: ['./appointments.component.scss']
})
export class AppointmentsComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  @ViewChild('filter', {static: false}) filterTerm: ElementRef;
  appointments: IAppointment[];
  myParams = new MyParams();
  totalCount: number;
  currentDate: Date = new Date();

  sortOptions = [
    {name: 'Upcoming', value: 'city'},
    {name: 'All', value: 'all'},
    {name: 'Pending', value: 'pending'},
    {name: 'Booked', value: 'booked'},
    {name: 'Confirmed', value: 'confirmed'},
    {name: 'Cancelled', value: 'cancelled'}
  ];

  constructor(private appointmentsService: AppointmentsService,
              private  router: Router) { }

  ngOnInit(): void {
    this.getAppointments();
  }

  getAppointments() {
    this.appointmentsService.getAppointments(this.myParams)
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

  onFilter() {
    this.myParams.status = this.filterTerm.nativeElement.value;
    this.getAppointments();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getAppointments();
  }

  onPageChanged(event: any) {
    if (this.myParams.page !== event) {
      this.myParams.page = event;
      this.getAppointments();
    }
}

onSortSelected(sort: string) {
  this.myParams.sort = sort;
  this.getAppointments();
}
}
