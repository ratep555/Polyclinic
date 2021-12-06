import { Component, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-textaera-input',
  templateUrl: './textaera-input.component.html',
  styleUrls: ['./textaera-input.component.scss']
})
export class TextaeraInputComponent implements ControlValueAccessor {
  @Input() label: string;
  @Input() type = 'text';

  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
  }

  writeValue(obj: any): void {
  }

  registerOnChange(fn: any): void {
  }

  registerOnTouched(fn: any): void {
  }

}
