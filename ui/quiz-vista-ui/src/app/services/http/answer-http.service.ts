import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiConfig } from 'src/app/config/api-config';
import { Answer } from 'src/app/models/answer';

@Injectable({
  providedIn: 'root'
})
export class AnswertHttpService {

  url: string =  `${ApiConfig.url}/Answer`;

  

  constructor(private http: HttpClient) { }

  createAnswer(answer:Answer): Observable<any>{
    return this.http.post(`${this.url}/create`,answer);
  }
  editAnswer(answer:Answer): Observable<any>{
    return this.http.put(`${this.url}/edit`,answer);
  }

  deleteAnswer(answerId: string):Observable<any>{
    return this.http.delete(`${this.url}/delete/${answerId}`)
  }

}
