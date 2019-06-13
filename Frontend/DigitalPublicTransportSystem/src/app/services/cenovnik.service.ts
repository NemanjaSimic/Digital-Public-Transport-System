import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest, HttpInterceptor, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Stavka } from '../models/cenovnik';
import { catchError, map } from 'rxjs/operators';
import { NoviCenovnik } from '../models/novi-cenovnik';


@Injectable({ providedIn: 'root'})
export class CenovnikService{

    private GetCenovnikUrl = 'http://localhost:52295/api/Cenovnik/GetCenovnik';
    private NapraviCenovnikUrl = 'http://localhost:52295/api/Cenovnik/NapraviCenovnik';

    constructor(private http: HttpClient) {}

    getCenovnik(): Observable<Stavka[]>{
        return this.http.get<Stavka[]>(this.GetCenovnikUrl);
    }

    napraviCenovnik(cenovnik: NoviCenovnik): Observable<any>{
        console.log(localStorage.jwt);
        return this.http.post<NoviCenovnik>(this.NapraviCenovnikUrl, cenovnik);
    }

      /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
     private handleError<T> (operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
            return of(result as T);
        };
    }
}
