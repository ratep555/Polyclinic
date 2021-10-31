import { Component, Input, OnInit } from '@angular/core';
import { MultipleSelectorModel } from '../../models/multiple-selector.model';

@Component({
  selector: 'app-multiple-selector',
  templateUrl: './multiple-selector.component.html',
  styleUrls: ['./multiple-selector.component.scss']
})
export class MultipleSelectorComponent implements OnInit {
  @Input()
  SelectedItems: MultipleSelectorModel[] = [];

  @Input()
  NonSelectedItems: MultipleSelectorModel[] = [];

  constructor() { }

  ngOnInit(): void {
  }

  select(item: MultipleSelectorModel, index: number){
    this.SelectedItems.push(item);
    this.NonSelectedItems.splice(index, 1);
  }

  deselect(item: MultipleSelectorModel, index: number){
    this.NonSelectedItems.push(item);
    this.SelectedItems.splice(index, 1);
  }


  selectAll(){
    // we are pushing all of the elements of the nonselected items into selecteditems
     this.SelectedItems.push(...this.NonSelectedItems);
     this.NonSelectedItems = [];
  }

  deSelectAll(){
    this.NonSelectedItems.push(...this.SelectedItems);
    this.SelectedItems = [];
  }
}
