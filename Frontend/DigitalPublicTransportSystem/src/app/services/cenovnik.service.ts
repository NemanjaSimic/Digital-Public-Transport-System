import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Stavka } from '../models/cenovnik';
import { catchError } from 'rxjs/operators';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type' : 'application/json'})
};

@Injectable({ providedIn: 'root'})
export class CenovnikService{

    private GetCenovnikUrl = 'http://localhost:52295/api/Cenovnik/GetCenovnik';

    constructor(private http: HttpClient) {}

    getCenovnik(): Observable<Stavka[]>{
        return this.http.get<Stavka[]>(this.GetCenovnikUrl).pipe(
            catchError(this.handleError<Stavka[]>(`getCenovnik`))
        );
    }

    izmeniStavku(stavka:any):void{
        
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