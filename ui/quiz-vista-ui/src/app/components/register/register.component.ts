import { Component } from '@angular/core';
import { UserHttpService } from 'src/app/services/http/user-http-service';
import { User } from 'src/app/models/user'; // Import interfejsu User
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerData: User = {
    userName: '',
    password: '',
    firstName: '',
    lastName: '',
    email: ''
  };

  backendErrorMessage: string | null = null;

  constructor(private userHttpService: UserHttpService, private router: Router) { }

  onSubmit(registerForm: NgForm): void {
    if (registerForm.valid) {
      console.log(this.registerData)

      this.userHttpService.register(this.registerData).subscribe(
        response => {
          console.log('Register successful', response);
          this.router.navigate(['/login']);
        },
        error => {
          console.error('Register failed', error.error);
          this.backendErrorMessage = error.error.ErrorMessage;
        }
      );
    }
  }
}
