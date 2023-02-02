
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, FormsModule,  FormControl, Validators } from '@angular/forms';
import { Login } from '../_models/login';
import { Role } from '../_models/role';
import { AuthService } from '../_services/auth.service';
import { AppComponent } from '../app.component';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-loginpage',
  template: `
  <div class="body">
    <img src="/assets/images/socialmachine.png" width="600">

    <form [formGroup]="userForm" class="form" (ngSubmit)="login()">
      <div class="formControl">
        <label>Email</label>
        <input type="text" formControlName="Email"/>
      </div>
      <div class="formControl">
        <label>Password</label>
        <input type="password" formControlName="Password"/>
      </div>

      <div class="buttonDiv">
        <button type="submit">Login</button>
        <button class="right" routerLink="/createuser">Create new user</button>
      </div>
    </form>

  </div>
  `,

  styles: [`
  .body {
    display: flex; 
    height: 700px; 
    margin-top: 20px;
    align-items: center; 
    flex-direction: column;
  }
  .form{
    width: 100%;
    max-width: 400px;
  }


  .formControl{
    display: flex;
    justify-content: center;
    margin: 5px 5px 5px 0;
    flex-direction: row;
  }
  label{
    order: 0;
    width: 100px;
    margin-right: 5px;
    text-align: right;
  }
  input{
    order: 1;
    width: 200px;
    margin-left: 3px;
    background-color: white;
  }


  .buttonDiv{
    display: flex;
    justify-content: center;
    margin-top: 20px;
  }
  button{
    margin-left: 5px;
    margin-right: 5px;
  }
  button:hover{
    background-color: rgb(211, 211, 211);
    cursor: pointer;
  }
  button:active{
    background-color: rgb(66, 66, 66);;
  }
  `]
})
export class LoginpageComponent {

  constructor(
    private auth: AuthService,
    private router: Router, 
    private route: ActivatedRoute, 
    private AppComponent: AppComponent
  ) { }
  
  message = '';
  userForm: FormGroup = this.resetForm();
  userlogin: Login = { loginId:0, email: "", password:"" }

  resetForm(): FormGroup{
    return new FormGroup({
      Email:     new FormControl(null, [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]),
      Password:  new FormControl(null,  Validators.required),
    })
  }


  login(){
    this.auth.login(this.userForm.value.Email, this.userForm.value.Password).subscribe({
      next: () => {
        
        let returnUrl = this.route.snapshot.queryParams['returnUrl']||'/main';
        this.router.navigate([returnUrl])
        
        //ændrer headeren
        this.AppComponent.validateHeader()
      },
      
      error: err => {
        
        if (err.error.status == 400) {
          this.message = 'Indtast brugernavn og kodeord';
          console.log(this.message)
        }
        if (err.error.status == 401) {
          this.message = 'Forkert brugernavn eller kodeord';
          console.log(this.message)
        }
        if ( err.error.status == 500) {
          this.message = 'Fejl ved forbindelse til server';
          console.log(this.message)
        }
        else {
          this.message = err.error.title;
        }
      }
      
    });  

  }
}

