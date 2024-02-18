import { Component } from '@angular/core';
import { UserHttpService } from 'src/app/services/http/user-http-service';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent {
    User: any;

  ngOnInit(): void {
    this.loadUser();
  }
  constructor(private userHttpService:UserHttpService){};

  private loadUser(): void {
    this.userHttpService.showUserDetails().subscribe(
      (data) => {
        this.User = data.model;
      },
      (error) => {
        console.error('Error fetching users', error);
      }
    );
  }
}
