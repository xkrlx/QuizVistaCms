import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiConfig } from 'src/app/config/api-config';

@Injectable({
  providedIn: 'root'
})
export class AttemptHttpService {
  //https://localhost:7136/api/Attempt/create

  url: string =  `${ApiConfig.url}/Attempt`;

  constructor(private http: HttpClient) { }

  saveAttempt(answers: Array<number>):Observable<any>{
    return this.http.post(`${this.url}/create`,{
      "answerIds": answers
    })
  }

  getUserResults():Observable<any>{
    return this.http.get(`${this.url}/userResults`)
  }

  getQuizResults(quizName: string): Observable<any> {
    return this.http.get(`${this.url}/quizResults/${quizName}`);
}


}
