import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth-service';
import { UserHttpService } from 'src/app/services/http/user-http-service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent {
  users: any[] = [];
  filteredUsers: any[] = [];
  searchTerm: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 10; 

  constructor(private userHttpService:UserHttpService, private authService:AuthService){};

  ngOnInit(): void {
    this.loadUsers();
  }

private loadUsers():void{
  this.userHttpService.showUsers().subscribe(
    (data) => {
      console.log(data);
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