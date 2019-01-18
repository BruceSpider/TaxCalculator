import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BsDropdownModule, TabsModule, BsDatepickerModule, PaginationModule, ButtonsModule, ModalModule } from 'ngx-bootstrap';
import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { routing } from './app.routing';
import { TaxCalculatorComponent } from './tax-calculator/tax-calculator.component';
import { AdminComponent } from './admin/admin.component';
import { AlertifyService } from './_services/alertify.service';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxLoadingModule } from 'ngx-loading';
import { AuthGuard } from './_guards/auth.guard';


export function tokenGetter() {
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      ValueComponent,
      NavBarComponent,
      HomeComponent,
      RegisterComponent,
      LoginComponent,
      TaxCalculatorComponent,
      AdminComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      routing,
      BsDropdownModule.forRoot(),
      TabsModule.forRoot(),
      ModalModule.forRoot(),
      JwtModule.forRoot({
         config: {
            tokenGetter: tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:/5000/api/auth']
         }
      }),
      NgxLoadingModule.forRoot({})
   ],
   providers: [
      AuthService,
      AlertifyService,
      AuthGuard
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
