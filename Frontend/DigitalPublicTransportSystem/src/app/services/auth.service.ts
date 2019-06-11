import { Response, Http, Headers } from '@angular/http'
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { NotificationService } from './notification.service';

@Injectable({ providedIn: 'root'})
export class AuthService{

    constructor(private notificationService : NotificationService) { }

    logIn(response: any) : void {

        // let responseJson = response.json();
        // let accessToken = response.access_token;
        const jwt =  response.access_token;


        let jwtData = jwt.split('.')[1]
        let decodedJwtJsonData = window.atob(jwtData)
        let decodedJwtData = JSON.parse(decodedJwtJsonData)

        let role = decodedJwtData.role
        let userId =  decodedJwtData.unique_name;

        localStorage.setItem('token', jwt)
        localStorage.setItem('role', role);
        localStorage.setItem('userId', userId);
        // let authdata = new AuthData(accessToken, role, id);
        // localStorage.setItem('token', JSON.stringify(authdata));
        // console.log(authdata);
    }

    logOut(): void {       
        // if(this.isLoggedIn() === true) {
        //     let token = localStorage.getItem("token");

        //     let headers = new Headers();
        //     headers.append('Content-type', 'application/x-www-form-urlencoded');
        //     headers.append('Authorization', 'Bearer ' +token);

        //     localStorage.clear();

        //     this.notificationService.sessionEvent.emit(false);

        //     return this.http.post(this.logOutAddress, "", { headers: headers });
        // }
        localStorage.removeItem("jwt");
        localStorage.removeItem("role");
        localStorage.removeItem("userId");
    }

    isLoggedIn(): boolean {
        if(!localStorage.getItem('jwt'))
            return false;
        else
            return true;
    }

    isAdmin(): boolean {
        if(!this.isLoggedIn()) {
            return false;
        }

        let role = localStorage.getItem('role');
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

        let role = localStorage.getItem('role');

        if (role=="Controller") {
            return true;
        } else {
            return false;
        }
    }

    isKorisnik(): boolean {
        if(!this.isLoggedIn()) {
            return false;
        }

        let role = localStorage.getItem('role');

        if (role=="AppUser") {
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