import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AppointmentsService } from '../appointments.service';

@Component({
  selector: 'app-appointment-add',
  templateUrl: './appointment-add.component.html',
  styleUrls: ['./appointment-add.component.scss']
})
export class AppointmentAddComponent implements OnInit {
  appointmentForms: FormArray = this.fb.array([]);
  officeList = [];
  errors: string[] = [];

  constructor(private appointmentsService: AppointmentsService,
              private router: Router,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.appointmentsService.getOffices()
    .subscribe(res => this.officeList = res as []);
    this.addAppointmentForm();
  }

  addAppointmentForm() {
    this.appointmentForms.push(this.fb.group({
      id: [0],
      startDateAndTimeOfAppointment: ['', Validators.required],
      endDateAndTimeOfAppointment: ['', Validators.required],
      office1Id: [0, Validators.min(1)],
    }));
  }

  get f() { return this.appointmentForms.controls; }

  recordSubmit(fg: FormGroup) {
      this.appointmentsService.createAppointment(fg.value).subscribe(
        (res: any) => {
          fg.patchValue({ id: res.id });
          this.router.navigateByUrl('appointments');
        }, error => {
            console.log(error);
            this.errors = error.errors;
          });
        }
}
