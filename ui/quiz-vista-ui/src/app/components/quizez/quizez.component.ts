import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Quiz } from 'src/app/models/quiz';
import { AuthService } from 'src/app/services/auth-service';
import { QuizHttpService } from 'src/app/services/http/quiz-http-service';

@Component({
  selector: 'app-quizez',
  templateUrl: './quizez.component.html',
  styleUrls: ['./quizez.component.css']
})
export class QuizezComponent implements OnInit{
  quizzes: any[] = [];
  categoryName: string | null = '';
  tagName: string | null = '';
  message: string='';
  filteredQuizzes: any[] = [];
  searchTerm: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 9; 

  constructor(private quizService: QuizHttpService, private authService: AuthService, private route: ActivatedRoute){}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
    this.categoryName = params.get('categoryName');
    
    this.tagName = params.get('tagName');

    if(this.tagName != null) {
      this.quizService.getQuizByTag(this.tagName).subscribe(
        data=>{
          this.quizzes=data.model;
        },
        error=>{
          console.error("Błąd!!",error);
        }
      )
    }
    else if(this.categoryName != null) {
      this.quizService.getQuizByCategory(this.categoryName).subscribe(
        data=>{
          this.quizzes=data.model;
        },
        error=>{
          console.error("Błąd!!",error);
        }
      )
    } 
    else {
      this.quizService.getQuiz().subscribe(
        data=>{
          this.quizzes=data.model;
          this.filterQuizzez();
        },
        error=>{
          console.error("Błąd!!",error);
        }
      )

    }

  }
  )}

  filterQuizzez(): void {
    this.currentPage=1;
    this.applyQuizFilterAndPagination();
  }

  private applyQuizFilterAndPagination(): void {
    const filtered = this.searchTerm ? this.quizzes.filter((quiz:Quiz) => 
      quiz.name.toLowerCase().includes(this.searchTerm.toLowerCase())) : this.quizzes;
  
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.filteredQuizzes = filtered.slice(startIndex, endIndex);
  }

  changePage(step: number): void {
    const newPage = this.currentPage + step;
    const filteredLength = this.searchTerm ? this.quizzes.filter((quiz: Quiz) => 
      quiz.name.toLowerCase().includes(this.searchTerm.toLowerCase())).length : this.quizzes.length;
    const maxPage = Math.ceil(filteredLength / this.itemsPerPage);
   
    if (newPage > 0 && newPage <= maxPage) {
      this.currentPage = newPage;
      this.applyQuizFilterAndPagination();
    }
  }

  IsUserLogged() {
    return this.authService.isUserLoggedIn(); 
  }
}
