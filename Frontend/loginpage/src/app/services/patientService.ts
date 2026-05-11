import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Patient } from '../models/PatientInterface';
import { CreatePatientDto } from '../models/CreatePatient';

@Injectable({
  providedIn: 'root',
})
export class PatientService {

  private apiUrl = environment.apiUrl + '/patient';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Patient[]> {
    return this.http.get<Patient[]>(this.apiUrl);
  }

  createPatient(data: CreatePatientDto): Observable<string> {
    return this.http.post(this.apiUrl, data, { responseType: 'text' });
  }

  updatePatient(id: number, data: CreatePatientDto) {
  return this.http.put(`${this.apiUrl}/${id}`, data);
}
}
