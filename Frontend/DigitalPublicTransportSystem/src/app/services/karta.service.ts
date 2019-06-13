import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Karta } from '../models/karta';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' })
  };

@Injectable({ providedIn: 'root' })
export class KartaService{
    private KupiKartuNeregistrovaniUrl = 'http://localhost:52295/api/Karta/PostKartaNeregistrovani';
    private KupiKartuRegistrovaniUrl = 'http://localhost:52295/api/Account/PostKartaRegistrovani';
    private GetKarteKorisnikaUrl = 'http://localhost:52295/api/Account/GetKarteKorisnika/';
    private ValidateKartaUrl = 'http://localhost:52295/api/Karta/Validate';


    constructor(private http: HttpClient) { }

    kupiKartuNeregistrovani(email: any) : Observable<any>{
        return this.http.post<any>(this.KupiKartuNeregistrovaniUrl, email, httpOptions);
    }

    kupiKartuRegistrovani(data: any) : Observable<any>{
        return this.http.post<any>(this.KupiKartuRegistrovaniUrl, data);
    }


    getKarteKorisnika(username: string) : Observable<Karta[]>{
        return this.http.get<Karta[]>(this.GetKarteKorisnikaUrl+username);
    }

    validateKarta(id: any) : Observable<any>{
        return this.http.get<any>(`${this.ValidateKartaUrl}?ID=${id}`);
    }

    private handleError<T> (operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
          return of(result as T);
        };
    }
}