import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ErrorHandlerService } from 'src/app/services/error-handler.service';
import { UserHttpService } from 'src/app/services/http/user-http-service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent {
    resetPasswordData: any = {
      token: '',
      password: '',
      confirmPassword: ''
    }
    updateSuccess = false;
    backendErrorMessages: any[]=[];
    submitted = false;

  constructor(private userHttpService: UserHttpService, private errorHandlerService:ErrorHandlerService,private router:Router) { }


  onSubmit(resetPasswordForm: NgForm): void {
    this.submitted= true;
    if (resetPasswordForm.valid) {
      this.userHttpService.resetPassword(this.resetPasswordData.token,this.resetPasswordData.password,this.resetPasswordData.confirmPassword).subscribe(
        response => {
          if(response.isValid===true){
            this.updateSuccess=true;
            setTimeout(() => {
              this.router.navigate(['/login']);
          }, 5000); 
          }
          else if(response.isValid===false){
            this.backendErrorMessages=[response.errorMessage]
          }

        },
        error => {
          this.backendErrorMessages = this.errorHandlerService.handleError(error);
        }
      );
    }
  }
}
