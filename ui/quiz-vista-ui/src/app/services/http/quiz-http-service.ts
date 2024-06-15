import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiConfig } from '../../config/api-config';
import { Quiz } from 'src/app/models/quiz';
import { GenQuiz } from 'src/app/models/genQuiz';

@Injectable({
  providedIn: 'root'
})

export class QuizHttpService {

  url: string =  `${ApiConfig.url}/Quiz`;

  constructor(private http: HttpClient) { }

  getQuiz(): Observable<any> {
    return this.http.get(`${this.url}/User`);
  }

  getQuizByTag(tagName: string): Observable<any> {
    return this.http.get(`${this.url}/tag?tagName=${tagName}`);
  }

  getQuizByCategory(categoryName: string): Observable<any> {
    return this.http.get(`${this.url}/category?categoryName=${categoryName}`);
  }

  getQuizezForMod(): Observable<any> {
    return this.http.get(`${this.url}/Moderator`);
  }


  getQuizDetails(quizName: string): Observable<any> {
    return this.http.get(`${this.url}/details?quizName=${quizName}`)
  }

  getQuizDetailsForMod(quizName: string): Observable<any> {
    return this.http.get(`${this.url}/details-mod?quizName=${quizName}`)
  }
  getQuizRunQusetions(quizName: string):Observable<any> {
    return this.http.get(`${this.url}/quiz-run?quizName=${quizName}`)
  }

  createQuiz(quiz: Quiz): Observable<any>{
    return this.http.post(`${this.url}/create`,quiz);
  }

  generateQuiz(genQuiz: GenQuiz): Observable<any>{
    return this.http.post(`${this.url}/generate`,genQuiz);
  }

  editQuiz(quiz: Quiz): Observable<any>{
    return this.http.put(`${this.url}/edit`,quiz);
  }

  getQuizModQuestions(quizName: string):Observable<any>{
    return this.http.get(`${this.url}/get-questions-mod?quizName=${quizName}`)
  }

  deleteQuiz(quizId:string):Observable<any>{
    return this.http.delete(`${this.url}/delete/${quizId}`)
  }

  AssignUser(quizName:string, userName:string):Observable<any>{
    return this.http.post(`${this.url}/assignuser`,{
      UserName: userName,
      QuizName: quizName
    })
  }

  unAssignUser(quizName:string, userName:string):Observable<any>{
    return this.http.post(`${this.url}/unassignuser`,{
      UserName: userName,
      QuizName: quizName
    })
  }
}
