import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormArray } from '@angular/forms';
import { LinijaService } from 'src/app/services/linija.service';
import { Router } from '@angular/router';
import { NovaLinija } from 'src/app/models/nova-linija';
import { StanicaService } from 'src/app/services/stanica.service';
import { Stanica } from 'src/app/models/stanica';

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
    ]),
    Stanice: [],
    IzabraneStanice: []
  });
  constructor(private fb: FormBuilder, private linijaService: LinijaService, private router: Router, private stanicaService: StanicaService) { }
  
  stanice = [];
  izabraneStanice = [];
 

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
    this.stanicaService.getStanica().subscribe(
      (data) =>{
        data.forEach(element => {
          this.stanice.push(element)
        })
        },
        (error) =>{
          console.log(error);
        }
        );
    }

    dodajStanicu(stanica: any){
      this.stanice = this.stanice.filter(s=> s.Naziv != stanica.Naziv)
      this.izabraneStanice.push(stanica);
      this.updateStaniceInForm();
      
    }

    izbrisiStanicu(stanica: any){
      this.izabraneStanice = this.izabraneStanice.filter(s=> s.Naziv != stanica.Naziv)
      this.stanice.push(stanica)
      this.updateStaniceInForm();
    }

    updateStaniceInForm(){
      this.linijaForm.controls['IzabraneStanice'].setValue(this.izabraneStanice);
      this.linijaForm.controls['Stanice'].setValue(this.stanice);
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

  IzbrisiTerminRadniDan(index:number){
    this.RadniDanTermini.removeAt(index);
  }

  IzbrisiTerminSubota(index:number){
    this.SubotaTermini.removeAt(index);
  }

  IzbrisiTerminNedelja(index:number){
    this.NedeljaTermini.removeAt(index);
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
    novaLinija.Stanice = [];
    this.izabraneStanice.forEach(element=>{
      novaLinija.Stanice.push(element.Naziv as never);
    })

    this.linijaService.napraviLiniju(novaLinija).subscribe(
      (response) => {
        this.router.navigate(['']);   
      },
      (error) => {console.log(error);}
      );
  }
}
