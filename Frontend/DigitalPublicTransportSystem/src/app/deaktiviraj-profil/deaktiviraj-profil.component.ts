import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ProfilService } from '../services/profil.service';
import { LoginService } from '../services/login.service';
import { UserPassModel } from '../models/userPassModel';

@Component({
  selector: 'app-deaktiviraj-profil',
  templateUrl: './deaktiviraj-profil.component.html',
  styleUrls: ['./deaktiviraj-profil.component.css']
})
export class DeaktivirajProfilComponent implements OnInit {

  constructor(private fb : FormBuilder, private profilService : ProfilService, private loginService : LoginService) { }

  deactivateForm = this.fb.group({
    password: ['', [Validators.required,
      Validators.minLength(6),
      Validators.pattern(/(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W])/)]]
  });

  get f() { return this.deactivateForm.controls; }

  ngOnInit() {
  }

  onSubmit()
  {
    let username = localStorage.getItem('userId');
    let password = this.deactivateForm.get('password').value;
    let userPass = new UserPassModel(username,password);
    this.profilService.deactivateMyProfil(userPass).subscribe(
      (response) => {
        alert("Uspesno ste deaktivirali profil.");
        this.loginService.logOut();
      },
      (error) => {
        alert("Greska prilikom deaktivacije profila.");
      }
    );
  }

}
