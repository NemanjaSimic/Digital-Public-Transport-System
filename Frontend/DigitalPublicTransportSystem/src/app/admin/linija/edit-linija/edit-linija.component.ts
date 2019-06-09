import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormArray } from '@angular/forms';
import { LinijaService } from 'src/app/services/linija.service';
import { Router } from '@angular/router';
import { NovaLinija } from 'src/app/models/nova-linija';

@Component({
  selector: 'app-edit-linija',
  templateUrl: './edit-linija.component.html',
  styleUrls: ['./edit-linija.component.css']
})
export class EditLinijaComponent implements OnInit {
  linijaForm = this.fb.group({  
    TipVoznje: [''],
    Ime:['']
  });

  izmenaForm = this.fb.group({
    Ime: [''],
    RedniBroj: [''],
    VrstaLinije: [''],
    RadniDanTermini: this.fb.array([
    ]),
    SubotaTermini: this.fb.array([
    ]),
    NedeljaTermini: this.fb.array([
    ])
  });

  constructor(private linijaService: LinijaService,
    private fb: FormBuilder,private router: Router) { }

  linije: Array<string> = [];
  izmeni: boolean = false;

  get RadniDanTermini(){
    return this.izmenaForm.get('RadniDanTermini') as FormArray;
  }

  get SubotaTermini(){
    return this.izmenaForm.get('SubotaTermini') as FormArray;
  }

  get NedeljaTermini(){
    return this.izmenaForm.get('NedeljaTermini') as FormArray;
  }

  ngOnInit() {

  }

  criteriaChanged():void{
    let tip = this.linijaForm.get('TipVoznje').value;
    if(tip){
      this.linijaService.getLinijeByTip(tip).subscribe(
        (linija) => {
          this.linije = linija;
          this.linijaForm.controls['Ime'].setValue(this.linije[0]);
        });
    }
  }

  getLiniju(){
    this.linijaService.getLinija(this.linijaForm.get('Ime').value).subscribe(
      (linija) =>{
        this.izmenaForm.reset();
        this.izmeni = true;
        this.izmenaForm.controls['Ime'].setValue(linija.Ime);
        this.izmenaForm.controls['RedniBroj'].setValue(linija.RedniBroj);
        this.izmenaForm.controls['VrstaLinije'].setValue(linija.VrstaLinije);
        this.DodajRadniDanTerminList(linija.RadniDanTermini);
        this.DodajSubotaTerminList(linija.SubotaTermini);
        this.DodajNedeljaTerminList(linija.NedeljaTermini);
        console.log(this.izmenaForm.value);
      },
      (error) =>{
        console.log(error);
      }
    );
  }


 
  DodajRadniDanTerminList(lista:Array<string>){
    this.RadniDanTermini.clear();
    lista.forEach(element => {
    this.RadniDanTermini.push(this.fb.control(element));
    });
  }

  DodajSubotaTerminList(lista:Array<string>){
    this.SubotaTermini.clear();
    lista.forEach(element => {
      this.SubotaTermini.push(this.fb.control(element));
      });
  }

  DodajNedeljaTerminList(lista:Array<string>){
    this.NedeljaTermini.clear();
    lista.forEach(element => {
      this.NedeljaTermini.push(this.fb.control(element));
      });
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

  IzmeniLiniju(){
    let novaLinija = new NovaLinija();
    novaLinija = this.izmenaForm.value;

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

    this.linijaService.izmeniLiniju(novaLinija).subscribe(
      (response) => {
        this.router.navigate(['']);   
      },
      (error) => {console.log(error);}
      );

  }
}
