import { Injectable } from '@angular/core';
import { HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { BehaviorSubject, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ErrorsService {
  // Observables
  private messageError = new BehaviorSubject<string>('');
  messageError$ = this.messageError.asObservable();

  constructor() { }

  handleErrorMessage = (error: HttpErrorResponse) => {
    if (error.status !== 0) {
      if (error.status === HttpStatusCode.BadRequest) {
        this.messageError.next(error.error.message);
        return throwError(() => (error.error.message));
      }
      if (error.status === HttpStatusCode.Unauthorized) {
        this.messageError.next(error.error.message);
        return throwError(() => (error.error.message));
      }
      if (error.status === HttpStatusCode.NotFound) {
        this.messageError.next('Endpoint not found.');
        return throwError(() => ('Endpoint not found.'));
      }
      if (error.status === HttpStatusCode.InternalServerError) {
        this.messageError.next(error.error.message);
        return throwError(() => (error.error.message));
      }
    }

    this.messageError.next('There is a connection error. Please, try later.');
    return throwError(() => ('There is a connection error. Please, try later.'));
  }

  clearErrorMessages() {
    this.messageError.next('');
  }
}
