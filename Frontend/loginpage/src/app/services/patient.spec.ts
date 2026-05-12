import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { PatientService } from './patientService';
import { environment } from '../environments/environment';
import { Component } from '@angular/core';
import { of } from 'rxjs';

describe('PatientService', () => {

  let service: PatientService;
  let httpMock: HttpTestingController;

  beforeEach(() => {

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });

    service = TestBed.inject(PatientService);
    httpMock = TestBed.inject(HttpTestingController);

  });

  it('should fetch patients', () => {

    const dummyPatients = [
      {
        patientId: 1,
      }
    ];

    service.getAll().subscribe(res => {
      expect(res.length).toBe(1);
      expect(res).toEqual(dummyPatients);
    });

    const req = httpMock.expectOne(`${environment.apiUrl}/patient`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyPatients);

  });

  it('should create a patient', () => {

    const newPatient = {
      firstName: 'Ashfaq',
      lastName: 'Doe',
      gender: 'Male',
      dateOfBirth: '1990-01-01',
      city: 'New York',
      email: 'ashfaq@example.com',
      phoneNumber: '1234567890'
    };

    service.createPatient(newPatient).subscribe(res => {
      expect(res).toBe('Patient created successfully');
    });

    const req = httpMock.expectOne(`${environment.apiUrl}/patient`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(newPatient);
    req.flush('Patient created successfully', { status: 200, statusText: 'OK' });

  });

  it('should update a patient', () => {

    const updatedPatient = {
      firstName: 'Ashfaq',
      lastName: 'Ansari',
      gender: 'Male',
      dateOfBirth: '1990-01-01',
      city: 'Lahore',
      email: 'ashfaq@example.com',
      phoneNumber: '0987654321'
    };

    service.updatePatient(1, updatedPatient).subscribe(res => {
      expect(res).toBe('Patient updated successfully');
    });

    const req = httpMock.expectOne(`${environment.apiUrl}/patient/1`);
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(updatedPatient);
    req.flush('Patient updated successfully', { status: 200, statusText: 'OK' });

  });

  it('should delete patient by id', () => {

    service.deletePatient(1).subscribe(res => {
      expect(res).toBeTruthy();
    });

    const req = httpMock.expectOne(`${environment.apiUrl}/patient/1`);

    expect(req.request.method).toBe('DELETE');

    req.flush({});
  });

});
