import { Injectable } from '@angular/core';
import { HttpErrorResponse, HttpRequest, HttpInterceptor, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { getSession } from '../session/sessionService';

@Injectable()
export class HttpClientInterceptor implements HttpInterceptor {
  apiUrl = '';
  constructor(private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    request = request.clone({ url: environment.apiUrl + request.url });

    // const token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImRldkBleGFtcGxlLmNvbSIsImdpdmVuX25hbWUiOiJkZXYiLCJuYmYiOjE3MTExNjY1MDEsImV4cCI6MTcxMTc3MTMwMSwiaWF0IjoxNzExMTY2NTAxLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUyNDYiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUyNDYifQ.Waqs-vd-p3SzDNp6WqsOC_JOcplmzLxCbTSO13JCjno';
    const user = getSession('user');

    if (user) {
      // this.router.navigate(['/auth']);
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${user.token}`,
        },
      });
    }



    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          this.router.navigate(['/auth']);
        }
        return throwError(error);
      })
    );
  }

  getApiUrl() {
    if (this.apiUrl) {
      return this.apiUrl;
    }

    this.apiUrl = environment.apiUrl;
    return this.apiUrl;
  }

  addToken(req: HttpRequest<any>, token: string): HttpRequest<any> {
    if (token) {
      return req.clone({ setHeaders: { Authorization: token } });
    } else {
      return req.clone({});
    }
  }
}
