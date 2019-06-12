import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { KartaService } from '../services/karta.service';

@Component({
  selector: 'app-kupovina-karte',
  templateUrl: './kupovina-karte.component.html',
  styleUrls: ['./kupovina-karte.component.css']
})
export class KupovinaKarteComponent implements OnInit {

  constructor(private fb: FormBuilder,private route: ActivatedRoute, private kartaService : KartaService) { }

  emailForm = this.fb.group({
    email : ["", Validators.email]
  });

  tipKarte: string;
  tipPopusta: string;
  cena: number;
  role:any;

  get f() { return this.emailForm.controls; }

  ngOnInit() {
    this.getInfo();
  }

  getInfo(): void{
    this.tipKarte = this.route.snapshot.paramMap.get('tip');
    this.tipPopusta = this.route.snapshot.paramMap.get('popust');
    this.cena = +this.route.snapshot.paramMap.get('cena');
    this.role = localStorage.role;
  }

  kupiKartu(): void{
    this.kartaService.kupiKartuNeregistrovani(this.emailForm.get('email').value).subscribe(
      response => {
        alert("Uspesno ste kupili vremensku kartu u trajanju od 60 min. Poslali smo Vam sifru karte na Vas email.");
      },
      error => {
        alert("Desila se greska prilikom kupovine karte. Pokusajte ponovo.");
      }
    )
  }

  potvrdiKupovinu():void{

  }

}
