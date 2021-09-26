import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ISpecialty } from 'src/app/shared/models/specialty';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  isCollapsed = true;


  constructor() { }

  ngOnInit(): void {

}
}
