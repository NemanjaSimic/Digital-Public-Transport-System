import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  private submitted: boolean = false;

  email:any;
  password:any;

  loginForm = this.fb.group({
    email : [this.email, [Validators.required, Validators.email]],
    password : [this.password, Validators.required]  
  });
  y

  constructor(private notificationService: NotificationService, private loginService: LoginService, 
              private authService: AuthService,
              private fb: FormBuilder, private router: Router) { }

  get f() {return this.loginForm.controls}

  ngOnInit() {
    if(this.authService.isLoggedIn()) {
      this.notificationService.sessionEvent.emit(true);
      this.router.navigate(['/']);
    }
  }

  onLogin(): void {
    this.submitted = true;  
    
    this.email = this.loginForm.get('email').value;
    this.password = this.loginForm.get('password').value;

    this.loginService.logIn(this.email,this.password).subscribe( 
      (response) => { 
        this.authService.logIn(response);
        this.notificationService.sessionEvent.emit(true);
        this.router.navigate(['/']);      
      },

      (error) => {
        this.submitted = false; 
        this.notificationService.notifyEvent.emit('An error ocurred while trying to log in. The server is probably down.');
        console.log(error);
        if(error.status !== 0){
          let errorBody = JSON.parse(error._body);
          this.notificationService.notifyEvent.emit(errorBody.error_description);
        }        
      }
    );
  }
}
