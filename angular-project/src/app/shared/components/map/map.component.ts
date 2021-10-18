import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { icon, latLng, LeafletMouseEvent, marker, Marker, tileLayer } from 'leaflet';
import { CoordinatesMap } from '../../models/coordinate';


@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit {
  @Output() selectedLocation = new EventEmitter<CoordinatesMap>();

  layers: Marker<any>[] = [];
  options = {
    layers: [
      tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 18, attribution: 'Office Location' })
    ],
    zoom: 14,
    center: latLng(45.815183840035594, 15.99454538427381)
  };

  constructor() { }

  ngOnInit(): void {
  }

  handleMapClick(event: LeafletMouseEvent) {
      const latitude = event.latlng.lat;
      const longitude = event.latlng.lng;
      console.log({ latitude, longitude });
      this.layers = [];
      this.layers.push(marker([latitude, longitude]));
      this.selectedLocation.emit({ latitude, longitude });
  }



}
