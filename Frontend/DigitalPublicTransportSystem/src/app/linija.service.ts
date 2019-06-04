import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { Linija } from './linija';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type' : 'application/json'})
};

@Injectable({ providedIn: 'root' })
export class LinijaService{

    private GetLinijeByTipUrl = 'http://localhost:52295/api/Linija/GetLinijeByTip';
    private GetTerminiOfLinijaUrl = 'http://localhost:52295/api/Linija/GetTerminiOfLinija';

    constructor(private http: HttpClient) { }

    getLinijeByTip (tip : any): Observable<Array<string>> {
      return this.http.get<Array<string>>(`${this.GetLinijeByTipUrl}?TipVoznje=${tip}`).pipe(
          catchError(this.handleError<Array<string>>(`getLinija`))
        );   
  }

  getTerminiOfLinija(ime:any,dan:any):Observable<Array<string>>{
    return this.http.get<Array<string>>(`${this.GetTerminiOfLinijaUrl}?Ime=${ime}&Dan=${dan}`).pipe(
      catchError(this.handleError<Array<string>>(`getTermini`))
    );   
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