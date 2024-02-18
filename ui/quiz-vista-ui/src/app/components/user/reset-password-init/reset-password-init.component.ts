import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ErrorHandlerService } from 'src/app/services/error-handler.service';
import { UserHttpService } from 'src/app/services/http/user-http-service';

@Component({
  selector: 'app-reset-password-init',
  templateUrl: './reset-password-init.component.html',
  styleUrls: ['./reset-password-init.component.css']
})
export class ResetPasswordInitComponent {
  updateSuccess = false;
  backendErrorMessages: any[]=[];
  submitted = false;

    resetPasswordData: any = {
      email: ''
    }

  constructor(private userHttpService: UserHttpService, private errorHandlerService:ErrorHandlerService) { }


  onSubmit(resetPasswordForm: NgForm): void {
    this.submitted= true;
    if (resetPasswordForm.valid) {
      this.userHttpService.resetPasswordInit(this.resetPasswordData.email).subscribe(
        response => {
          if(response.isValid===true){
            this.updateSuccess=true;
          }
          else{
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
