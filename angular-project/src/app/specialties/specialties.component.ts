import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { MyParams } from '../shared/models/myparams';
import { ISpecialty } from '../shared/models/specialty';
import { SpecialtiesService } from './specialties.service';

@Component({
  selector: 'app-specialties',
  templateUrl: './specialties.component.html',
  styleUrls: ['./specialties.component.scss']
})
export class SpecialtiesComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  specialties: ISpecialty[];
  myParams = new MyParams();
  totalCount: number;

  constructor(private specialtiesService: SpecialtiesService,
              private  router: Router) { }

  ngOnInit(): void {
    this.getSpecialties();
  }

  getSpecialties() {
    this.specialtiesService.getSpecialties(this.myParams)
    .subscribe(response => {
      this.specialties = response.data;
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
    this.getSpecialties();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getSpecialties();
  }

  onPageChanged(event: any) {
    if (this.myParams.page !== event) {
      this.myParams.page = event;
      this.getSpecialties();
    }
}


}
