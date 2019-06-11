import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ProfilService } from '../../services/profil.service';

@Component({
  selector: 'app-validacija-dokumenata',
  templateUrl: './validacija-dokumenata.component.html',
  styleUrls: ['./validacija-dokumenata.component.css']
})
export class ValidacijaDokumenataComponent implements OnInit {

  constructor(private profilService : ProfilService, private fb: FormBuilder) { }
  reasonForm = this.fb.group({
    reason: [""]
  });

  usersToValidate = null;
  userToValidate = null;
  imageToValidate = null;
  reason = "";
  ngOnInit() {
    this.profilService.getUsersForValidation().subscribe(
      data => {
        this.usersToValidate = data;
      },
      error =>{
        console.log(error);
      }
    )
  }

  onValidateClick(index: number){
    this.userToValidate = this.usersToValidate[index];  
    this.profilService.downloadImage(this.usersToValidate[index].Username).subscribe(
      data => {
        this.imageToValidate = 'data:image/jpeg;base64,' + data;
      }, error => {
        console.log(error);
        this.userToValidate = null;
        this.imageToValidate = null;
      }
    ) 

    
  }

  onValidateUser(option: string) {
    let sendData = {
      Username : this.userToValidate.Username,
      Status : option,
      Reason: this.reasonForm.get('reason').value
    }
    console.log(this.reasonForm.get('reason'));
    this.profilService.validateUser(sendData).subscribe(
      data => {
        alert(`Korisnik: ${this.userToValidate.Username} je uspesno validiran.`)
        this.userToValidate = null;
        this.profilService.getUsersForValidation().subscribe(
          data => {
            this.usersToValidate = data;
            this.reasonForm.reset();
          },
          error =>{
            console.log(error);
          }
        )
      },
      error => {
        console.log(error);
      }
    )
  }
}
