import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Korisnik } from '../models/korisnik';
import { ProfilService } from '../services/profil.service';

@Component({
  selector: 'app-profil',
  templateUrl: './profil.component.html',
  styleUrls: ['./profil.component.css']
})
export class ProfilComponent implements OnInit {

  user: Korisnik = new Korisnik();

  constructor(private http: HttpClient, private profilService : ProfilService) { }

  ngOnInit() {
    this.profilService.getUser(localStorage.getItem('userId')).subscribe(
      user => {
        this.user = user
      }
    );
  }

}
