import { Injectable } from "@angular/core";
import { Router } from '@angular/router';
import { jwtDecode } from "jwt-decode";
import { JwtPayload } from 'jsonwebtoken';

//import jwt_decode from 'jwt-decode';

interface CustomJwtPayload {
    'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name': string;
    'http://schemas.microsoft.com/ws/2008/06/identity/claims/role': string[];
    exp: number;
  }

@Injectable({
    providedIn: 'root'
})

export class AuthService{
    
    constructor(private router: Router) { }

    isUserLoggedIn(): boolean{
        return !!localStorage.getItem('jwtToken');
    }


    login(token: string): void {
        localStorage.setItem('jwtToken', token);
    }

    logout(): void {
        localStorage.removeItem('jwtToken')
        this.router.navigate(['/home'])
    }

    getJWTToken(): string{
        return localStorage.getItem('jwtToken')??'';
    }
    isRole(role: string): boolean {
        const token = this.getJWTToken();
        if (!token) return false;

        try {
            const decodedToken: CustomJwtPayload = jwtDecode<CustomJwtPayload>(token);
            return decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'].includes(role);
        } catch (error) {
            console.error('Error decoding JWT:', error);
            return false;
        }
    }

    isAdmin(): boolean {
        return this.isRole('Admin');
    }

    isModerator(): boolean {
        return this.isRole('Moderator');
    }

    isUser(): boolean {
        return this.isRole('User');
    }

    
}