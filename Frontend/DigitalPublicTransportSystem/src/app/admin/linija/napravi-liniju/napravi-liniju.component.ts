import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormArray } from '@angular/forms';
import { LinijaService } from 'src/app/services/linija.service';
import { Router } from '@angular/router';
import { NovaLinija } from 'src/app/models/nova-linija';

@Component({
  selector: 'app-napravi-liniju',
  templateUrl: './napravi-liniju.component.html',
  styleUrls: ['./napravi-liniju.component.css']
})
export class NapraviLinijuComponent implements OnInit {

  linijaForm = this.fb.group({
    Ime: [''],
    RedniBroj: [''],
    VrstaLinije: [''],
    RadniDanTermini: this.fb.array([
      this.fb.control('')
    ]),
    SubotaTermini: this.fb.array([
      this.fb.control('')
    ]),
    NedeljaTermini: this.fb.array([
      this.fb.control('')
    ])
  });
  constructor(private fb: FormBuilder, private linijaService: LinijaService, private router: Router) { }

  radniDanBrojac = 0;

  get RadniDanTermini(){
    return this.linijaForm.get('RadniDanTermini') as FormArray;
  }

  get SubotaTermini(){
    return this.linijaForm.get('SubotaTermini') as FormArray;
  }

  get NedeljaTermini(){
    return this.linijaForm.get('NedeljaTermini') as FormArray;
  }

  ngOnInit() {
  }

  DodajRadniDanTermin(){
    this.RadniDanTermini.push(this.fb.control(''));
  }

  DodajSubotaTermin(){
    this.SubotaTermini.push(this.fb.control(''));
  }

  DodajNedeljaTermin(){
    this.NedeljaTermini.push(this.fb.control(''));
  }

  napraviLiniju(){ 

    let novaLinija = new NovaLinija();
    novaLinija = this.linijaForm.value;
    if(novaLinija.RadniDanTermini != undefined)
      novaLinija.RadniDanTermini = novaLinija.RadniDanTermini.filter(l => l != "") as [];
    else
      novaLinija.RadniDanTermini = [];

    if(novaLinija.SubotaTermini != undefined)
      novaLinija.SubotaTermini = novaLinija.SubotaTermini.filter(l => l != "") as [];
    else
      novaLinija.SubotaTermini = [];

    if(novaLinija.NedeljaTermini != undefined)
      novaLinija.NedeljaTermini = novaLinija.NedeljaTermini.filter(l => l != "") as [];
    else
    novaLinija.NedeljaTermini = [];

    this.linijaService.napraviLiniju(novaLinija).subscribe(
      (response) => {
        this.router.navigate(['']);   
      },
      (error) => {console.log(error);}
      );
  }
}
