import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { KartaService } from '../services/karta.service';
import { Karta } from '../models/karta';

@Component({
  selector: 'app-kupovina-karte',
  templateUrl: './kupovina-karte.component.html',
  styleUrls: ['./kupovina-karte.component.css']
})
export class KupovinaKarteComponent implements OnInit {

  constructor(private fb: FormBuilder,private route: ActivatedRoute, private kartaService : KartaService,
    private router: Router) { }

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
        this.router.navigate(['/']);
      },
      error => {
        alert("Desila se greska prilikom kupovine karte. Pokusajte ponovo.");
      }
    )
  }

  potvrdiKupovinu():void{
    let karta = new Karta(localStorage.getItem('userId'), this.tipKarte, this.tipPopusta, this.cena);
    this.kartaService.kupiKartuRegistrovani(karta).subscribe(
      response => {
        alert("Uspesno ste kupili kartu tipa ->"  + this.tipKarte);
        this.router.navigate(['/']);
      },
      error => {
        alert("Vas dokument nije validiran ili Vam nije dozvoljeno da kupite izabran tip karte. Pokusajte ponovo.");
      }
    )
  }

}
