import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { INewOfficeToCreate, INewOfficeToEdit } from 'src/app/shared/models/office';
import { OfficesService } from '../offices.service';

@Component({
  selector: 'app-edit-office',
  templateUrl: './edit-office.component.html',
  styleUrls: ['./edit-office.component.scss']
})
export class EditOfficeComponent implements OnInit {
  model: INewOfficeToEdit;
  id: number;

  constructor(private activatedRoute: ActivatedRoute,
              private officesService: OfficesService,
              private router: Router) { }

  ngOnInit(): void {

    this.activatedRoute.params.subscribe(params => {
      this.officesService.getOfficeById(params.id).subscribe(office =>
        this.model = office);
    });
  }

  saveChanges(office: INewOfficeToCreate){
    this.officesService.updateOffice1(this.model.id, office).subscribe(() =>
    this.router.navigate(['/offices']));
  }

}
