import { Component, OnInit } from '@angular/core';
import { CenovnikService } from '../cenovnik.service';
import { Stavka } from '../cenovnik';

@Component({
  selector: 'app-cenovnik-prikaz',
  templateUrl: './cenovnik-prikaz.component.html',
  styleUrls: ['./cenovnik-prikaz.component.css']
})
export class CenovnikPrikazComponent implements OnInit {

  stavke: Stavka[] = [];
  constructor(private cenovnikService: CenovnikService) { }

  ngOnInit() {
    this.getCenovnik();
  }

  getCenovnik():void{
    this.cenovnikService.getCenovnik().subscribe(stavka => this.stavke = stavka);
  }
}
