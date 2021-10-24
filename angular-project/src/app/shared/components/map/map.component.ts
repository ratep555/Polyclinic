import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { icon, latLng, LeafletMouseEvent, marker, Marker, tileLayer } from 'leaflet';
import { CoordinatesMap, CoordinatesMapWithMessage } from '../../models/coordinate';
import { INewOfficeToCreateOrEdit } from '../../models/office';
import * as L from 'leaflet';


@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit {
  @Output() selectedLocation = new EventEmitter<CoordinatesMap>();
  @Input()  initialCoordinates: CoordinatesMapWithMessage[] = [];
  @Input() editMode: boolean = true;
  /* @Input()  initialCoordinates: CoordinatesMap[] = [ {latitude: 45.81501605852479,
    longitude: 15.997348455331675 }]; */

  model: CoordinatesMap = {latitude: 45.823191360974505,
    longitude: 16.00924925704021 };


  layers: Marker<any>[] = [];
  options = {
    layers: [
      tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 18, attribution: 'Office Location' })
    ],
    zoom: 7,
    center: latLng(44.815183840035594, 15.99454538427381)
  };

  // odavde dolje proba
  /* markerIcon = {
    icon: L.icon({
      iconSize: [25, 41],
      iconAnchor: [10, 41],
      popupAnchor: [2, -40],
      // specify the path here
      iconUrl: 'https://unpkg.com/leaflet@1.4.0/dist/images/marker-icon.png',
      shadowUrl: 'https://unpkg.com/leaflet@1.4.0/dist/images/marker-shadow.png'
    })
  }; */

/* options1 = {
layers: [
  L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 18, attribution: '...' }),
  L.marker([ 12.972442, 77.580643 ]),  // Markers image not displaying
  ],
zoom: 5,
center: L.latLng({ lat: 12.9754246, lng: 77.5926457 }),

};
 */
  constructor() { }

  ngOnInit(): void {
    this.layers = this.initialCoordinates.map((value) => {
      const m = marker([value.latitude, value.longitude]);
      if (value.message){
        m.bindPopup(value.message, {autoClose: false, autoPan: false});
      }
      return m;
    });
  }

  handleMapClick(event: LeafletMouseEvent) {
    if (this.editMode) {
      const latitude = event.latlng.lat;
      const longitude = event.latlng.lng;
      console.log({ latitude, longitude });
      this.layers = [];
      this.layers.push(marker([latitude, longitude]));
      this.selectedLocation.emit({ latitude, longitude });
    }

  }



}
