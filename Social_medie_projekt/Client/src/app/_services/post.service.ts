import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Post } from '../_models/post'
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  private readonly apiUrl = environment.apiUrl + 'Post' ;

  constructor(private http: HttpClient) { }


  //getall every posts
  getAll(): Observable<Post[]>{
    return this.http.get<Post[]>(`${this.apiUrl}`)
  }

  GetPostById(id: number): Observable<Post>{
    return this.http.get<Post>(`${this.apiUrl}/${id}`)
    
  }

  //create post
  createPost(user: User){
    return this.http.post<Post>(this.apiUrl, user)
  }

  //delete post
  delPost(){

  }

  //update post
  updPost(){

  }

}
