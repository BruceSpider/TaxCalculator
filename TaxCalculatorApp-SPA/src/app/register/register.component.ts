import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service.js';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '../_models/User.js';
import { AlertifyService } from '../_services/alertify.service.js';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  user: User;
  registerForm: FormGroup;

  constructor(private authService: AuthService,
    private fb: FormBuilder,
    private router: Router,
    private alertify: AlertifyService) { }

  ngOnInit() {
    this.authService.loggedInRedirect();
    this.initRegisterForm();
  }



  initRegisterForm(): any {
    this.registerForm = this.fb.group({
      username: ['', Validators.required, Validators.pattern('[a-zA-Z0-9]*')],
      email: ['', Validators.required],
      password: ['', [Validators.required,
      Validators.minLength(4),
      Validators.maxLength(8)]],
      confirmPassword: ['', Validators.required]
    }, {validator: this.passwordMatchValidator});
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value
      ? null
      : { mismatch: true };
  }

  register() {
    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      this.authService.register(this.user).subscribe( () => {
      }, error => {
        this.alertify.error(error);
      }, () => {
        this.authService.login(this.user).subscribe(() => {
          this.router.navigate(['/home']);
        });
      });
    } else {
      const controls = this.registerForm.controls;
      Object.keys(controls).forEach(controlName => controls[controlName].markAsTouched());
      return false;
    }
  }
}
