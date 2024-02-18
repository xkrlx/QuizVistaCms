import { Component, ElementRef, ViewChild } from '@angular/core';
import { CategoryHttpService } from '../../services/http/category-http-service';
import { TagHttpService } from '../../services/http/tag-http-service';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css']
})
export class SideNavComponent {


  categories: Array<string> = []
  tags: Array<string> = []

  @ViewChild('categoryList', { static: true }) categoryList!: ElementRef<HTMLDivElement>;

  
  constructor(private categoryHttpService: CategoryHttpService, private tagHttpService: TagHttpService) {  }

  ngOnInit(){

    this.categoryHttpService.showCategories().subscribe(
      res=>{
        this.categories = res.model.map((x: { name: any; })=>x.name)
      },
      error=>{
        console.warn(error)
      }
    )

    this.tagHttpService.showTags().subscribe(
      res =>{
        this.tags = res.model.map((x: { name: any; })=>x.name)
      },
      error =>{
        console.warn(error)
      }
    )

    this.checkScrollbar();

    

  }

  minFontSize = 12; // Minimalna wielkość czcionki
  maxFontSize = 22; // Maksymalna wielkość czcionki

  
  calculateTagFontSize(tag: string): number {
    const categoryLength = this.categories.reduce((max, category) => (category.length > max ? category.length : max), 0);
    const ratio = tag.length / categoryLength;
    return this.minFontSize + Math.floor((this.maxFontSize - this.minFontSize) * ratio);
  }

checkScrollbar(){
  const element = this.categoryList.nativeElement;
    if (element.scrollHeight > element.clientHeight) {
      element.style.overflowY = 'scroll';
    }
}



}
