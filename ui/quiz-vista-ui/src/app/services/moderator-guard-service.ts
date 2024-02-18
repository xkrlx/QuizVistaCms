import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from './auth-service';

@Injectable({
    providedIn: 'root'
})
export class ModeratorGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) {}

    canActivate(): boolean {
        if (this.authService.isModerator()) {
            return true;
        } else {
            this.router.navigate(['/error/401']);
            return false;
        }
    }
}
