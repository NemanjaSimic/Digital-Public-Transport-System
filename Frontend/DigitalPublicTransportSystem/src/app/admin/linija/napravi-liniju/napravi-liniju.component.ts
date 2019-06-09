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
    novaLinija.RadniDanTermini = novaLinija.RadniDanTermini.filter(l => l != "") as [];
    novaLinija.SubotaTermini = novaLinija.SubotaTermini.filter(l => l != "") as [];
    if(novaLinija.NedeljaTemini != undefined)
      novaLinija.NedeljaTemini = novaLinija.NedeljaTemini.filter(l => l != "") as [];
    else
    novaLinija.NedeljaTemini = [];

    this.linijaService.napraviLiniju(novaLinija).subscribe(
      (response) => {
        this.router.navigate(['']);   
      },
      (error) => {console.log(error);}
      );
  }
}
