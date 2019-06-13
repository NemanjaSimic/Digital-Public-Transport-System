import { Component, OnInit } from '@angular/core';
import { KartaService } from '../services/karta.service';
import { Karta } from '../models/karta';

@Component({
  selector: 'app-prikaz-transakcija',
  templateUrl: './prikaz-transakcija.component.html',
  styleUrls: ['./prikaz-transakcija.component.css']
})
export class PrikazTransakcijaComponent implements OnInit {

  constructor(private kartaService : KartaService) { }

  karte: Karta[] = [];

  ngOnInit() {
    this.kartaService.getKarteKorisnika(localStorage.getItem('userId')).subscribe(karte => this.karte = karte);
    
  }


}
