import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IAppointment } from 'src/app/shared/models/appointment';
import { CoordinatesMap, CoordinatesMapWithMessage } from 'src/app/shared/models/coordinate';
import { MyParams } from 'src/app/shared/models/myparams';
import { IOffice } from 'src/app/shared/models/office';
import { PatientOfficesService } from '../patient-offices.service';

@Component({
  selector: 'app-office-info',
  templateUrl: './office-info.component.html',
  styleUrls: ['./office-info.component.scss']
})
export class OfficeInfoComponent implements OnInit {
  office: IOffice;
  // coordinates: CoordinatesMap[] = [];
  coordinates: CoordinatesMapWithMessage[] = [];
  appointments: IAppointment[];
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  myParams = new MyParams();
  totalCount: number;

  sortOptions = [
    {name: 'Sort by Date Ascending', value: 'dateAsc'},
    {name: 'Sort by Date Descending', value: 'dateDesc'},
  ];

  constructor(private patientofficesService: PatientOfficesService,
              private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
     this.activatedRoute.params.subscribe((params) => {
      this.patientofficesService.getOffice1(params.id).subscribe((office) => {
        console.log(office);
        this.office = office;
        this.coordinates = [{latitude: office.latitude, longitude: office.longitude, message: office.street}];
        console.log(this.coordinates);
      });
    });
     this.getAppointments();
  }

  getAppointments() {
    this.patientofficesService
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

