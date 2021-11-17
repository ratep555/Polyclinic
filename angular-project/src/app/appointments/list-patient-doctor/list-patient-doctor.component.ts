import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { IPatient } from 'src/app/shared/models/patient';
import { UserParams } from 'src/app/shared/models/userParams';
import { AppointmentsService } from '../appointments.service';

@Component({
  selector: 'app-list-patient-doctor',
  templateUrl: './list-patient-doctor.component.html',
  styleUrls: ['./list-patient-doctor.component.scss']
})
export class ListPatientDoctorComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  patients: IPatient[];
  userParams: UserParams;
  totalCount: number;

  sortOptions = [
    {name: 'Sort Alphabetical by Name', value: 'city'},
    {name: 'Sort Alphabetical by Name Descending', value: 'nameDesc'},
  ];

  constructor(private appointmentsService: AppointmentsService,
              private  router: Router) {
              this.userParams = this.appointmentsService.getUserParams();

               }

  ngOnInit(): void {
    this.getPatients();
  }

  getPatients() {
    this.appointmentsService.setUserParams(this.userParams);
    this.appointmentsService.getDoctorPatients(this.userParams)
    .subscribe(response => {
      this.patients = response.data;
      this.userParams.page = response.page;
      this.userParams.pageCount = response.pageCount;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
    }
    );
  }

  resetFilters() {
    this.userParams = this.appointmentsService.resetUserParams();
    this.getPatients();
  }

  onSearch() {
    this.userParams.query = this.searchTerm.nativeElement.value;
    this.getPatients();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.userParams = this.appointmentsService.resetUserParams();
    this.getPatients();
  }

  onSortSelected(sort: string) {
    this.userParams.sort = sort;
    this.getPatients();
  }

  onPageChanged(event: any) {
    if (this.userParams.page !== event) {
      this.userParams.page = event;
      this.appointmentsService.setUserParams(this.userParams);
      this.getPatients();
    }
}


}

