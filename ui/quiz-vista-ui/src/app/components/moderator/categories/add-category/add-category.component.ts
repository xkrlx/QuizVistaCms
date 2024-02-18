import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Category } from 'src/app/models/category';
import { CategoryHttpService } from 'src/app/services/http/category-http-service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent {
  category = new Category();
  createSuccess = false;
  errors: string[] = [];

  constructor(private categoryHttpService: CategoryHttpService, private router: Router) {}

  createCategory(): void {
    this.categoryHttpService.create(this.category).subscribe(
      response => {
        console.log('Category created successfully', response);
        this.router.navigate(['/moderator/categories']);
      },
      error => {
        this.createSuccess = false;
        if (error.error && error.error.errors) {
          this.errors = Object.keys(error.error.errors).flatMap(k => error.error.errors[k]);
        } else {
          this.errors = ['Wystąpił błąd podczas tworzenia kategorii.'];
        }
      }
    );
  }
}
