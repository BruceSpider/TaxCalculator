import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import '../../assets/login-animation.js';
import { AuthService } from '../_services/auth.service.js';
import { AlertifyService } from '../_services/alertify.service.js';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from '../_models/User.js';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  user: User;
  displayMessage = '';
  loading = false;
  returnUrl: string;

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit() {
    (window as any).initialize();
    this.authService.loggedInRedirect();
    this.InitLoginForm();
    this.returnUrl = (this.route.snapshot.queryParamMap.get('returnUrl') ? this.route.snapshot.queryParamMap.get('returnUrl') : '/');
  }

  InitLoginForm(): any {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required,
      Validators.minLength(4),
      Validators.maxLength(8)]]
    });
  }

  login() {
    if (this.loginForm.valid) {
      this.loading = true;
      this.user = Object.assign({}, this.loginForm.value);
      this.authService.login(this.user).subscribe(
        () => {
          this.loading = false;
          this.alertify.success('Logged in successfully');
        },
        error => {
          this.loading = false;
          this.displayMessage = error.status === 400 ? error.error : error.statusText;
          this.alertify.error(this.displayMessage);
        },
        () => {
          this.router.navigate([this.returnUrl]);
        }
      );
    } else {
      const controls = this.loginForm.controls;
      Object.keys(controls).forEach(controlName => controls[controlName].markAsTouched());
      return false;
    }
  }

  register() {
    this.router.navigate(['/register']);
  }

}
