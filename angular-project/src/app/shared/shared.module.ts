import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PagerComponent } from './components/pager/pager.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
// import { TextInputComponent } from './components/text-input/text-input.component';
// import { GoogleChartsModule } from 'angular-google-charts';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import { TextInputComponent } from './components/text-input/text-input.component';
import { DateInputComponent } from './components/date-input/date-input.component';
import { Date1InputComponent } from './components/date1-input/date1-input.component';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ToastrModule } from 'ngx-toastr';
import { LeafletModule} from '@asymmetrik/ngx-leaflet';
import 'leaflet/dist/images/marker-shadow.png';
import 'leaflet/dist/images/marker-icon-2x.png';
// import './assets/downloadLea.jpg';
import { MapComponent } from './components/map/map.component';
import { RatingComponent } from './components/rating/rating.component';
import { RatingModule } from 'ngx-bootstrap/rating';
import { HasRoleDirective } from './directives/has-role.directive';


@NgModule({
  declarations: [

    PagerComponent,
     TextInputComponent,
     DateInputComponent,
     Date1InputComponent,
     MapComponent,
     RatingComponent,
     HasRoleDirective
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    LeafletModule,
    PaginationModule.forRoot(),
    TypeaheadModule.forRoot(),
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    CarouselModule.forRoot(),
    BsDatepickerModule.forRoot(),
    RatingModule.forRoot()
  ],
  exports: [
    PaginationModule,
    ToastrModule,
    TabsModule,
    TypeaheadModule,
    BsDropdownModule,
    PagerComponent,
    CarouselModule,
    ReactiveFormsModule,
    LeafletModule,
    TextInputComponent,
     DateInputComponent,
     Date1InputComponent,
     MapComponent,
    // GoogleChartsModule,
    BsDatepickerModule,
    RatingModule,
    RatingComponent,
    HasRoleDirective
  ]

})
export class SharedModule { }
