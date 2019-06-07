import {Http, Headers} from '@angular/http'
import { Injectable } from  '@angular/core'
import { Observable } from 'rxjs';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { NotificationService } from './notification.service';

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

    constructor(private httpClient : HttpClient,private notificationService: NotificationService) { }

    logIn(email: string, password: string): Observable<any>
    {    
        const headers = new Headers({'Content-Type': 'application/x-www-form-urlencoded', 'Accept': 'application/json'});
        const query = `username=${email}&password=${password}&grant_type=password`;
        const httpOptions = {
            headers: new HttpHeaders({ 'Content-Type': 'application/json' })
          };
          this.log.password = password;
          this.log.username = email;
        // return this.http.post(this.apiAddress, query, { headers: headers });
        return this.httpClient.post<LOG>(this.apiAddress, query, httpOptions);
    }

    logOut(): Observable<any>{

        if(this.isLoggedIn() === true) {
            let token = localStorage.getItem("token");

            let header = new HttpHeaders();
            header.append('Content-type', 'application/x-www-form-urlencoded');
            header.append('Authorization', 'Bearer ' + token);
            const httpOptions = {
                headers: header
              };
            localStorage.clear();

            this.notificationService.sessionEvent.emit(false);

            return this.httpClient.post<any>(this.logOutAddress, "", httpOptions );
        }
     
        
    }

    isLoggedIn(): boolean{
        if(localStorage.getItem("token") !== null)
            return true;
        else
            return false;
    }
}