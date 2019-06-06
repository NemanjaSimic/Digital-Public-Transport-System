import { Component, OnInit } from '@angular/core';
import { LinijaService } from '../../services/linija.service';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
  selector: 'app-red-voznje',
  templateUrl: './red-voznje.component.html',
  styleUrls: ['./red-voznje.component.css']
})
export class RedVoznjeComponent implements OnInit {
  
  redVoznjeForm = this.fb.group({  
    TipVoznje: [''],
    Dan: ['RadniDan'],
    Ime:['']
  });
  
  dan:any;
  ime:any;
  linije: Array<string> = [];
  termini: Array<string> = [];
  constructor(private linijaService: LinijaService,
    private fb: FormBuilder,
    private router: Router) { }


  ngOnInit() {
  }

  criteriaChanged():void{
    let tip = this.redVoznjeForm.get('TipVoznje').value;
    if(tip){
      this.linijaService.getLinijeByTip(tip).subscribe(linija => this.linije = linija);
    }
    this.redVoznjeForm.controls['Ime'].setValue(this.linije[0]);
  }

  prkaziTermine():void{
    this.ime = this.redVoznjeForm.get('Ime').value;
    this.dan = this.redVoznjeForm.get('Dan').value;
    if(this.ime){
      this.linijaService.getTerminiOfLinija(this.ime,this.dan).subscribe(termini => this.termini = termini);
    }
  }

}
