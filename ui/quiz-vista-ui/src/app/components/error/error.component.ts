// error.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.css']
})

export class ErrorComponent implements OnInit {
  errorCode: string = '';
  errorMessage: string ='';

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.errorCode = this.route.snapshot.paramMap.get('code') as string;
    this.errorMessage = this.getErrorMessage(this.errorCode);
  }

  getErrorMessage(code: string): string {
    switch (code) {
      case '404':
        return 'Page not found.';
      case '403':
        return 'Access denied.';
      case '401':
        return 'Unauthorized';
      case '500':
        return 'Internal server error.';
      default:
        return 'An unknown error occurred.';
    }
  }
}
