import { Component, OnInit } from '@angular/core';
import { ProfilService } from 'src/app/services/profil.service';

@Component({
  selector: 'app-korisnici',
  templateUrl: './korisnici.component.html',
  styleUrls: ['./korisnici.component.css']
})
export class KorisniciComponent implements OnInit {

  constructor(private profilService : ProfilService) { }

  users = null;

  ngOnInit() {
    this.profilService.getAllUsers().subscribe(users => this.users = users);
  }

  onClick(username:string)
  {
    this.profilService.deactivateProfilByAdmin(username).subscribe(
      response => {
        alert("Uspesno ste deaktivirali profil korisniku: " + username);
        this.profilService.getAllUsers().subscribe(
          data => {this.users = data;}
        );
      },
      error => {
        alert("Korisnik sa username-om: " + username + " ne postoji ili je u medjuvremenu deaktiviran.");
      }
    )
  }
}
