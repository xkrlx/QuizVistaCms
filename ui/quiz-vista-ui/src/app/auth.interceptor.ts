import { Injectable } from '@angular/core';
import {
  HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse, HttpErrorResponse
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './services/auth-service';
import { Route, Router } from '@angular/router';
import { tap} from 'rxjs/operators'

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor( private authService: AuthService, private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const accessToken = this.authService.getJWTToken()
    
    if(accessToken){
        req = req.clone({
            url:  req.url,
            setHeaders: {
              Authorization: `bearer ${accessToken}`
            }
          });
    }
    
    return next.handle(req).pipe(
        tap(
          (event: HttpEvent<any>) => {
            if (event instanceof HttpResponse) {
              // Obsługa odpowiedzi HTTP
            }
          },
          (err: any) => {
            if (err instanceof HttpErrorResponse) {
              if (err.status === 401) {
                this.router.navigate(['/login']);
              }
            }
          }
        )
      );
    }
  }