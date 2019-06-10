import {Http, Headers} from '@angular/http'
import { Injectable } from  '@angular/core'
import { Observable } from 'rxjs';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { NotificationService } from './notification.service';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';

class LOG{
    username : string;
    password : string;
}

@Injectable({ providedIn: 'root'})
export class LoginService
{
    log:LOG = new LOG;
    private apiAddress: string = `http://localhost:52295/oauth/token`;
    private logOutAddress: string = `http://localhost:52295/api/Account/Logout`;

    constructor(private httpClient : HttpClient,private notificationService: NotificationService,
                private authService: AuthService,private router: Router) { }

    logIn(email: string, password: string)
    {    
        const query = `username=${email}&password=${password}&grant_type=password`;
        const httpOptions = {
            headers: new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' })
          };
        this.log.password = password;
        this.log.username = email;
        this.httpClient.post<any>(this.apiAddress, query, httpOptions).subscribe(
           (data) =>{
                    
            let jwt = data.access_token;

            let jwtData = jwt.split('.')[1]
            let decodedJwtJsonData = window.atob(jwtData)
            let decodedJwtData = JSON.parse(decodedJwtJsonData)

            let role = decodedJwtData.role
            let userId = decodedJwtData.unique_name

            console.log('jwtData: ' + jwtData)
            console.log('decodedJwtJsonData: ' + decodedJwtJsonData)
            console.log('decodedJwtData: ' + decodedJwtData)
            console.log('Role ' + role)

            localStorage.setItem('jwt', jwt)
            localStorage.setItem('role', role);
            localStorage.setItem('userId', userId);

            this.authService.logIn(data);
            this.notificationService.sessionEvent.emit(true);
            this.router.navigate(['/']);
            },
            (error) => {
                this.notificationService.notifyEvent.emit('An error ocurred while trying to log in. The server is probably down.');
                console.log(error);
                if(error.status !== 0){
                    let errorBody = JSON.parse(error._body);
                    this.notificationService.notifyEvent.emit(errorBody.error_description);
                }        
              }
        );
    }

    logOut(): Observable<any>{

        if(this.isLoggedIn() === true) {
            let token = localStorage.getItem("jwt");

            const httpOptions = {
                headers: new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' })
              };
            localStorage.clear();

            this.notificationService.sessionEvent.emit(false);

            return this.httpClient.post<any>(this.logOutAddress, token, httpOptions );
        }
     
        
    }

    isLoggedIn(): boolean{
        if(localStorage.getItem("token") !== null)
            return true;
        else
            return false;
    }
}