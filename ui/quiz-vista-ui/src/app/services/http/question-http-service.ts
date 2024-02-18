import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiConfig } from '../../config/api-config';
import { Question } from 'src/app/models/question';

@Injectable({
  providedIn: 'root'
})

export class QuestionHttpService {

  url: string =  `${ApiConfig.url}/Question`;

  constructor(private http: HttpClient) { }

  
  createQuestion(question:Question): Observable<any>{
    return this.http.post(`${this.url}/create`,question);
  }

  editQuestion(question:Question):Observable<any>{
    return this.http.put(`${this.url}/edit`,question);
  }

  deleteQuestion(questionId: string):Observable<any>{
    return this.http.delete(`${this.url}/delete/${questionId}`)
  }

}
