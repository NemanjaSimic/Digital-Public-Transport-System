import { Component, OnInit, NgZone } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MarkerInfo } from 'src/app/models/marker-info.mode';
import { Polyline } from 'src/app/models/polyline';
import { GeoLocation } from 'src/app/models/geolocation';
import { Stanica } from 'src/app/models/stanica';
import { StanicaService } from 'src/app/services/stanica.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nova-stanica',
  templateUrl: './nova-stanica.component.html',
  styleUrls: ['./nova-stanica.component.css'],
  styles: ['agm-map {height: 500px; width: 700px;}'] 
})
export class NovaStanicaComponent implements OnInit {

  public geoLocation: GeoLocation;
  public zoom: number;

  constructor(private fb: FormBuilder, private ngZone: NgZone, private stanicService: StanicaService, private router:Router) { }

  ngOnInit() {
  }

  placeMarker($event){
    this.geoLocation = new GeoLocation($event.coords.lat, $event.coords.lng);
  }

  napraviStanicu(form: any){
    let stanica = new Stanica();
    stanica.Naziv = form.Ime;
    stanica.Adresa= form.Adresa;
    stanica.X = this.geoLocation.latitude;
    stanica.Y = this.geoLocation.longitude;
    this.stanicService.postStanica(stanica).subscribe(
      (response)=>{
        this.router.navigate(['']);
      },
      (error)=>{
        console.log(error);
      }
    );
  }
}
