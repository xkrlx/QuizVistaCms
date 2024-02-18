import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/models/user';
import { UserHttpService } from 'src/app/services/http/user-http-service';


@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {
  user!: User;
  userId: string = '';
  updateSuccess = false;
  errors: string[] = [];

  constructor(private userHttpService: UserHttpService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.userId = params['id'];
      this.getUserData();
    });
    this.getUserData();
  }

  getUserData(): void {
    this.userHttpService.showUser(this.userId).subscribe(
      (userData: any) => {
        console.log(userData);
        this.user = userData.model;
      },
      error => {
        console.error('Error fetching user:', error);
      }
    );
  }

  updateUser(): void {
    this.userHttpService.update(this.user).subscribe(
      response => {
        console.log('User updated successfully', response);
        this.updateSuccess = true;
        this.errors = [];
        setTimeout(() => this.updateSuccess = false, 5000);
      },
      error => {
        this.updateSuccess = false;
        if (error.error && error.error.errors) {
          this.errors = Object.keys(error.error.errors).flatMap(k => error.error.errors[k]);
        } else {
          this.errors = ['Wystąpił błąd podczas aktualizacji danych użytkownika.'];
        }
      }
    );
  }
}