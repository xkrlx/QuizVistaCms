import { Component } from '@angular/core';
import { TagHttpService } from 'src/app/services/http/tag-http-service';

@Component({
  selector: 'app-tags',
  templateUrl: './tags.component.html',
  styleUrls: ['./tags.component.css']
})
export class TagsComponent {
tags: any[]=[];
filteredTags: any[]=[];
searchTerm: string='';
previousSearchTerm: string = '';
currentPage: number=1;
itemsPerPage: number=10;

constructor(private tagService: TagHttpService){}

ngOnInit(): void {
  this.loadTags();
}



private loadTags():void{
  this.tagService.showTags().subscribe(
    (data)=>{
      this.tags=data.model;
      this.filterTags();
    },
    (error)=>{
      console.error('Error fetching categories',error);
    }
  )
}

filterTags(): void {
  this.currentPage = 1; 
  this.applyFilterAndPagination();
}

private applyFilterAndPagination(): void {
  const filtered = this.searchTerm ? this.tags.filter(tag => 
    tag.name.toLowerCase().includes(this.searchTerm.toLowerCase())) : this.tags;

  const startIndex = (this.currentPage - 1) * this.itemsPerPage;
  const endIndex = startIndex + this.itemsPerPage;
  this.filteredTags = filtered.slice(startIndex, endIndex);
}

changePage(step: number): void {
  const newPage = this.currentPage + step;
  const filteredLength = this.searchTerm ? this.tags.filter(tag => 
    tag.name.toLowerCase().includes(this.searchTerm.toLowerCase())).length : this.tags.length;
  const totalPages = Math.ceil(filteredLength / this.itemsPerPage);

  if (newPage > 0 && newPage <= totalPages) {
    this.currentPage = newPage;
    this.applyFilterAndPagination();
  }
}

}
