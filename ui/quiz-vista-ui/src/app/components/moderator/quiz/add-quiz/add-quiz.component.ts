import { Component } from '@angular/core';
import { QuizHttpService } from 'src/app/services/http/quiz-http-service';
import { Quiz } from 'src/app/models/quiz';
import { CategoryHttpService } from 'src/app/services/http/category-http-service';
import { TagHttpService } from 'src/app/services/http/tag-http-service';
import { ActivatedRoute, Router } from '@angular/router';
import { ErrorHandlerService } from 'src/app/services/error-handler.service';

@Component({
  selector: 'app-add-quiz',
  templateUrl: './add-quiz.component.html',
  styleUrls: ['./add-quiz.component.css']
})
export class AddQuizComponent {
  newQuiz: Quiz = {
    id: '',
    name: '',
    description: '',
    categoryId: 0,
    cmsTitleStyle: '',
    isActive: true,
    attemptCount:0,
    publicAccess: true,
    tagIds: [] 
  };

  categories: any[] =[];
  tags: any[] =[];
  //backendErrorMessage: string | null = null;
  backendErrorMessages: any[]=[];
  err:any[]=[];

  ngOnInit() {
    this.loadCategories();
    this.loadTags();
  }

  constructor(private quizService: QuizHttpService, private categoryService:CategoryHttpService, private tagService: TagHttpService, private router: Router, private errorHandlerService:ErrorHandlerService) { }

  addQuiz() {
    const { id, ...quizData } = this.newQuiz; 
    this.quizService.createQuiz(quizData).subscribe(response => {
      
      if(response.isValid===true){
        this.router.navigate(['/moderator/add-questions/', quizData.name])
      }
      else{
        console.log(response)
        console.log(response.errorMessage)
        this.backendErrorMessages=[response.errorMessage]
      }

    }, error => {
      console.log(error);
      this.backendErrorMessages = this.errorHandlerService.handleError(error);
    });
  }

  loadCategories() {
    this.categoryService.showCategories().subscribe(
      (data) => {
        console.log(data);
        this.categories = data.model;
      },
      (error) => {
        console.error('Error loading categories', error);
      }
    );
  }


  loadTags() {
    this.tagService.showTags().subscribe(
      (data) => {
        this.tags = data.model;
      },
      (error) => {
        console.error('Error loading tags', error);
      }
    );
  }

  

}
