import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IAppointment } from 'src/app/shared/models/appointment';
import { MyParams } from 'src/app/shared/models/myparams';
import { IOffice } from 'src/app/shared/models/office';
import { PatientOfficesService } from '../patient-offices.service';

@Component({
  selector: 'app-patient-office-detail',
  templateUrl: './patient-office-detail.component.html',
  styleUrls: ['./patient-office-detail.component.scss']
})
export class PatientOfficeDetailComponent implements OnInit {
  office: IOffice;
  appointments: IAppointment[];
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  myParams = new MyParams();
  totalCount: number;

  constructor(private activatedRoute: ActivatedRoute,
              private patientOfficesService: PatientOfficesService) { }

  ngOnInit(): void {

    this.loadOffice();
    this.getAppointments();
  }

  getAppointments() {
    this.patientOfficesService
    .getAvailableAppointmentsForOfficeForPatient(+this.activatedRoute.snapshot.paramMap.get('id'), this.myParams)
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

  loadOffice() {
    return this.patientOfficesService.getOffice(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
    this.office = response;
    }, error => {
    console.log(error);
    });
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


}
