import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostService } from '../_services/post.service';


@Component({
  selector: 'app-profilepage',
  template: `

  <div id="post" *ngFor="let data of post">
    <h1 id="title">{{data.title}}</h1>
    <h5 id="username">{{data.username}}</h5>
    <h3 id="description">{{data.desc}}</h3>
    <p id="date">{{data.date}}</p>
    <p id="tags">{{data.tag}} </p>
    <!-- <p id="tags">{{data.postId}} </p> -->
    <!-- <p id="tags">{{data.userId}} </p> -->
    <!-- <p id="tags">{{data.postInput}} </p> -->
    <!-- <p id="tags">{{data.likes}} </p> -->
    <button class="postBtn" id="like"><3</button>
  </div>
  `,
  styleUrls: ["../_css/poststyle.css"]
})
export class ProfilepageComponent {

  post:any
  
  // sætter values i getTempData til data
  constructor(post:PostService){ this.post = post.getAllSelf(1) }
}
