import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { Linija } from '../models/linija';
import { NovaLinija } from '../models/nova-linija';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type' : 'application/json'})
};

@Injectable({ providedIn: 'root' })
export class LinijaService{
  private DeleteLinijaUrl = 'http://localhost:52295/api/Linija/DeleteLinija';
  private PutLinijaUrl = 'http://localhost:52295/api/Linija/PutLinija';
  private GetLinijaUrl = 'http://localhost:52295/api/Linija/GetLinija';
  private GetLinijeByTipUrl = 'http://localhost:52295/api/Linija/GetLinijeByTip';
  private GetTerminiOfLinijaUrl = 'http://localhost:52295/api/Linija/GetTerminiOfLinija';
  private PostLinijaUrl = 'http://localhost:52295/api/Linija/PostLinija';
  private GetStaniceOfLinijaUrl = 'http://localhost:52295/api/Linija/GetStaniceOfLinija';

    constructor(private http: HttpClient) { }

  getLinija(name: string): Observable<any>{
    return this.http.get(`${this.GetLinijaUrl}?name=${name}`);
  }

  getLinijeByTip (tip : any): Observable<Array<string>> {
      return this.http.get<Array<string>>(`${this.GetLinijeByTipUrl}?TipVoznje=${tip}`);
  }

  getTerminiOfLinija(ime:any,dan:any):Observable<Array<string>>{
    return this.http.get<Array<string>>(`${this.GetTerminiOfLinijaUrl}?Ime=${ime}&Dan=${dan}`);
  }

  getStaniceOfLinija(ime:any): Observable<any>{
    return this.http.get(`${this.GetStaniceOfLinijaUrl}?ime=${ime}`);
  }

  napraviLiniju(linija:any):Observable<any>{
    return this.http.post<any>(this.PostLinijaUrl, linija);
  }

  izmeniLiniju(linija:any):Observable<any>{
    return this.http.put<any>(this.PutLinijaUrl, linija);
  }

  izbrisiLiniju(ime: string): Observable<any>{
    return this.http.delete<any>(`${this.DeleteLinijaUrl}?ime=${ime}`);
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