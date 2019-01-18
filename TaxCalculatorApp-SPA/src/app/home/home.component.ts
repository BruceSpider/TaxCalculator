import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../_models/User';
import { AuthService } from '../_services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  jwtHelper = new JwtHelperService();

  constructor(
    private router: Router,
    private authService: AuthService) { }

  ngOnInit() {
    const token = localStorage.getItem('token');
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }

    if (user) {
      this.authService.currentUser = user;
    }
  }

  login() {
    this.router.navigate(['/login']);
  }

  register() {
    this.router.navigate(['/register']);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }

  calculator() {
    this.router.navigate(['/tax-calculator']);
  }

}
