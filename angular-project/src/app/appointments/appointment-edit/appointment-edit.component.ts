import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientAppointmentsService } from 'src/app/patient-appointments/patient-appointments.service';
import { IAppointmentSingle, INewAppointmentToCreateOrEdit } from 'src/app/shared/models/appointment';
import { AppointmentsService } from '../appointments.service';

@Component({
  selector: 'app-appointment-edit',
  templateUrl: './appointment-edit.component.html',
  styleUrls: ['./appointment-edit.component.scss']
})
export class AppointmentEditComponent implements OnInit {
  appointmentForms: FormArray = this.fb.array([]);
  officeList = [];
  id: number;
  appointmentnew: INewAppointmentToCreateOrEdit;
  appointment: IAppointmentSingle;
  errors: string[] = [];


  constructor(private appointmentsService: AppointmentsService,
              private patientappointmentsService: PatientAppointmentsService,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.id = this.activatedRoute.snapshot.params['id'];
    this.loadAppointment();
    this.appointmentsService.getOffices()
    .subscribe(res => this.officeList = res as []);

    this.patientappointmentsService.getApointmentForEdit(this.id).subscribe(
  (appointment: INewAppointmentToCreateOrEdit) => {
    this.appointmentForms.push(this.fb.group({
              id: [this.id],
              office1Id: [appointment.office1Id, Validators.required],
              patient1Id: [appointment.patient1Id],
              startDateAndTimeOfAppointment: [new Date(appointment.startDateAndTimeOfAppointment), Validators.required],
              endDateAndTimeOfAppointment: [new Date(appointment.endDateAndTimeOfAppointment), Validators.required],
              remarks: [appointment.remarks],
              status: [appointment.status]
            }));
      });
  }

  recordSubmit(fg: FormGroup) {
      this.appointmentsService.editAppointment(fg.value).subscribe(
        (res: any) => {
          this.router.navigateByUrl('appointments');
        }, error => {
            console.log(error);
            this.errors = error.errors;
          });
        }

  loadAppointment() {
  return this.patientappointmentsService.getAppointmentById(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
    this.appointment = response;
    }, error => {
    console.log(error);
    });
  }

}
