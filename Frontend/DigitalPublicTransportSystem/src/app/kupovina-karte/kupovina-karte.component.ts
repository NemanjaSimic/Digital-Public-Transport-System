import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-kupovina-karte',
  templateUrl: './kupovina-karte.component.html',
  styleUrls: ['./kupovina-karte.component.css']
})
export class KupovinaKarteComponent implements OnInit {

  constructor(private fb: FormBuilder,private route: ActivatedRoute) { }

  tipKarte: string;
  tipPopusta: string;
  cena: number;
  role:any;
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

  }

  potvrdiKupovinu():void{
    
  }

}
