import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  AbstractControl,
  AsyncValidatorFn,
  ValidationErrors,
} from '@angular/forms';
import { catchError, debounceTime, map, Observable, of } from 'rxjs';
import { enviroment } from '../../../enviroment/enviroment';

@Injectable({
  providedIn: 'root',
})
export class CheckEmailService {
  constructor(private http: HttpClient) {}

  // Async Validator to check if the email is used
  emailAsyncValidator(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      if (!control.value) {
        return of(null); // No email, so no validation
      }

      const email = control.value;

      // Call the API to check if the email is used
      return this.http
        .get<boolean>(
          `${enviroment.apiUrl}/api/account/isEmailUsed?email=${email}`
        )
        .pipe(
          debounceTime(500), // Add a small delay to prevent multiple rapid requests
          map((isUsed) => (isUsed ? { emailUsed: true } : null)), // Return error if email is used
          catchError(() => of(null)) // Handle errors (e.g., network issues) gracefully
        );
    };
  }
}
