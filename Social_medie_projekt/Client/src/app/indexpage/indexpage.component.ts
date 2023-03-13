import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Data } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { PostService } from '../_services/post.service';
import { Post } from '../_models/post';
import { Tag } from '../_models/tags';


@Component({
  selector: 'app-indexpage',
  template: `    
  <app-createPostpage></app-createPostpage>
  
  <!-- looper igennem alle post fra data(DataService) -->
  <div id="post" *ngFor="let post of posts" [routerLink]="['/post-details', post.postId]">
    <h5 id="username"> 
      <img class="profilepic"src="./assets/images/placeholder.png" width="50" height="50">
      {{post.user?.userName}}
    </h5>
    <h1 id="title">{{post.title}}</h1>
    <h3 id="description">{{post.desc}}</h3>

    <div id="postTags" *ngIf="post.tags">
      <!-- {{log(post.tags)}} -->
      <p id="tag" *ngFor="let tag of post.tags">
        <!-- {{log(tag.name)}} -->
        {{tag.name}}
      </p>
    </div>

    <!-- <p id="tags" *ngIf="post.tags">#{{post.tags}}, </p> -->
    <p id="date">{{post.date | date:'MMM d yyyy, HH:mm a'}}</p> 
    <button class="postBtn" id="like"><3</button>
  </div>
  
  `,
  styleUrls: ["../_css/poststyle.css"]
  
})
export class IndexpageComponent {
  posts: Post[] = [];
  //jsonString: string;
  tags: Tag[];
  //tag: Tag = { tagId: 0, tagName: '' };

  log(val: any) 
  {
    console.log(val);
  }

  // post: Post = {
  //   postId: 0,
  //   title: '',
  //   desc: '',
  //   tags: '',
  //   likes: 0,
  //   date: new Date,
  //   user: {
  //     userId: 0,
  //     userName: '', 
  //     created: new Date,
  //     login: {
  //       loginId: 1, 
  //       email: '', 
  //       password: ''
  //     }, 
  //   posts: []}
  // }

  constructor(
    private postService: PostService,
    private auth: AuthService
  )
  {
    this.tags = [];
    //this.jsonString = JSON.stringify(this.tags.map(tag => tag.tagName));
  }

  ngOnInit(): void {
    this.postService.getAll().subscribe(x => this.posts = x)
  }

  // toArray(tags: object) {
  //   return Object.keys(tags).map(key => tags[key])
  // }

}
