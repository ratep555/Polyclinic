import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IDoctor } from 'src/app/shared/models/doctor';
import { PatientOfficesService } from '../patient-offices.service';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { ISpecialization } from 'src/app/shared/models/specialization';
import { ISubspecialization } from 'src/app/shared/models/subspecialization';
import { IProfessionalAssociation } from 'src/app/shared/models/professionalAssociation';
import { IPublication } from 'src/app/shared/models/publication';
import { IOffice } from 'src/app/shared/models/office';

@Component({
  selector: 'app-doctor-detail',
  templateUrl: './doctor-detail.component.html',
  styleUrls: ['./doctor-detail.component.scss']
})
export class DoctorDetailComponent implements OnInit {
  doctor: IDoctor;
  specializations: ISpecialization[];
  subspecializations: ISubspecialization[];
  associations: IProfessionalAssociation[];
  publications: IPublication[];
  offices: IOffice[];

  constructor(private patientOfficesService: PatientOfficesService,
              private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadDoctor();
    this.loadSpecializations();
    this.loadSubspecializations();
    this.loadProfessionalAssociations();
    this.loadPublications();
    this.loadOffices();
  }

  loadDoctor() {
    return this.patientOfficesService.getDoctor(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
    this.doctor = response;
    }, error => {
    console.log(error);
    });
    }

  loadSpecializations() {
    return this.patientOfficesService.getSpecializationsForDoctor(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
    this.specializations = response;
    }, error => {
    console.log(error);
    });
    }

  loadSubspecializations() {
    return this.patientOfficesService.getSubspecializationsForDoctor
    (+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
    this.subspecializations = response;
    }, error => {
    console.log(error);
    });
    }

  loadProfessionalAssociations() {
    return this.patientOfficesService.getProfessionalAssociationsForDoctor
    (+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
    this.associations = response;
    }, error => {
    console.log(error);
    });
    }

  loadPublications() {
    return this.patientOfficesService.getPublicationsForDoctor
    (+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
    this.publications = response;
    }, error => {
    console.log(error);
    });
    }

  loadOffices() {
    return this.patientOfficesService.getOfficesForDoctor
    (+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
    this.offices = response;
    }, error => {
    console.log(error);
    });
    }

  }
