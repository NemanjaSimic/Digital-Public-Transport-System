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

  editForm = this.fb.group({
    name: [Validators.required],
    surname: [Validators.required],
    address: [Validators.required],
    dateOfBirth: [Validators.required],
    type: [Validators.required],
    username: [[Validators.required, Validators.minLength(6)]],
    email: [[Validators.email]],
    imgUrl: [],
    
  });
  selectedFile: File = null;
  onFileSelected(event) {
    this.selectedFile = <File>event.target.files[0];
  }

  public imagePath;
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
  }

    get f() { return this.editForm.controls; }

  user:Korisnik = new Korisnik();
  editedUser: Korisnik
  oldUsername : string;

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
          imgUrl: this.user.ImgUrl
        });
        this.oldUsername = this.user.Username;
        this.imgURL = this.user.ImgUrl;
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
      this.editedUser.ImgUrl = this.imgURL;
    

    if(this.authService.isLoggedIn()){
      this.profilService.editProfile(this.editedUser).subscribe(
        (response) => {
          this.router.navigate(['/']);   
        },
        (error) => {}
      );
    }
  }
}
