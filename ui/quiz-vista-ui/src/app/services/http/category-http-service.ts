import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiConfig } from 'src/app/config/api-config';
import { Category } from 'src/app/models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryHttpService {

  url: string =  `${ApiConfig.url}/Category`;

  constructor(private http: HttpClient) { }

  showCategories():Observable<any>{
    return this.http.get(`${this.url}`)
  }

  showCategory(categoryId: string): Observable<any>{
    return this.http.get(`${this.url}/${categoryId}`);
  }

  create(category: Category): Observable<any> {
    return this.http.post(`${this.url}/create`, category);
  }


  update(category: Category): Observable<any> {
    return this.http.put(`${this.url}/edit`, category);
  }

  delete(categoryId: string): Observable<any>{
    return this.http.delete(`${this.url}/delete/${categoryId}`)
  }
}
