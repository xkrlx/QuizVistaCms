import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from 'src/app/models/category';
import { ErrorHandlerService } from 'src/app/services/error-handler.service';
import { CategoryHttpService } from 'src/app/services/http/category-http-service';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent {
  category!: Category;
  categoryId: string = '';
  updateSuccess = false;
  errors: string[] = [];
  backendErrorMessages:any[]=[];


  constructor(private categoryHttpService: CategoryHttpService, private route: ActivatedRoute, private router: Router, private errorHandlerService:ErrorHandlerService) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.categoryId = params['id'];
      this.getCategoryData();
    });
    this.getCategoryData();
  }

  getCategoryData(): void {
    this.categoryHttpService.showCategory(this.categoryId).subscribe(
      (Data: any) => {
        console.log(Data);
        this.category = Data.model;
      },
      (error) => {
        this.backendErrorMessages = this.errorHandlerService.handleError(error);
      }
    );
  }

  updateCategory(): void {
    this.categoryHttpService.update(this.category).subscribe(
      response => {
        if(response.isValid===true){
          this.updateSuccess = true;
          this.errors = [];
          setTimeout(() => this.updateSuccess = false, 5000)
        }
        else if(response.isValid===false){
          this.backendErrorMessages=[response.errorMessage]
        }
      },
      error => {
        this.updateSuccess = false;
        this.backendErrorMessages = this.errorHandlerService.handleError(error);
      }
    );
  }

  deleteCategory(categoryId: string | undefined): void {
    if (!categoryId) {
      console.error("categoryId undefined");
      return;
    }

    this.categoryHttpService.delete(categoryId).subscribe(
      response => {
        if(response.isValid===true){
          this.updateSuccess=true;
            this.router.navigate(['/moderator/categories']);
        }
        else if(response.isValid===false){
          this.backendErrorMessages=[response.errorMessage]
        }
      },
      error => {
        console.log(error)
        this.backendErrorMessages = this.errorHandlerService.handleError(error);
      }
    );
  }
  
  
}

