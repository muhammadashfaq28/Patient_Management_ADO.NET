import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  const router = inject(Router);
  const token = localStorage.getItem('token');

  let request = req;


  if (token) {
    request = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  return next(request).pipe(
    catchError((error) => {


      if (error.status === 401) {
        localStorage.removeItem('token');

        alert('Session expired. Please login again.');

        router.navigate(['/login']);
      }


      else if (error.status === 500) {
        alert('Server error. Please try again later.');
      }

  
      else {
        console.error('Error:', error);
      }

      return throwError(() => error);
    })
  );
};