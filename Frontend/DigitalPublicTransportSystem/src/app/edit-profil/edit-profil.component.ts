import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { ConfirmPasswordValidator } from '../services/passvalidator.service';
import { Korisnik } from '../models/korisnik';
import { ProfilService } from '../services/profil.service';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-profil',
  templateUrl: './edit-profil.component.html',
  styleUrls: ['./edit-profil.component.css']
})
export class EditProfilComponent implements OnInit {

  novaSlika: boolean = false;
  mySrc : string;

  editForm = this.fb.group({
    name: [Validators.required],
    surname: [Validators.required],
    address: [Validators.required],
    dateOfBirth: [Validators.required],
    type: [Validators.required],
    username: [[Validators.required, Validators.minLength(6)]],
    email: [[Validators.email]],
    imgUrl: [],
    password: [[Validators.required,
      Validators.minLength(6),
      Validators.pattern(/(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W])/)]]
    
  });
  selectedFile: File = null;
  onFileSelected(event) {
    this.selectedFile = <File>event.target.files[0];
  }
  NovaSlika(event)
  {
    this.novaSlika = true;
  }
    get f() { return this.editForm.controls; }

  user:Korisnik = new Korisnik();
  editedUser: Korisnik
  oldUsername : string;
  date:string;

  constructor(private fb : FormBuilder, private profilService : ProfilService, private authService : AuthService,
    private router: Router) { }

  ngOnInit() {
    this.profilService.getUser(localStorage.getItem('userId')).subscribe(
      user => {
        this.user = user;
        this.editForm.setValue({
          name : this.user.Name,
          surname: this.user.Surname,
          address: this.user.Address,
          dateOfBirth: this.user.DateOfBirth,
          type: this.user.UserType,
          username: this.user.Username,
          email: this.user.Email,
          imgUrl: this.user.ImgUrl,
          password: ""
        });
        this.oldUsername = this.user.Username;
        this.date = this.user.DateOfBirth.toString().substring(0,10);
        if(this.user.ImgUrl != null)
        {
          this.profilService.downloadImage(this.oldUsername).subscribe(
            data => {
              this.mySrc = 'data:image/jpeg;base64,' + data;
              }
          );
        }
      }
    );

    
  }

  onSubmit(){
    this.editedUser = new Korisnik();
    
      this.editedUser.Username = this.editForm.get('username').value;
      this.editedUser.Name = this.editForm.get('name').value;
      this.editedUser.Surname = this.editForm.get('surname').value;
      this.editedUser.Email = this.editForm.get('email').value;
      this.editedUser.Address = this.editForm.get('address').value;
      this.editedUser.DateOfBirth = this.editForm.get('dateOfBirth').value;
      this.editedUser.UserType = this.editForm.get('type').value;
      this.editedUser.OldUsername = this.oldUsername;
      this.editedUser.ImgUrl = this.mySrc;
      this.editedUser.Password = this.editForm.get('password').value;

    if(this.authService.isLoggedIn()){
      this.profilService.editProfile(this.editedUser).subscribe(
        (response) => {
          if(this.selectedFile != null && this.novaSlika)
          {
            let formData: FormData = new FormData();
            formData.append('image', this.selectedFile, this.selectedFile.name);
            this.profilService.uploadImage(formData, this.oldUsername).subscribe(
              data =>  { }
            )
          }
          localStorage.setItem('userId', this.editedUser.Username);
          this.router.navigate(['/']);   
        },
        (error) => { alert("Pogresna lozinka, neuspesna izmena profila. Pokusajte ponovo."); }
      );
    }
  }
}
