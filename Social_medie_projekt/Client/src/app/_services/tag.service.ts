import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Post } from '../_models/post'
import { User } from '../_models/user';
import { Tag } from '../_models/tags';

@Injectable({
    providedIn: 'root'
})
export class TagService {
    private readonly apiUrl = environment.apiUrl + 'Tag'
    constructor(private http: HttpClient) {}

    // Henter alle posts med et bestemt tagId
    GetPostByTagId(tagId: number): Observable<Tag>{
        return this.http.get<Post[]>(`${this.apiUrl}/tag/${Tagid}`).pipe(
            map(posts => ({
                
            }))
        );
    }
}