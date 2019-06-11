import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { NotificationService } from '../services/notification.service';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private notificationService:NotificationService, private authService:AuthService, private loginService: LoginService,
    private router: Router){}

  IsLoggedIn : boolean;
  IsAdmin : boolean;
  isKontrolor: boolean;
  IsAppUser: boolean;

  ngOnInit() {
    this.loadData();
  }

  LogOut() : void{
<<<<<<< HEAD
      this.loginService.logOut()
=======
      this.loginService.logOut();
>>>>>>> 570391e6b62d9162d62a41a0b2abbf78d5170fe9
  }

  loadData() : void{
    this.notificationService.sessionEvent.subscribe((loggedIn : boolean) => {
      this.IsLoggedIn = this.authService.isLoggedIn();
      this.IsAdmin = this.authService.isAdmin();
      this.isKontrolor = this.authService.isKontrolor();
      this.IsAppUser = this.authService.isKorisnik();
  });
  this.isKontrolor = this.authService.isKontrolor();
  this.IsLoggedIn = this.authService.isLoggedIn();
  this.IsAdmin = this.authService.isAdmin();
  this.IsAppUser = this.authService.isKorisnik();
  }
}
