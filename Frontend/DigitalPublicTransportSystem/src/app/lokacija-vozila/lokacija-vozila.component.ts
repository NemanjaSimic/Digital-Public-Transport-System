import { Component, OnInit, NgZone } from '@angular/core';
import { LokacijaService } from '../services/lokacija.service';
import { MarkerInfo } from '../models/marker-info.mode';
import { LinijaService } from '../services/linija.service';
import { GeoLocation } from '../models/geolocation';
import { NavigationStart, Router } from '@angular/router';

@Component({
  selector: 'app-lokacija-vozila',
  templateUrl: './lokacija-vozila.component.html',
  styleUrls: ['./lokacija-vozila.component.css'],
  styles: ['agm-map {height: 600px; width: 1125px;}'] 
})
export class LokacijaVozilaComponent implements OnInit {

  isConnected: Boolean;
  locations: string[];
  selectedLinija: string;
  markeri: MarkerInfo[] = [];
  autobus: MarkerInfo;
  linije: Array<string> = [];
  bus: string[];
  private routeSub:any;

  constructor(private lokacijaService: LokacijaService, private ngZone: NgZone,private linijaService: LinijaService,private router: Router) {
    this.isConnected = false;
    this.locations = [];
   }

  ngOnInit() {

    this.checkConnection();
    this.subscribeForLocations();
    this.lokacijaService.registerForLocation();
    this.linijaService.getLinijeByTip('').subscribe(
      (linija) => {
        this.linije = linija;
      });
  }

  private checkConnection(){
    this.lokacijaService.startConnection().subscribe(e => {this.isConnected = e; 
        if (e) {
          //  this.lokacijaService.Start();
          this.lokacijaService.Start();
        }
    });
  }

  private subscribeForLocations (){
     this.lokacijaService.locationReceived.subscribe(l => this.onNotification(l));
  }

  public onNotification(notification: string) {

    this.ngZone.run(() => { 
      console.log(notification);
      let busevi = notification.split(";");
      busevi.forEach(element => {
        let temp = element.split("_");
        if(temp[0] == this.selectedLinija)
          this.bus = temp;
      });
      if (this.bus != undefined){
        this.autobus = new MarkerInfo(new GeoLocation(+this.bus[1],+this.bus[2]),{url:"assets/img/bus.png", scaledSize:{width: 25, height: 25}}, "", "", "");
      }
   });  
 }

 prikaziBus(linija: any){
  this.selectedLinija = linija;
  this.markeri = [];
  this.linijaService.getStaniceOfLinija(linija).subscribe(
    (data) =>{
        data.forEach(element => {
          let location = new GeoLocation(element.X, element.Y);
          this.markeri.push(new MarkerInfo(location,{url:"assets/img/marker.png", scaledSize:{width: 50, height: 50}}, element.Naziv, element.Adresa, ""))
        });
    },
    (error) =>{
      console.log(error);
    }
  );
 }
}
