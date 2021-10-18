import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IOffice } from 'src/app/shared/models/office';
import { PatientOfficesService } from '../patient-offices.service';

@Component({
  selector: 'app-office-info',
  templateUrl: './office-info.component.html',
  styleUrls: ['./office-info.component.scss']
})
export class OfficeInfoComponent implements OnInit {
  office: IOffice;

  constructor(private patientofficesService: PatientOfficesService,
              private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadOffice();
  }

  loadOffice() {
    return this.patientofficesService.getOffice(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
    this.office = response;
    }, error => {
    console.log(error);
    });

    }
}
