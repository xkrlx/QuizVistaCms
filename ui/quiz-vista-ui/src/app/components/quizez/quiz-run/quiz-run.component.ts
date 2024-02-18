import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs';
import { QuizRun } from 'src/app/models/quiz-run/quiz-run';
import { QuizHttpService } from 'src/app/services/http/quiz-http-service';
import { QuestionRun } from '../../../models/quiz-run/question-run';
import { AnswerRun } from 'src/app/models/quiz-run/answer-run';
import { AttemptHttpService } from '../../../services/http/attempt-http.service';

@Component({
  selector: 'app-quiz-run',
  templateUrl: './quiz-run.component.html',
  styleUrls: ['./quiz-run.component.css']
})
export class QuizRunComponent {
  quizName: string = '';
  quizData!: QuizRun
  
  selectedAnswers: { [key: string]: any } = {}; // Przechowuje wybrane odpowiedzi przez użytkownika
  isFormInvalid: boolean = true;


  constructor(private route: ActivatedRoute, 
    private quizHttpService: QuizHttpService, 
    private attemptHttpService: AttemptHttpService,
    private router: Router
    ) { }

  ngOnInit(): void {
    this.route.paramMap.pipe(
      switchMap(params => {
        this.quizName = params.get('quizName') ?? '';
        return this.quizHttpService.getQuizRunQusetions(this.quizName);
      })
    )
    .subscribe(
      response => {
        this.quizData = response;
        this.initializeSelectedQuestions()
      },
      error => {
        console.error('Error fetching quiz questions:', error);
      }
    );
  }
  initializeSelectedQuestions() {
    this.quizData.model.questions.forEach((question:QuestionRun) => {
      if(question.type === '1' || question.type === '2'){
        this.selectedAnswers[question.id] = null;
      }
      else if(question.type === '3'){
        question.answers.forEach((answer: AnswerRun) => {
          this.selectedAnswers[question.id + '-' + answer.id] = false
        })
      }
    })
  }
  
  checkFormValidity(): void {
    console.log("changed answers: ", this.selectedAnswers)

    for(const key in this.selectedAnswers){
      console.log(key)
      if(key.includes('-')){
        const[questionId, answerId] = key.split('-')
        const answersToQuestion = Object.keys(this.selectedAnswers)
          .filter((k) => k.startsWith(`${questionId}-`));


        let multiCheck = answersToQuestion.some((k) => this.selectedAnswers[k]);


        if (!multiCheck) {
          this.isFormInvalid = true;
          return;
        }
  

      }
      else{
        const value = this.selectedAnswers[key]
        if(value == null){
          this.isFormInvalid = true
          return
        }
      }
    }

    this.isFormInvalid = false;
  }


  
  onSubmit(){

    this.isFormInvalid = true;

    const userAnswers: Array<number> = [];
    for (const key in this.selectedAnswers) {
      if (this.selectedAnswers.hasOwnProperty(key)) {
        if (this.selectedAnswers[key]) {
          if(key.includes('-')){
            const [questionId, answerId] = key.split('-')
            userAnswers.push(+answerId)
          }
          else userAnswers.push(+this.selectedAnswers[key]);
        }
      }
    }

    // userAnswers zawiera teraz informacje o wybranych odpowiedziach przez użytkownika
    console.log('Wybrane odpowiedzi przez użytkownika:', userAnswers);
    
    this.attemptHttpService.saveAttempt(userAnswers).subscribe(
      response => {
        this.router.navigate(['quizez'])
      },
      error => {
        console.warn(error)
      }
    )
      
  }



}
