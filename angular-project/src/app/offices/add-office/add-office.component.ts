import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { INewOfficeToCreate } from 'src/app/shared/models/office';
import { OfficesService } from '../offices.service';

@Component({
  selector: 'app-add-office',
  templateUrl: './add-office.component.html',
  styleUrls: ['./add-office.component.scss']
})
export class AddOfficeComponent implements OnInit {

  constructor(private officesService: OfficesService,
              private router: Router) { }

  ngOnInit(): void {
  }

  saveChanges(office: INewOfficeToCreate){
    console.log(office);
    this.officesService.createOffice1(office).subscribe(() => this.router.navigate(['/offices']));
  }

}
