import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Korisnik } from '../models/korisnik';
import { catchError } from 'rxjs/operators';
import { ChangePassModel } from '../models/changePassModel';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json'  })
  };

@Injectable({ providedIn: 'root' })
export class ProfilService{

    private GetUserUrl = 'http://localhost:52295/api/Account/GetUser';
    private EditUserUrl = 'http://localhost:52295/api/Account/EditUser';
    private ChangePassUrl = 'http://localhost:52295/api/Account/ChangePassword';
    constructor(private http: HttpClient, private router: Router){}

   getUser(username:string) : Observable<Korisnik> {
        return this.http.get<Korisnik>(`${this.GetUserUrl}?username=${username}`).pipe(
            catchError(this.handleError<Korisnik>(`getTermini`)
        ));
   }

   editProfile(user: Korisnik) : Observable<any> {
      return this.http.put(this.EditUserUrl, user, httpOptions).pipe(catchError(this.handleError<any>('EditUser')));
   }

   changePassword(passwordModel : ChangePassModel) : Observable<any>{
    return this.http.post(this.ChangePassUrl, passwordModel, httpOptions).pipe(catchError(this.handleError<any>('ChangePassword')));
   }
   
   private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      return of(result as T);
    };
}
    
}