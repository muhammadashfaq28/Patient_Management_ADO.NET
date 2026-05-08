import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class Auth {


  private apiUrl = environment.apiUrl + '/auth';

  constructor(private http: HttpClient) {}

  login(userName: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, {
      userName,
      password
    });
  }
  
}
