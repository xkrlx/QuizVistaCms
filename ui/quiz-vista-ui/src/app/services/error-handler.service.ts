import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {

  constructor() { }

  public handleError(errorResponse: any): string[] {
    let errorMessages = [];
    if (errorResponse && errorResponse.error && errorResponse.error.errors) {
      Object.keys(errorResponse.error.errors).forEach(key => {
        const errorsForKey = errorResponse.error.errors[key];
        if (Array.isArray(errorsForKey)) {
          errorsForKey.forEach((message: string) => {
            errorMessages.push(`${message}`);
          });
        } else {
          errorMessages.push(`${key}: ${errorsForKey}`);
        }
      });
    } else {
      errorMessages.push('An unexpected error occurred.');
    }
    return errorMessages;
  }
  
}
