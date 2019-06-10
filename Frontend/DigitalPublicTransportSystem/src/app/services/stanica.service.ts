import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Stanica } from '../models/stanica';
import { Observable } from 'rxjs';



@Injectable({ providedIn: 'root'})
export class StanicaService{
    private PostStanicaUrl = 'http://localhost:52295/api/Stanica/PostStanica';
    private GetStanicaUrl = 'http://localhost:52295/api/Stanica/GetStanica';
    private PutStanicaUrl = 'http://localhost:52295/api/Stanica/PutStanica';
    constructor(private http: HttpClient) {}

    postStanica(stanica: Stanica): Observable<any>{
        return this.http.post(this.PostStanicaUrl, stanica);
    }

    getStanica(): Observable<any>{
        return this.http.get(this.GetStanicaUrl);
    }
    
    putStanica(stanica: Stanica){
        return this.http.put(this.PutStanicaUrl, stanica);
    }
}