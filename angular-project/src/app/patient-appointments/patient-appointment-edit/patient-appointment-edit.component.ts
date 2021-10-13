import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { IAppointment, IAppointmentSingle, INewAppointmentToCreateOrEdit } from 'src/app/shared/models/appointment';
import { PatientAppointmentsService } from '../patient-appointments.service';

@Component({
  selector: 'app-patient-appointment-edit',
  templateUrl: './patient-appointment-edit.component.html',
  styleUrls: ['./patient-appointment-edit.component.scss']
})
export class PatientAppointmentEditComponent implements OnInit {
  appointmentForms: FormArray = this.fb.array([]);
  officeList = [];
  id: number;
  appointmentnew: INewAppointmentToCreateOrEdit;
  errors: string[] = [];
  appointment: IAppointmentSingle;


  constructor(private patientAppointmentsService: PatientAppointmentsService,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.id = this.activatedRoute.snapshot.params['id'];
    this.loadAppointment();
    this.patientAppointmentsService.getOffices()
    .subscribe(res => this.officeList = res as []);

    this.patientAppointmentsService.getApointmentForEdit(this.id).subscribe(
  (appointment: INewAppointmentToCreateOrEdit) => {
    this.appointmentForms.push(this.fb.group({
              id: [this.id],
              office1Id: [appointment.office1Id, Validators.required],
              remarks: ['']
            }));
      });
  }

  recordSubmit(fg: FormGroup) {
      this.patientAppointmentsService.editAppointment(fg.value).subscribe(
        (res: any) => {
          this.router.navigateByUrl('patient-appointments');
        }, error => {
            console.log(error);
            this.errors = error.errors;
          });
        }


    loadAppointment() {
      return this.patientAppointmentsService.getAppointmentById(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
        this.appointment = response;
      }, error => {
        console.log(error);
      });
    }

}

