import { Response, Http, Headers } from '@angular/http'
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { NotificationService } from './notification.service';

@Injectable({ providedIn: 'root'})
export class AuthService{

    private logOutAddress: string = `http://localhost:52295/Account/Logout`;

    constructor(private http: Http, private notificationService : NotificationService) { }

    logIn(response: Response) : void {

        let responseJson = response.json();
        let accessToken = responseJson.access_token;
        let role = response.headers.get('Role');
        let id = response.headers.get('UserId');

        let authdata = new AuthData(accessToken, role, id);
        localStorage.setItem('token', JSON.stringify(authdata));
        console.log(authdata);
    }

    logOut(): Observable<any> {       
        if(this.isLoggedIn() === true) {
            let token = localStorage.getItem("token");

            let headers = new Headers();
            headers.append('Content-type', 'application/x-www-form-urlencoded');
            headers.append('Authorization', 'Bearer ' + JSON.parse(token).token);

            localStorage.removeItem('token');
            this.notificationService.sessionEvent.emit(false);

            return this.http.post(this.logOutAddress, "", { headers: headers });
        }
    }

    isLoggedIn(): boolean {
        if(!localStorage.getItem('token'))
            return false;
        else
            return true;
    }

    isAdmin(): boolean {
        if(!this.isLoggedIn()) {
            return false;
        }

        let token = localStorage.getItem('token');
        let role = JSON.parse(token).role;

        if (role=="Admin") {
            return true;
        } else {
            return false;
        }
    }

    isKontrolor(): boolean {
        if(!this.isLoggedIn()) {
            return false;
        }

        let token = localStorage.getItem('token');
        let role = JSON.parse(token).role;

        if (role=="Kontrolor") {
            return true;
        } else {
            return false;
        }
    }

    isKorisnik(): boolean {
        if(!this.isLoggedIn()) {
            return false;
        }

        let token = localStorage.getItem('token');
        let role = JSON.parse(token).role;

        if (role=="Korisnik") {
            return true;
        } else {
            return false;
        }
    }
    
}

export class AuthData {
    
    token: string;
    role: string;
    id: string;

    constructor(token:string, role: string, id: string) {
        this.token = token;
        this.role = role;
        this.id = id;
     }
}