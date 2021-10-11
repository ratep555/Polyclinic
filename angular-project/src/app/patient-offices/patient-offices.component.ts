import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { MyParams } from '../shared/models/myparams';
import { IOffice } from '../shared/models/office';
import { ISpecialization } from '../shared/models/specialization';
import { PatientOfficesService } from './patient-offices.service';

@Component({
  selector: 'app-patient-offices',
  templateUrl: './patient-offices.component.html',
  styleUrls: ['./patient-offices.component.scss']
})
export class PatientOfficesComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  @ViewChild('filter', {static: false}) filterTerm: ElementRef;
  offices: IOffice[];
  myParams = new MyParams();
  totalCount: number;
  specializations: ISpecialization[];

  sortOptions = [
    {name: 'Sort Alphabetical by City', value: 'city'},
  // value is what we are sending as a query string parameter
    {name: 'Initial Fee Price: Low to High', value: 'priceAsc'},
    {name: 'Initial Fee Price: High to Low', value: 'priceDesc'},
    {name: 'Follow Up Fee Price: Low to High', value: 'priceAscFollowUp'},
    {name: 'Follow Up Fee Price: High to Low', value: 'priceDescFollowUp'}
  ];

  constructor(private patientofficesService: PatientOfficesService,
              private  router: Router) { }

  ngOnInit(): void {
    this.getOffices();
    this.getSpecializations();
  }

  getOffices() {
    this.patientofficesService.getOfficesForPatientView(this.myParams)
    .subscribe(response => {
      this.offices = response.data;
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
      this.getOffices();
      }


  onSearch() {
    this.myParams.query = this.searchTerm.nativeElement.value;
    this.getOffices();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getOffices();
  }

  onReset1() {
    this.filterTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getOffices();
  }

  onSortSelected(sort: string) {
    this.myParams.sort = sort;
    this.getOffices();
  }

  onPageChanged(event: any) {
    if (this.myParams.page !== event) {
      this.myParams.page = event;
      this.getOffices();
    }
}


}
