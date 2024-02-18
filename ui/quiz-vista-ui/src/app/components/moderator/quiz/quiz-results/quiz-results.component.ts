import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ModeratorResultBrief } from 'src/app/models/moderator-results/moderator-result-brief';
import { QuizResult } from 'src/app/models/moderator-results/quiz-result';
import { AttemptHttpService } from 'src/app/services/http/attempt-http.service';

@Component({
  selector: 'app-quiz-results',
  templateUrl: './quiz-results.component.html',
  styleUrls: ['./quiz-results.component.css']
})
export class QuizResultsComponent implements OnInit {
  quizName: string = ''; 
  quizResults!: ModeratorResultBrief;

  constructor(private attemptHttpService: AttemptHttpService,private route: ActivatedRoute) { }

  ngOnInit(): void {
    
    this.route.paramMap.subscribe(params =>{
      this.quizName = params.get('quizName') ?? '';
    })

    this.loadQuizResults(this.quizName);

  }

  loadQuizResults(quizName: string): void {
    this.attemptHttpService.getQuizResults(quizName).subscribe(
      res => {
        this.quizResults = res.model;
        console.log(res)
      },
      error => {
        console.error(error);
      }
    );
  }
}
