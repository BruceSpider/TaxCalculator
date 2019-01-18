import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { TaxCalculatorComponent } from './tax-calculator/tax-calculator.component';
import { AdminComponent } from './admin/admin.component';
import { AuthGuard } from './_guards/auth.guard';
// import { AuthGuard } from './_guards/auth.guard';

const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'login', component: LoginComponent, },
    { path: 'register', component: RegisterComponent },
    { path: 'home', component: HomeComponent },
    { path: 'tax-calculator', component: TaxCalculatorComponent, canActivate: [AuthGuard] },
    { path: 'admin', component: AdminComponent, canActivate: [AuthGuard] },
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);
