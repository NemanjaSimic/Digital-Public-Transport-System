import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ConfirmPasswordValidator } from '../services/passvalidator.service';
import { ProfilService } from '../services/profil.service';
import { AuthService } from '../services/auth.service';
import { ChangePassModel } from '../models/changePassModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  changePassForm = this.fb.group({
    oldPassword: ['', [Validators.required,
    Validators.minLength(6),
    Validators.pattern(/(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W])/)]],
    password: ['', [Validators.required,
    Validators.minLength(6),
    Validators.pattern(/(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W])/)]],
  confirmPassword: ['',
    Validators.required],
  }, { validators: ConfirmPasswordValidator })

  get f() { return this.changePassForm.controls; }

  constructor(private fb : FormBuilder, private profilService : ProfilService,
    private authService : AuthService, private router : Router) { }

  ngOnInit() {
  }

  passModel : ChangePassModel;

  onSubmit(){
      if(this.authService.isLoggedIn)
      {
        this.passModel = new ChangePassModel(this.changePassForm.get('oldPassword').value,
        this.changePassForm.get('password').value, this.changePassForm.get('confirmPassword').value);
        this.profilService.changePassword(this.passModel).subscribe(
          (response) => {
            this.router.navigate(['/']); //uspesno izmenjena sifra
          },
          (error) => {} //greska
        );
      }
  }
}
