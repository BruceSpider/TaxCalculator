import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
import { User } from '../_models/User';
import { environment } from '../../environments/environment';
import { AlertifyService } from './alertify.service';
import { Router } from '@angular/router';

const httpOptions = {
  headers: new HttpHeaders({
    'Authorization': 'Bearer ' + localStorage.getItem('token'),
  })
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  decodedToken: any;
  jwtHelper = new JwtHelperService();
  currentUser: User;

  constructor(
    private http: HttpClient,
    private alertify: AlertifyService,
    private router: Router
  ) { }

  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model, httpOptions)
      .pipe(
        map((response: any) => {
          const obj = response;
          if (obj) {
            localStorage.setItem('token', obj.token);
            localStorage.setItem('user', JSON.stringify(obj.user));
            this.decodedToken = this.jwtHelper.decodeToken(obj.token);
            this.currentUser = obj.user;
          }
        })
      );
  }

  register(user: User) {
    return this.http.post(this.baseUrl + 'register', user);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.decodedToken = null;
    this.currentUser = null;
    this.alertify.message('Logged out');
    this.router.navigate(['/home']);
  }

  loggedInRedirect() {
    if (this.loggedIn()) {
      this.router.navigate(['/home']);
    }
  }

}
