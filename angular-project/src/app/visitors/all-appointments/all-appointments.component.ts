import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { PatientOfficesService } from 'src/app/patient-offices/patient-offices.service';
import { IAppointment } from 'src/app/shared/models/appointment';
import { MyParams } from 'src/app/shared/models/myparams';
import { ISpecialization } from 'src/app/shared/models/specialization';
import { VisitorsService } from '../visitors.service';

@Component({
  selector: 'app-all-appointments',
  templateUrl: './all-appointments.component.html',
  styleUrls: ['./all-appointments.component.scss']
})
export class AllAppointmentsComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  @ViewChild('filter', {static: false}) filterTerm: ElementRef;
  appointments: IAppointment[];
  myParams = new MyParams();
  totalCount: number;
  specializations: ISpecialization[];

  sortOptions = [
    {name: 'Latest', value: 'city'},
    {name: 'Earliest', value: 'dateAsc'}
  ];

  constructor(private patientofficesService: PatientOfficesService,
              private visitorsService: VisitorsService,
              private  router: Router) { }

  ngOnInit(): void {
    this.getAppointments();
    this.getSpecializations();
  }

  getAppointments() {
    this.visitorsService.getAllAvailableAppointmentsForAllVisitors(this.myParams)
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

  getSpecializations() {
    this.patientofficesService.getSpecializations().subscribe(response => {
    this.specializations = response;
    }, error => {
    console.log(error);
    });
    }

    onSpecialisationSelected(specializationId: number) {
      this.myParams.specializationId = specializationId;
      this.getAppointments();
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

  onReset1() {
    this.filterTerm.nativeElement.value = '';
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


}
