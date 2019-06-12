import { Component, OnInit, NgZone } from '@angular/core';
import { Stanica } from '../models/stanica';
import { LinijaService } from '../services/linija.service';
import { FormBuilder } from '@angular/forms';
import { MarkerInfo } from '../models/marker-info.mode';
import { Polyline } from '../models/polyline';
import { GeoLocation } from '../models/geolocation';


@Component({
  selector: 'app-mreza-linija',
  templateUrl: './mreza-linija.component.html',
  styleUrls: ['./mreza-linija.component.css'],
  styles: ['agm-map {height: 600px; width: 1125px;}'] 
})
export class MrezaLinijaComponent implements OnInit {


  constructor(private linijaService: LinijaService,private fb: FormBuilder,private ngZone: NgZone) { }
  selectedLinija: string;
  stanica: Stanica;
  linije: Array<string> = [];
  markeri: MarkerInfo[] = [];
  mreza: Polyline;

  ngOnInit() {
    this.linijaService.getLinijeByTip('').subscribe(
      (linija) => {
        this.linije = linija;
      });
      
  }

  prikaziMrezu(linija: any){
    this.selectedLinija = linija;
    this.mreza = new Polyline([], 'blue', {url:"assets/img/marker.png", scaledSize:{width: 50, height: 50}});
    this.markeri = [];
    this.linijaService.getStaniceOfLinija(linija).subscribe(
      (data) =>{
          data.forEach(element => {
            let location = new GeoLocation(element.X, element.Y);
            this.markeri.push(new MarkerInfo(location,{url:"assets/img/marker.png", scaledSize:{width: 50, height: 50}}, element.Naziv, element.Adresa, ""))
            this.mreza.addLocation(location);
          });
      },
      (error) =>{
        console.log(error);
      }
    );
  }
  
  

}
