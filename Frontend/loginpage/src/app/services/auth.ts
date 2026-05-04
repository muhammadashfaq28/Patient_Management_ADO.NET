import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class Auth {


  private apiUrl = 'https://localhost:7004/api/auth';

  constructor(private http: HttpClient) {}

  login(userName: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, {
      userName,
      password
    });
  }
  
}
