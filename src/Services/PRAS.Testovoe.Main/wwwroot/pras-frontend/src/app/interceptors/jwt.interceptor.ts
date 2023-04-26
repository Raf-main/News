import { LoginResponse } from './../models/response/loginResponse';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, switchMap, throwError } from 'rxjs';
import { AuthService } from '../services/auth/authentication.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  private isRefreshing = false;

  constructor(private authService: AuthService) {

  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>>
  {
    if (this.authService.isAuthenticated()){
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.authService.getToken()}`
        }
      });
    }

    return next.handle(request).pipe(catchError(error => {
      const isError = error instanceof HttpErrorResponse;
      const isStatus401 = error.status === 401;

      if(isError && isStatus401)
      {
        return this.handle401(request,next);
      }

      return throwError(() => new Error(error));
    }))
  }

  handle401(request: HttpRequest<unknown>, next: HttpHandler)
  {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
    }

    return this.authService.refreshToken().pipe(
      switchMap((response: LoginResponse) => {
        this.isRefreshing = false;

        this.authService.saveToken(response.accessToken);
        this.authService.saveUser(response.user);

        return next.handle(request);
      }),
      catchError((error) => {
        this.isRefreshing = false;

        if (error.status == '403') {
          this.authService.logout;
        }

        return throwError(() => new Error(error));
      })
   );
  }
}
