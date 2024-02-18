import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth-service';
import { UserHttpService } from 'src/app/services/http/user-http-service';

@Component({
  selector: 'app-users-roles',
  templateUrl: './users-roles.component.html',
  styleUrls: ['./users-roles.component.css']
})
export class UsersRolesComponent {
  users: any[] = [];
  filteredUsers: any[] = [];
  searchTerm: string = '';
  updateSuccess = false;
  currentPage: number = 1;
  itemsPerPage: number = 10; 

  constructor(private authService:AuthService, private userHttpService:UserHttpService){};

  ngOnInit(): void {
    this.loadUsers();
  }


  private loadUsers(): void {
    this.userHttpService.showUsers().subscribe(
      (data) => {
        this.users = data.model;
        this.filterUsers();
      },
      (error) => {
        console.error('Error fetching users', error);
      }
    );
  }

  filterUsers(): void {
    this.currentPage=1;
    this.applyUserFilterAndPagination();
  }


IsUserLogged() {
    return this.authService.isUserLoggedIn(); 
  }
hasRole(roles: any[], roleName: string): string {
    const role = roles.find(r => r.name === roleName);
    return role;
}

toggleRole(userName: string, roleName: string): void {
  this.userHttpService.toggleRole(userName, roleName).subscribe(
    response => {
      console.log(response);
      this.refreshUsers();
      this.updateSuccess = true;
      setTimeout(() => this.updateSuccess = false, 5000);

    },
    error => {
      console.error('Error toggling role', error);
    }
  );
}

private refreshUsers(): void {
  this.userHttpService.showUsers().subscribe(
    (data) => {
      this.users = data.model;
      this.applyUserFilterAndPagination();
    },
    (error) => {
      console.error('Error refreshing users', error);
    }
  );
}

private applyUserFilterAndPagination(): void {
  const filtered = this.searchTerm ? this.users.filter(user => 
    user.userName.toLowerCase().includes(this.searchTerm.toLowerCase())) : this.users;

  const startIndex = (this.currentPage - 1) * this.itemsPerPage;
  const endIndex = startIndex + this.itemsPerPage;
  this.filteredUsers = filtered.slice(startIndex, endIndex);
}

changePage(step: number): void {
  const newPage = this.currentPage + step;
  const filteredLength = this.searchTerm ? this.users.filter(user => 
    user.userName.toLowerCase().includes(this.searchTerm.toLowerCase())).length : this.users.length;
  const totalPages = Math.ceil(filteredLength / this.itemsPerPage);

  if (newPage > 0 && newPage <= totalPages) {
    this.currentPage = newPage;
    this.applyUserFilterAndPagination();
  }
}
}
