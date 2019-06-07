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

  ngOnInit() {
    this.loadData();
  }

  LogOut() : void{
    if(this.authService.isLoggedIn())
    {
      this.authService.logOut().subscribe(
        (response) => {
          this.IsLoggedIn = false;
          this.notificationService.notifyEvent.emit('You have successfully logged out.');
          this.loadData();
          this.router.navigate(['/']);
        },

        (error) => {
          this.notificationService.notifyEvent.emit('An error occured while logging out.');
          this.router.navigate(['/']);
        }
      )
    }
  }

  loadData() : void{
    this.notificationService.sessionEvent.subscribe((loggedIn : boolean) => {
      this.IsLoggedIn = loggedIn;
  });

  this.IsLoggedIn = this.authService.isLoggedIn();
  }
}
