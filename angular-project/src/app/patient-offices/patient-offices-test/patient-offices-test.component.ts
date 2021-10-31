import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { IOffice } from 'src/app/shared/models/office';
import { ISpecialization } from 'src/app/shared/models/specialization';
import { UserParams } from 'src/app/shared/models/userParams';
import { PatientOfficesService } from '../patient-offices.service';

@Component({
  selector: 'app-patient-offices-test',
  templateUrl: './patient-offices-test.component.html',
  styleUrls: ['./patient-offices-test.component.scss']
})
export class PatientOfficesTestComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  @ViewChild('filter', {static: false}) filterTerm: ElementRef;
  offices: IOffice[];
  userParams: UserParams;
  totalCount: number;
  specializations: ISpecialization[];

  sortOptions = [
    {name: 'Sort Alphabetical by City', value: 'city'},
    {name: 'Initial Fee Price: Low to High', value: 'priceAsc'},
    {name: 'Initial Fee Price: High to Low', value: 'priceDesc'},
    {name: 'Follow Up Fee Price: Low to High', value: 'priceAscFollowUp'},
    {name: 'Follow Up Fee Price: High to Low', value: 'priceDescFollowUp'}
  ];

  constructor(private patientofficesService: PatientOfficesService,
              private  router: Router) {
              this.userParams = this.patientofficesService.getUserParams();

               }

  ngOnInit(): void {
    this.getOffices();
    this.getSpecializations();
  }

  getOffices() {
    this.patientofficesService.setUserParams(this.userParams);
    this.patientofficesService.getOfficesForPatientView(this.userParams)
    .subscribe(response => {
      this.offices = response.data;
      this.userParams.page = response.page;
      this.userParams.pageCount = response.pageCount;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
    }
    );
  }

  resetFilters() {
    this.userParams = this.patientofficesService.resetUserParams();
    this.getOffices();
  }

  getSpecializations() {
    this.patientofficesService.getSpecializations().subscribe(response => {
    this.specializations = response;
    }, error => {
    console.log(error);
    });
    }

    onSpecialisationSelected(specializationId: number) {
      this.userParams.specializationId = specializationId;
      this.getOffices();
      }


  onSearch() {
    this.userParams.query = this.searchTerm.nativeElement.value;
    this.getOffices();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.userParams = this.patientofficesService.resetUserParams();
    this.getOffices();
  }

  onReset1() {
    this.filterTerm.nativeElement.value = '';
    this.userParams = this.patientofficesService.resetUserParams();
    this.getOffices();
  }

  onSortSelected(sort: string) {
    this.userParams.sort = sort;
    this.getOffices();
  }

  onPageChanged(event: any) {
    if (this.userParams.page !== event) {
      this.userParams.page = event;
      this.patientofficesService.setUserParams(this.userParams);
      this.getOffices();
    }
}


}

