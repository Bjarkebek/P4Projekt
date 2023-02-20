import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, first, last, map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Role } from '../_models/role';
import { User } from '../_models/user';
import { Login } from '../_models/login';
import { EmailValidator } from '@angular/forms';
import { SignInResponse } from '../Req-Res/signInResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<SignInResponse>;
  currentUser: Observable<SignInResponse>;

  constructor(private http: HttpClient) { 
    this.currentUserSubject = new BehaviorSubject<SignInResponse>(
      JSON.parse(sessionStorage.getItem('currentUser') as string)
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get CurrentUserValue(): SignInResponse {
    return this.currentUserSubject.value;
  }

  public getUser(): any {
    const user = window.sessionStorage.getItem('currentUser');
    if (user) {
      return JSON.parse(user);
    }
    return {};
  }

  login(email: string, password: string){
    let authenticateUrl = `${environment.apiUrl}Login/authenticate`;
    return this.http.post<SignInResponse>(authenticateUrl, { "email": email, "password": password}).pipe(map(user => {
      sessionStorage.setItem('currentUser', JSON.stringify(user));

      this.currentUserSubject.next(user as SignInResponse);
      return user;
    }))
  }

  logout(){
    sessionStorage.removeItem('currentUser');
    this.currentUserSubject = new BehaviorSubject<SignInResponse>(JSON.parse(sessionStorage.getItem('currentUser') as string));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  register(login: Login): Observable<Login> {
    let registerUrl = `${environment.apiUrl}login/register/`;
    return this.http.post<Login>(registerUrl, login)
  }
}
