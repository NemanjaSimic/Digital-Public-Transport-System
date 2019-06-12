import { Component, OnInit } from '@angular/core';
import { KartaService } from 'src/app/services/karta.service';
import {NgForm} from '@angular/forms';


@Component({
  selector: 'app-validacija-karta',
  templateUrl: './validacija-karta.component.html',
  styleUrls: ['./validacija-karta.component.css']
})
export class ValidacijaKartaComponent implements OnInit {

  porukaStigla: boolean = false;
  poruka: [];
  constructor(private kartaService: KartaService) { }

  ngOnInit() {
  }

  validirajKartu(id: any){
    this.kartaService.validateKarta(id.ID).subscribe(
      (response) =>{
        this.poruka =  response.split(";");
        this.porukaStigla = true;
      },
      (error) =>{
        console.log(error);
      }
    );
  }
}
