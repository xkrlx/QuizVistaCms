import { Component } from '@angular/core';
import { Quiz } from 'src/app/models/quiz';
import { AuthService } from 'src/app/services/auth-service';
import { QuizHttpService } from 'src/app/services/http/quiz-http-service';

@Component({
  selector: 'app-quizzez',
  templateUrl: './quizzez.component.html',
  styleUrls: ['./quizzez.component.css']
})
export class QuizzezComponent {
  quizzes: any = []
  filteredQuizzes: any[] = [];
  searchTerm: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 9; 

  constructor(private quizService: QuizHttpService, private authService: AuthService) {}

  ngOnInit(): void {
    this.quizService.getQuizezForMod().subscribe(
      data=>{
        this.quizzes=data.model;
        this.filterQuizzez()
      },
      error=>{
        console.error("Błąd!!",error);
      }
    )
  }


  IsUserLogged() {
    return this.authService.isUserLoggedIn(); 
  }

  
  filterQuizzez(): void {
    this.currentPage=1;
    this.applyQuizFilterAndPagination();
  }

  private refreshQuizzez(): void {
    this.quizService.getQuizezForMod().subscribe(
      (data) => {
        this.quizzes = data.model;
        this.applyQuizFilterAndPagination();
      },
      (error) => {
        console.error('Error refreshing quizzez', error);
      }
    );
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
  
  
}
