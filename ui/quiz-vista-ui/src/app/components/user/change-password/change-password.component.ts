import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ErrorHandlerService } from 'src/app/services/error-handler.service';
import { UserHttpService } from 'src/app/services/http/user-http-service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent {

  changePasswordData = {
    currentPassword: '',
    newPassword: '',
    confirmNewPassword: ''
  };
  submitted = false;
  updateSuccess = false;
  backendErrorMessages: any[]=[];


  constructor(private userHttpService:UserHttpService, private errorHandlerService:ErrorHandlerService){}


  onSubmit(changePasswordForm: NgForm): void {
    this.submitted = true; 

    if (!changePasswordForm.valid) {
      return;
    }

    this.userHttpService.changePassword(this.changePasswordData.currentPassword, this.changePasswordData.newPassword, this.changePasswordData.confirmNewPassword).subscribe(
      response => {
        if(response.isValid===true){
        console.log("Działa")
        this.updateSuccess=true;
        setTimeout(() => this.updateSuccess = false, 5000);
        }
        else if(response.isValid===false){
          this.backendErrorMessages=[response.errorMessage]
        }
      },
      error => {
        console.error('Nie działa', error.error);
        this.backendErrorMessages = this.errorHandlerService.handleError(error);
      }
    );
  }
}
