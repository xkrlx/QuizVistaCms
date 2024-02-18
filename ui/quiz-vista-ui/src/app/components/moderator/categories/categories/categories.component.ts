import { Component } from '@angular/core';
import { CategoryHttpService } from 'src/app/services/http/category-http-service';

@Component({
  selector: 'app-category',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent {
  categories: any[]=[];
  filteredCategories: any[]=[];
  searchTerm: string = '';
  previousSearchTerm: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 10; 

  constructor(private categoryService: CategoryHttpService){}

  ngOnInit(): void {
    this.loadCategories();
  }

  private loadCategories():void{
    this.categoryService.showCategories().subscribe(
      (data)=>{
        this.categories=data.model;
        this.filterCategories();
      },
      (error)=>{
        console.error('Error fetching categories',error);
      }
    )
  }

  filterCategories(): void {
    this.currentPage = 1;
    this.applyFilterAndPagination();
  }

  private applyFilterAndPagination(): void {
    const filtered = this.searchTerm ? this.categories.filter(category => 
      category.name.toLowerCase().includes(this.searchTerm.toLowerCase())) : this.categories;

    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.filteredCategories = filtered.slice(startIndex, endIndex);
  }

  changePage(step: number): void {
    const newPage = this.currentPage + step;
    const filteredLength = this.searchTerm ? this.categories.filter(category => 
      category.name.toLowerCase().includes(this.searchTerm.toLowerCase())).length : this.categories.length;
    const totalPages = Math.ceil(filteredLength / this.itemsPerPage);

    if (newPage > 0 && newPage <= totalPages) {
      this.currentPage = newPage;
      this.applyFilterAndPagination();
    }
  }
  

}
