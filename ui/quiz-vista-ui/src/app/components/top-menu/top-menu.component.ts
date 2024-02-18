import { Component } from '@angular/core';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.css']
})
export class TopMenuComponent {

  constructor( private authService: AuthService) { }

  IsUserLogged(): boolean{
    return this.authService.isUserLoggedIn();
  }

  IsAdmin(): boolean{
    return this.authService.isAdmin();
  }

  IsModerator(): boolean{
    return this.authService.isModerator();
  }

  Logout(): void{
    this.authService.logout();
  }

}
