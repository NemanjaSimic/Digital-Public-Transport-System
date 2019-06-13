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
  mySrc : string;
  datum : string
  constructor(private http: HttpClient, private profilService : ProfilService) { }

  ngOnInit() {
    this.profilService.getUser(localStorage.getItem('userId')).subscribe(
      user => {
        this.user = user;
        this.datum = user.DateOfBirth.toString().substring(0,10);
        if(this.user.ImgUrl != null)
        {
        this.profilService.downloadImage(localStorage.getItem('userId')).subscribe(
          data => {
              this.mySrc = 'data:image/jpeg;base64,' + data;
            });
        }
      }
    );
  }

}
