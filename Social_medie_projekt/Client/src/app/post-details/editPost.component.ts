import { Component } from '@angular/core';
import { AppComponent } from '../app.component';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { User } from '../_models/user';
import { PostService } from '../_services/post.service';
import { Post } from '../_models/post';
import { FormGroup, FormsModule, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-editPost',
  template: `
  <div class="body">
    <div class="post">
      <form [formGroup]="postForm" id="form">
        
        <div class="formControl">
          <textarea type="text" id="title" formControlName="Title" maxlength="100" (keyup)="titleMaxLenght($event)" placeholder="Title"></textarea>
          <span id="titleCharLenght">{{titleCharLenght}}</span>
        </div>

        <div class="formControl">
          <textarea id="content" formControlName="Content" maxlength="1000" (keyup)="contentMaxLenght($event)" placeholder="Write about anything ..." 
            cdkTextareaAutosize #autosize="cdkTextareaAutosize" cdkAutosizeMaxRows="20">
          </textarea>
          <span id="contentCharLenght">{{contentCharLenght}}</span>
        </div>

        <div class="formControl">
          <input type="text" id="tags" formControlName="Tags" placeholder="#Tags,"/>
        </div>
        
        <div class="buttonDiv">
          <button id="createBtn" (click)="edit()">Edit post</button>
        </div>  

        <div class="buttonDiv">
        </div>  

      </form>
    </div>
  </div>
  `,
  styles: [`
  
  .body {
    display: flex;
    margin-top: 20px;
    align-items: center; 
    flex-direction: column;
  }
  .post{
    width: 100%;
    max-width: 37%;
  }
  #form, .formControl, .buttonDiv{
    background-color: lightgrey;
    border-radius: 15px;
    position: relative; /* important for titleCharLenght*/
  }
  #form{
    padding: 20px 20px 0px 20px;
    box-shadow: 0px 0px 10px 20px rgb(80, 80, 100);
  }
  input, textarea{
    width: 400px;
    height: 22px;
    padding: 30px 15px 5px 15px;
    border: none;
    border-bottom: 1px solid darkgray;
    font-size: 16px;
    resize: none;
    background-color: transparent;
  }
  input:focus, textarea:focus{
    outline: none;
    border-bottom: 1px solid gray;
  }


  #title{
    width: 100%;
    max-width: 85%;
    overflow: hidden;
    max-height: 200px;
  }
  #titleCharLenght{
    position: absolute;
    background-color: transparent;
    margin-top: 25px;
    right: 25px;
  }
  #content{
    width: 100%;
    max-width: 85%;
    overflow: hidden;
    max-height: 200px;
  }
  #contentCharLenght{
    position: absolute;
    background-color: transparent;
    margin-top: 25px;
    right: 25px;
    color: red;
  }
  #tags{
    width: 100%;
    max-width: 85%;
  }



  .buttonDiv{
    position: relative;
    display: flex;
    justify-content: center;
    margin-top: 20px;
  }
  #createBtn{
    width: 150px;
    height: 35px;
    font-size: 25px;
  }
  #createBtn, #expandBtn, #collapseBtn, #cancelBtn{
    border: none;
    border-radius: 15px;
  }
  #collapseBtn{
    width: 25px;
    height: 25px;
    position: absolute;
    bottom: 20px;
    right: 5px;
  }
  #cancelBtn{
    width: 25px;
    height: 25px;
    position: absolute;
    bottom: 20px;
    right: 35px;
    font-size: 15px;
  }
  #expandBtn{
    width: 150px;
    height: 35px;
    position: absolute;
    bottom: 15px;
    font-size: 25px;
  }
  button{
    background-color: darkgray;
  }
  button:hover{
    box-shadow: 0px 0px 10px 5px rgb(80, 80, 100);
    cursor: pointer;
  }
  button:active{
    background-color: rgb(66, 66, 66);
  }
  `]
})
export class EditPostComponent {

  currentUser: any = {};
  postForm: FormGroup = this.resetForm()
  post: Post = this.resetPost()
  posts: Post[] = []
  titleCharLenght: number //til at vise hvor mange tegn der kan være i post-title
  contentCharLenght: number //til at vise hvor mange tegn der kan være i post-content

  constructor(private route: ActivatedRoute, private authService: AuthService, private postService: PostService){ }

  ngOnInit(): void {
    this.authService.currentUser.subscribe(x => this.currentUser = x)
    this.route.params.subscribe(params => { this.postService.GetPostByPostId(params['postId']).subscribe(x => this.post = x) })
  }

  edit(){

    if(!this.postForm.pristine){
      this.post = {
        postId: 0, 
        title: this.postForm.value.Title, 
        desc: this.postForm.value.Content,
        tags: this.postForm.value.Tags,
      }
  
      console.log(this.post)

      // this.postService.editPost(this.post).subscribe({
      //   next: (x) => {
      //     this.posts.push(x);
      //     // route to post-detail(post.postId)
      //   },
      //   error: (err) => {
      //     console.warn(Object.values(err.error.errors).join(', '))
      //   }
      // });
    }
    else{
      console.log("nothing changed")
    }

    
  }

  resetPost():Post {
    return{ postId: 0, title: '', desc: '', tags: '' }
  }

  resetForm(){
    return new FormGroup({ Title: new FormControl(''), Content: new FormControl(''), Tags: new FormControl('') })
  }

  titleMaxLenght(event: any) {
    this.titleCharLenght = 100 - event.target.textLength;
    if(this.titleCharLenght <= 20)
      document.getElementById("titleCharLenght")!.style.color = "red"
    else
      document.getElementById("titleCharLenght")!.style.color = "black"
  }

  contentMaxLenght(event: any) {
    this.contentCharLenght = 1000 - event.target.textLength;
    if(this.contentCharLenght <= 100)
    document.getElementById("contentCharLenght")!.style.display = 'inline'
    else
    document.getElementById("contentCharLenght")!.style.display = 'none'
  }

}
