import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService
  ) { }
  canActivate(state: ActivatedRouteSnapshot): boolean {

    if (this.authService.loggedIn()) {
      return true;
    }

    this.alertify.error('403 Forbidden');
    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
    return false;
  }
}
