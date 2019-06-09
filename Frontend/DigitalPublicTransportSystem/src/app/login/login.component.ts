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

  username:any;
  password:any;

  loginForm = this.fb.group({
    username : [this.username, Validators.required],
    // email : [this.email, [Validators.required, Validators.email]],
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
    
    this.username = this.loginForm.get('username').value;
    this.password = this.loginForm.get('password').value;

    this.loginService.logIn(this.username,this.password);

  }
}
