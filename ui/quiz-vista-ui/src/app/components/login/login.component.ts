import { Component } from '@angular/core';
import { UserHttpService } from 'src/app/services/http/user-http-service';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth-service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginData = {
    userName: '',
    password: ''
  };

  backendErrorMessage: string | null = null;

  constructor(private userHttpService: UserHttpService,
              private router: Router,
              private authService: AuthService) { }

  onSubmit(loginForm: NgForm): void {
    if (loginForm.valid) {
      this.userHttpService.login(this.loginData.userName, this.loginData.password).subscribe(
        response => {
          this.authService.login(response.model.token);
          this.router.navigate(['/quizez']);
        },
        error => {
          console.error('Login failed', error.error);
          this.backendErrorMessage = error.error.ErrorMessage;
        }
      );
    }
  }
}
