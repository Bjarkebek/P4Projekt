import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
  <div class="container">
    <div class="header">
      <app-header></app-header>
    </div>
    <div class="content">
      <router-outlet></router-outlet>
    </div>
    <div class="footer">
      <app-footer></app-footer>
    </div>
  </div>
  `,
  styles: [`
  .header{
    width: 100%;
    position: fixed;
    top: 0px;
    left: 0px;
    padding: 10px 0px 10px 0px;
    background-color: gray;
    z-index: 1;
  }
  .content{
    position: relative;
    margin-top: 40px;
    padding: 3px 5px;
  }
  .footer{
    position: relative;
    bottom: 0px;
    padding: 3px 5px;
  }
  `]
})
export class AppComponent {
  title = 'Client';
}
