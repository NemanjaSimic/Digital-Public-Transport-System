import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { ConfirmPasswordValidator } from '../services/passvalidator.service';
import { RegisterService } from '../services/register.service';
import { Korisnik } from '../models/korisnik';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { NotificationService } from '../services/notification.service';
import { ProfilService } from '../services/profil.service';

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
    imgUrl: [''],
    
  }, { validators: ConfirmPasswordValidator });
  selectedFile: File = null;

  // mySrc: SafeUrl;
  mySrc: String;
  
  onFileSelected(event) {
    this.selectedFile = <File>event.target.files[0];
  }

  /* public imagePath;
  imgURL : any;
  preview(files) {
    if (files.length === 0)
      return;

    var reader = new FileReader();
    this.imagePath = files;
    reader.readAsDataURL(files[0]); 
    reader.onload = (_event) => { 
      this.imgURL = reader.result;
    }
  } */
  
  get f() { return this.registerForm.controls; }

  user:Korisnik;

  constructor(private fb : FormBuilder, private registerService : RegisterService, private authService:AuthService,
    private router: Router, private notificationService : NotificationService,
    private profilService : ProfilService) { }

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
      
    );

    if(!this.authService.isLoggedIn()){
      this.registerService.register(this.user).subscribe(
        (response) => {
          if(this.selectedFile != null)
          {
            let formData: FormData = new FormData();
            formData.append('image', this.selectedFile, this.selectedFile.name);
            this.profilService.uploadImage(formData, this.registerForm.get('username').value).subscribe(
              data => {
                this.profilService.downloadImage(this.registerForm.get('username').value).subscribe(
                  data => {
                    this.mySrc = 'data:image/jpeg;base64,' + data;
                    });
              }
            )
          }
          this.router.navigate(['/login']);   
        },
        (error) => 
        { 
          alert("Korisnik sa korisnickim imenom -> " + this.registerForm.get('username').value + " vec postoji. Pokusajte ponovo." );
          this.registerForm.patchValue({username : ''});
        }
      );
    }
  }
}
