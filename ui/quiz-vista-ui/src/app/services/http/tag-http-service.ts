import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiConfig } from 'src/app/config/api-config';
import { Tag } from 'src/app/models/tag';

@Injectable({
  providedIn: 'root'
})
export class TagHttpService {

  url: string =  `${ApiConfig.url}/Tag`;

  constructor(private http: HttpClient) { }

  showTags():Observable<any>{
    return this.http.get(`${this.url}`)
  }

  showTag(tagId: string): Observable<any>{
    return this.http.get(`${this.url}/${tagId}`);
  }

  create(tag: Tag): Observable<any> {
    return this.http.post(`${this.url}/create`, tag);
  }


  update(tag: Tag): Observable<any> {
    return this.http.put(`${this.url}/edit`, tag);
  }

  delete(tagId: string): Observable<any>{
    return this.http.delete(`${this.url}/delete/${tagId}`)
  }
}
