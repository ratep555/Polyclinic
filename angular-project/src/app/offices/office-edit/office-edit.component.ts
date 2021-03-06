import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { marker, Marker } from 'leaflet';
import { first } from 'rxjs/operators';
import { CoordinatesMap } from 'src/app/shared/models/coordinate';
import { OfficesService } from '../offices.service';

@Component({
  selector: 'app-office-edit',
  templateUrl: './office-edit.component.html',
  styleUrls: ['./office-edit.component.scss']
})
export class OfficeEditComponent implements OnInit {
  officeForm: FormGroup;
  id: number;
  errors: string[] = [];
  initialCoordinates: CoordinatesMap[] = [];
  layers: Marker<any>[] = [];


  constructor(private fb: FormBuilder,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private officesService: OfficesService
) { }

ngOnInit(): void {

  this.id = this.activatedRoute.snapshot.params['id'];

  this.officeForm = this.fb.group({
    id: [this.id],
    street: ['', [Validators.required]],
    city: ['', [Validators.required]],
    country: ['', [Validators.required]],
    initialExaminationFee: ['', [Validators.required]],
    followUpExaminationFee: ['', [Validators.required]],
    description: ['', [Validators.required]],
    longitude: ['', [Validators.required]],
    latitude: ['', [Validators.required]]
  });

  this.officesService.getOfficeById(this.id)
  .pipe(first())
  .subscribe(x => this.officeForm.patchValue(x));
}

onSubmit() {
  if (this.officeForm.invalid) {
      return;
  }
  this.updateOffice();
}

private updateOffice() {
this.officesService.updateOffice(this.id, this.officeForm.value)
    .pipe(first())
    .subscribe(() => {
        this.router.navigateByUrl('offices');
      }, error => {
        console.log(error);
        this.errors = error.errors;
      });
    }

    onSelectedLocation(coordinates: CoordinatesMap) {
      this.officeForm.patchValue(coordinates);
      this.initialCoordinates.push({latitude: this.officesService.formData.latitude,
        longitude: this.officesService.formData.longitude});
   }
}






