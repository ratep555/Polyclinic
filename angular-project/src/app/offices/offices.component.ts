import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { MyParams } from '../shared/models/myparams';
import { IOffice } from '../shared/models/office';
import { ISpecialty } from '../shared/models/specialty';
import { OfficesService } from './offices.service';

@Component({
  selector: 'app-offices',
  templateUrl: './offices.component.html',
  styleUrls: ['./offices.component.scss']
})
export class OfficesComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  offices: IOffice[];
  myParams = new MyParams();
  totalCount: number;

  constructor(private officesService: OfficesService,
              private  router: Router) { }

  ngOnInit(): void {
    this.getOffices();
  }

  getOffices() {
    this.officesService.getOffices(this.myParams)
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

  onSearch() {
    this.myParams.query = this.searchTerm.nativeElement.value;
    this.getOffices();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MyParams();
    this.getOffices();
  }

  onPageChanged(event: any) {
    if (this.myParams.page !== event) {
      this.myParams.page = event;
      this.getOffices();
    }
}


}
