import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { QuizHttpService } from 'src/app/services/http/quiz-http-service';

@Component({
  selector: 'app-quiz-details',
  templateUrl: './quiz-details.component.html',
  styleUrls: ['./quiz-details.component.css']
})
export class QuizDetailsComponent implements OnInit {
  quizName: string = ''

  quizDetails: any

  constructor(private route: ActivatedRoute, private quizHttpService: QuizHttpService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params =>{
      this.quizName = params.get('quizName') ?? '';
    })

    this.getQuizDetails(this.quizName)

    
  }

  getQuizDetails(quizName: string){
    this.quizHttpService.getQuizDetails(quizName).subscribe(res=>{
      this.quizDetails = res.model
    })
  }





}
