import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Korisnik } from '../models/korisnik';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http'
import { HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json'  })
  };

@Injectable({ providedIn: 'root' })
export class RegisterService{
    private UserRegisterUrl = 'http://localhost:52295/api/Account/Register';

    constructor(private http: HttpClient, private router: Router){}

    register(user: Korisnik): Observable<Korisnik> {
        return this.http.post<Korisnik>(this.UserRegisterUrl, user, httpOptions).pipe(catchError(this.handleError<any>('RegisterUser')));
    }

    private handleError<T> (operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
          return of(result as T);
        };
      }

}