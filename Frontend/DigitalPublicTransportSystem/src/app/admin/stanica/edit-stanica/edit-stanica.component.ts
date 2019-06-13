import { Component, OnInit, NgZone } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { StanicaService } from 'src/app/services/stanica.service';
import { Router } from '@angular/router';
import { GeoLocation } from 'src/app/models/geolocation';
import { Stanica } from 'src/app/models/stanica';

@Component({
  selector: 'app-edit-stanica',
  templateUrl: './edit-stanica.component.html',
  styleUrls: ['./edit-stanica.component.css'],
  styles: ['agm-map {height: 500px; width: 700px;}'] 
})
export class EditStanicaComponent implements OnInit {
  stanicaForm = this.fb.group({
    Naziv: [''],
    Adresa:['']
  });
  
  public geoLocation: GeoLocation;
  public zoom: number;
  stanice: Stanica[] = [];
  selectedStanica: Stanica;
  verzija:number;
  constructor(private fb: FormBuilder, private ngZone: NgZone, private stanicService: StanicaService, private router:Router) { }

  ngOnInit() {
    this.getStanice();
  }

  placeMarker($event){
    this.geoLocation = new GeoLocation($event.coords.lat, $event.coords.lng);
  }

  izmeniStanicu(form: any){
    let stanica = new Stanica();
    stanica = this.stanicaForm.value;
    stanica.X = this.geoLocation.latitude;
    stanica.Y = this.geoLocation.longitude;
    stanica.Verzija = this.verzija
    this.stanicService.putStanica(stanica).subscribe(
      (response)=>{
        this.router.navigate(['']);
      },
      (error)=>{
        alert(error.error.Message);
      }
    );
  }

  getStanice(){
    this.stanicService.getStanica().subscribe(
    (data) =>{
      this.stanice = data;
    },
    (error)=>{
      alert(error.error.Message);
    });
  }

  OnSelectStanica(ime: string){
    let stanica = this.stanice.find(f=> f.Naziv === ime);
    this.selectedStanica = stanica;
    this.verzija = stanica.Verzija;
    this.stanicaForm.controls["Naziv"].setValue(stanica.Naziv);
    this.stanicaForm.controls["Adresa"].setValue(stanica.Adresa);
    this.geoLocation = new GeoLocation(stanica.X, stanica.Y);
  }

  izbrisiStanicu(){
    this.stanicService.deleteStanica(this.selectedStanica.Naziv).subscribe(
      (response)=>{
          this.selectedStanica = undefined;
          this.getStanice();
      },
      (error)=>{
        alert("Neuspesna izmena stanice. Moguce brisanje ili izmena od strane drugog admina u medjuvremenu. Pokusajte ponovo.");
      }
    );
  }
}
