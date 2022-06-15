import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

// Environments
import { environment } from '@environments/environment';

// Models
import { Authorization } from '@models/authorization.model';

// Services
import { ErrorsService } from '@services/errors/errors.service';
import { TokenService } from '@services/token/token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl: string = `${environment.api_url}/aurora/api/identity/session`

  constructor(
    private http: HttpClient,
    private errorsService: ErrorsService,
    private tokenService: TokenService,
    private router: Router
  ) { }

  login(loginName: string, password: string) {
    return this.http.post<Authorization>(`${this.apiUrl}/login`, { loginName, password })
      .pipe(
        catchError((error) => this.errorsService.handleErrorMessage(error)),
        tap((token) => this.tokenService.setToken(token.accessToken))
      );
  }

  logout() {
    this.tokenService.removeToken();
    this.router.navigate(['login']);
  }
}
