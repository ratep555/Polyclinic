import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-felipe-list',
  templateUrl: './felipe-list.component.html',
  styleUrls: ['./felipe-list.component.scss']
})
export class FelipeListComponent implements OnInit {
  @Input() doctors;

  constructor() { }

  ngOnInit(): void {
  }

}
