import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { ConfirmPasswordValidator } from '../services/passvalidator.service';
import { RegisterService } from '../services/register.service';
import { Korisnik } from '../models/korisnik';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-registracija',
  templateUrl: './registracija.component.html',
  styleUrls: ['./registracija.component.css']
})
export class RegistracijaComponent implements OnInit {

  registerForm = this.fb.group({
    name: ['',
      Validators.required],
    surname: ['',
      Validators.required],
    address: ['', Validators.required],
    dateOfBirth: ['', Validators.required],
    type: ['RegularniKorisnik', Validators.required],
    username: ['',
      [Validators.required,
      Validators.minLength(6)]],
    email: ['',
      [Validators.email]],
    password: ['',
      [Validators.required,
      Validators.minLength(6),
      Validators.pattern(/(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W])/)]],
    confirmPassword: ['',
      Validators.required],
    picture: [''],
    
  }, { validators: ConfirmPasswordValidator });
  selectedFile: File = null;
  onFileSelected(event) {
    this.selectedFile = <File>event.target.files[0];
  }
  get f() { return this.registerForm.controls; }

  user:Korisnik;

  constructor(private fb : FormBuilder, private registerService : RegisterService, private authService:AuthService,
    private router: Router, private notificationService : NotificationService) { }

  ngOnInit() {
  }

  onSubmit(){
      this.user = new Korisnik(
      this.registerForm.get('username').value,
      this.registerForm.get('name').value,
      this.registerForm.get('surname').value,
      this.registerForm.get('email').value,
      this.registerForm.get('password').value,
      this.registerForm.get('confirmPassword').value,
      this.registerForm.get('address').value,
      this.registerForm.get('dateOfBirth').value,
      this.registerForm.get('type').value,
      ""
      //this.registerForm.get('picture').value
    );

    if(!this.authService.isLoggedIn()){
      this.registerService.register(this.user).subscribe(
        (response) => {
          this.router.navigate(['/login']);   
        },
        (error) => {}
      );
    }
  }
}
