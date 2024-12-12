import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, Observable, of, throwError } from 'rxjs';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
  providedIn: 'root',
})
export class AuthInterceptorService implements HttpInterceptor {
  constructor(
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (!isPlatformBrowser(this.platformId)) {
      return of();
    }
    const authReq = this.addTokenToRequest(req);

    return next
      .handle(authReq)
      .pipe(
        catchError((error: HttpErrorResponse) => this.handleAuthError(error))
      );
  }

  private addTokenToRequest(req: HttpRequest<any>): HttpRequest<any> {
    if (typeof window !== 'undefined' && window.localStorage.getItem('token')) {
      const token = window.localStorage.getItem('token');
      if (token) {
        return req.clone({
          setHeaders: { Authorization: `Bearer ${token}` },
        });
      }
    }
    return req;
  }

  private handleAuthError(error: HttpErrorResponse): Observable<never> {
    console.log(error);
    if (error.status === 401) {
      if (
        typeof window !== 'undefined' &&
        window.localStorage.getItem('token')
      ) {
        window.localStorage.removeItem('token');
      }
      this.router.navigate(['/login']);
    }
    return throwError(error);
  }
}
