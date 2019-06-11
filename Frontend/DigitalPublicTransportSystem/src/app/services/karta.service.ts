import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { catchError } from 'rxjs/operators';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({ providedIn: 'root' })
export class KartaService{
    private KupiKartuNeregistrovaniUrl = 'http://localhost:52295/api/Karta/PostNeregKarta';

    constructor(private http: HttpClient) { }

    kupiKartuNeregistrovani(email: string) : Observable<any>{
        return this.http.post<string>(this.KupiKartuNeregistrovaniUrl, email, httpOptions);
    }

    private handleError<T> (operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
          return of(result as T);
        };
    }
}