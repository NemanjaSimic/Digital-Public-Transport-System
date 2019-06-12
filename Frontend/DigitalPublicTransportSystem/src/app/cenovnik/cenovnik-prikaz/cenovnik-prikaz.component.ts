import { Component, OnInit } from '@angular/core';
import { CenovnikService } from '../../services/cenovnik.service';
import { Stavka } from '../../models/cenovnik';

@Component({
  selector: 'app-cenovnik-prikaz',
  templateUrl: './cenovnik-prikaz.component.html',
  styleUrls: ['./cenovnik-prikaz.component.css']
})
export class CenovnikPrikazComponent implements OnInit {

  stavke: Stavka[] = [];
  constructor(private cenovnikService: CenovnikService) { }
  role : string;
  type : string;
  ngOnInit() {
    this.getCenovnik();
    this.role = localStorage.getItem('role');
    this.type = localStorage.getItem('userType');
  }

  getCenovnik():void{
    this.cenovnikService.getCenovnik().subscribe(stavka => this.stavke = stavka);
  }
  
}
