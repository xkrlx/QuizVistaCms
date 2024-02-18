import { Component } from '@angular/core';
import { AuthService } from './services/auth-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent {
  title = 'quiz-vista-ui';

  constructor(private authService: AuthService){}

  IsUserLogged() {
    return this.authService.isUserLoggedIn(); 
  }
}
