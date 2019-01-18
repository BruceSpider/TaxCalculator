import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { TaxCalculationInput } from '../_models/TaxCalculationInput';

@Injectable({
  providedIn: 'root'
})
export class TaxCalculatorService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  calculateTax(taxCalculationInput: TaxCalculationInput) {
    return this.http.post(
      this.baseUrl + 'taxCalculator/calculateTax',
      taxCalculationInput
    );
  }
}
