import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiConfig } from '../../config/api-config';
import { User } from 'src/app/models/user';

@Injectable({
  providedIn: 'root'
})

export class UserHttpService {

  url: string =  `${ApiConfig.url}/User`;

  constructor(private http: HttpClient) { }

  register(user: User): Observable<any>{
    return this.http.post(`${this.url}/register`, user)
  }

  login(userName: string, password: string): Observable<any>{
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(`${this.url}/login`, {
      "userName": userName,
      "password": password,
      "firstName": "string",
      "lastName": "string",
      "email": "user@example.com"
    },{headers})
  }

  toggleRole(userName: string, roleName: string): Observable<any> {
    return this.http.post(`${this.url}/toggle-role`,{
        userName: userName,
        roleName: roleName
    })
  }

  update(user: User): Observable<any> {
    return this.http.put(`${this.url}/edit`, user);
  }
  
  showUsers():Observable<any>{
    return this.http.get(`${this.url}/showusers`);
  }
  showUser(userId: string): Observable<any> {
    return this.http.get(`${this.url}/showuser/${userId}`);
  }

  changePassword(currentPassword: string, newPassword: string, ConfirmNewPassword: string): Observable<any>{
    return this.http.post(`${this.url}/changepassword`,{
      currentPassword: currentPassword,
      newPassword: newPassword,
      ConfirmNewPassword: ConfirmNewPassword
  })
  }

  showUserDetails():Observable<any>{
    return this.http.get(`${this.url}/details`);
  }

  resetPasswordInit(email:string):Observable<any>{
    return this.http.post(`${this.url}/reset-password-init`,{
      email:email
    })
  }

  resetPassword(token: string, password: string, ConfirmNewPassword: string): Observable<any>{
    return this.http.post(`${this.url}/reset-password`,{
      token: token,
      Password: password,
      ConfirmPassword: ConfirmNewPassword
  })
  }


}
