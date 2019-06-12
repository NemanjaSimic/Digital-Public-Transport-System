import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { catchError } from 'rxjs/operators';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' })
  };

@Injectable({ providedIn: 'root' })
export class KartaService{
    private KupiKartuNeregistrovaniUrl = 'http://localhost:52295/api/Karta/PostKarta';

    constructor(private http: HttpClient) { }

    kupiKartuNeregistrovani(email: any) : Observable<any>{
        return this.http.post<any>(this.KupiKartuNeregistrovaniUrl, email, httpOptions);
    }

    private handleError<T> (operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
          return of(result as T);
        };
    }
}