import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Korisnik } from '../models/korisnik';
import { catchError } from 'rxjs/operators';
import { ChangePassModel } from '../models/changePassModel';
import { ReturnStatement } from '@angular/compiler';
import { RequestOptions } from '@angular/http';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json'  })
  };

  const httpOptionsImage = new RequestOptions();

@Injectable({ providedIn: 'root' })
export class ProfilService{

    private GetUserUrl = 'http://localhost:52295/api/Account/GetUser';
    private EditUserUrl = 'http://localhost:52295/api/Account/EditUser';
    private ChangePassUrl = 'http://localhost:52295/api/Account/ChangePassword';
    private UploadImageUrl = 'http://localhost:52295/api/Account/UploadImage/';
    private DownloadImageUrl = 'http://localhost:52295/api/Account/DownloadImage/';
    private GetUsersForValidationUrl = 'http://localhost:52295/api/Account/GetAllUsersForValidation';
    private ValidateUserUrl = 'http://localhost:52295/api/Account/ValidateUser';
    private DeactivateMyProfilUrl = 'http://localhost:52295/api/Account/DeactivateMyProfil';

    constructor(private http: HttpClient, private router: Router){}

   getUser(username:string) : Observable<Korisnik> {
        return this.http.get<Korisnik>(`${this.GetUserUrl}?username=${username}`).pipe(
            catchError(this.handleError<Korisnik>(`GetUser`)
        ));
   }

   editProfile(user: Korisnik) : Observable<any> {
      return this.http.put(this.EditUserUrl, user, httpOptions);
   }

   changePassword(passwordModel : ChangePassModel) : Observable<any>{
    return this.http.post(this.ChangePassUrl, passwordModel, httpOptions);
   }

   uploadImage(data: any, username: string) : Observable<any> {
     
    return this.http.post(this.UploadImageUrl + username, data);
   }

   downloadImage(username: string) : Observable<any>{
    return this.http.get(this.DownloadImageUrl + username);
   }

   getUsersForValidation() : Observable<any>{
    return this.http.get(this.GetUsersForValidationUrl);
   }

   validateUser(data: any) : Observable<any>{
     return this.http.put(this.ValidateUserUrl, data);
   }

   deactivateMyProfil(data: any) : Observable<any>{
     return this.http.post(this.DeactivateMyProfilUrl, data);
   }
   
   private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      return of(result as T);
    };
}
    
}