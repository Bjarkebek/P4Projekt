import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-headerLoggedOut',
  template: `
    <nav>
      <div class="nav">
        <img id="logo" class="linkLeft" src="/assets/images/socialmachine.png"  routerLink="/main"> 
        <!-- <a class="linkLeft"  routerLink="/main"    >Home</a> -->
        <!-- <a class="linkRight" routerLink="/"        >Logout</a>
        <a class="linkRight" routerLink="/profile" >Profile</a> -->
      </div>
    </nav>
  `,
  styles: [`
  .nav{
    background-color: gray;
  }
  #logo{
    display: inline-block;
    width: 40px;
    margin-right: 10px;
    background-color: transparent;
  }
  #logo:hover{
    cursor: pointer;
  }
  a:link, a:visited{
    display: inline-block;
    border-radius: 5px;
    padding: 5px 5px 5px 5px;
    color: #eee;
    text-decoration: none;
  }
  .linkLeft{
    margin: 0px 5px 0px 5px;
    float: left;
  }
  .linkRight{
    margin: 0px 5px 0px 5px;
    float: right;
  }
  `]
})
export class HeaderLoggedOutComponent {

}
