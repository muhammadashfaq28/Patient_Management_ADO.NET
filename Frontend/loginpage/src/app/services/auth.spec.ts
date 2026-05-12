import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { Auth } from './auth';
import { environment } from '../environments/environment';

describe('Auth', () => {

  let service: Auth;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [Auth]
    });

    service = TestBed.inject(Auth);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('Should Call Login API With Correct Data', () => {
    const dummyResponse = { token: '123' };

    service.login('admin', '123').subscribe(response => {
      expect(response).toEqual(dummyResponse);
    });

    const req = httpMock.expectOne(`${environment.apiUrl}/auth/login`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({ userName: 'admin', password: '123' });
    req.flush(dummyResponse);
  });

    it('should handle login failure', () => {

    service.login('wrong', 'wrong').subscribe({
      next: () => expect(null).toBeTruthy(),
      error: (err) => {
        expect(err.status).toBe(401);
      }
    });

    const req = httpMock.expectOne(`${environment.apiUrl}/auth/login`);

    req.flush('Unauthorized', { status: 401, statusText: 'Unauthorized' });
  });

  it('should handle empty login input', () => {

  service.login('', '').subscribe({
    error: () => {}
  });

  const req = httpMock.expectOne(`${environment.apiUrl}/auth/login`);

  req.flush('Bad Request', { status: 400, statusText: 'Bad Request' });
});

});