import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;

  constructor(
    private http: HttpClient
  ) { }

  getPostalCodes() {
    return this.http.get(this.baseUrl + 'admin/postalCodes');
  }

  getTaxCalculationTypes() {
    return this.http.get(this.baseUrl + 'admin/taxCalculationTypes');
  }

  addPostalCode(postalCodeObj: any) {
    return this.http.post(this.baseUrl + 'admin/addPostalCode', postalCodeObj);
  }

  deletePostalCode(id: number) {
    return this.http.delete(this.baseUrl + 'admin/deletePostalCode/' + id);
  }

}

