import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataService } from '../_services/data.service';
import { Data } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { Post } from '../_models/post';
import { PostService } from '../_services/post.service';


@Component({
  selector: 'app-indexpage',
  // standalone: false,
  // imports: [CommonModule],

  template: `    
  <!-- looper igennem alle post fra data(DataService) -->
  <div id="post" *ngFor="let post of posts">
    <h1 id="title">{{post.title}}</h1>
    <h5 id="username">{{post.user.firstName}} {{post.user.lastName}}</h5>
    <h3 id="description">{{post.desc}}</h3>
    <p id="date">{{post.date}}</p>
    <p id="tags">{{post.tag.tag}} </p>
    <button class="postBtn" id="like"><3</button>
  </div>

  `,
  styleUrls: ["./indexstyle.css"]
  
})
export class IndexpageComponent implements OnInit {

  posts: Post[] = [];


  // sætter values i getTempData til data
  constructor(private postService: PostService, private auth: AuthService){
    
  }

  ngOnInit(): void {
    this.postService.getAll().subscribe((x) => (this.posts = x));
  }

}
