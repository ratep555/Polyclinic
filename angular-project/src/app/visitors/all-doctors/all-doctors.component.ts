import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { IDoctor } from 'src/app/shared/models/doctor';
import { ISpecialization } from 'src/app/shared/models/specialization';
import { UserParams } from 'src/app/shared/models/userParams';
import { VisitorsService } from '../visitors.service';

@Component({
  selector: 'app-all-doctors',
  templateUrl: './all-doctors.component.html',
  styleUrls: ['./all-doctors.component.scss']
})
export class AllDoctorsComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  @ViewChild('filter', {static: false}) filterTerm: ElementRef;
  doctors: IDoctor[];
  userParams: UserParams;
  totalCount: number;
  specializations: ISpecialization[];

  sortOptions = [
    {name: 'Sort Alphabetical by Name', value: 'city'},
    {name: 'Sort Descending by Name', value: 'nameDesc'},
    {name: 'Highest Ratings', value: 'rateDesc'},
    {name: 'Lowest Ratings', value: 'rateAsc'},
    {name: 'Most Experienced', value: 'practicingAsc'},
    {name: 'Least Experienced', value: 'practicingDesc'}
  ];

  constructor(private visitorsService: VisitorsService,
              private  router: Router) {
              this.userParams = this.visitorsService.getUserParams();

               }

  ngOnInit(): void {
    this.getDoctors();
    this.getSpecializations();
  }

  getDoctors() {
    this.visitorsService.setUserParams(this.userParams);
    this.visitorsService.getAllDoctors(this.userParams)
    .subscribe(response => {
      this.doctors = response.data;
      this.userParams.page = response.page;
      this.userParams.pageCount = response.pageCount;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
    }
    );
  }


  getSpecializations() {
    this.visitorsService.getSpecializations().subscribe(response => {
    this.specializations = response;
    }, error => {
    console.log(error);
    });
    }

    onSpecialisationSelected(specializationId: number) {
      this.userParams.specializationId = specializationId;
      this.getDoctors();
      }


  onSearch() {
    this.userParams.query = this.searchTerm.nativeElement.value;
    this.getDoctors();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.userParams = this.visitorsService.resetUserParams();
    this.getDoctors();
  }

  onReset1() {
    this.filterTerm.nativeElement.value = '';
    this.userParams = this.visitorsService.resetUserParams();
    this.getDoctors();
  }

  onSortSelected(sort: string) {
    this.userParams.sort = sort;
    this.getDoctors();
  }

  onPageChanged(event: any) {
    if (this.userParams.page !== event) {
      this.userParams.page = event;
      this.visitorsService.setUserParams(this.userParams);
      this.getDoctors();
    }
}


}

