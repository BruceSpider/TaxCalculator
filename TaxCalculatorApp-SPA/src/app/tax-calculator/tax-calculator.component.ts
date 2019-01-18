import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { TaxCalculationInput } from '../_models/TaxCalculationInput';
import { TaxCalculatorService } from '../_services/tax-calculator.service';
import { TaxCalculationOutput } from '../_models/TaxCalculationOutput';

@Component({
  selector: 'app-tax-calculator',
  templateUrl: './tax-calculator.component.html',
  styleUrls: ['./tax-calculator.component.css']
})
export class TaxCalculatorComponent implements OnInit {
  taxCalculationInput: TaxCalculationInput;
  taxCalculationOutput: any;
  taxCalculatorForm: FormGroup;
  displayMessage = '';
  loading = false;
  calculatedTax?: number;

  constructor(
    private taxCalcService: TaxCalculatorService,
    private alertify: AlertifyService,
    private fb: FormBuilder,
    private router: Router
  ) {}

  ngOnInit() {
    this.InitTaxCalculatorForm();
    this.calculatedTax = null;
  }

  InitTaxCalculatorForm(): any {
    this.taxCalculatorForm = this.fb.group({
      annualIncome: ['', [Validators.required, Validators.pattern('[0-9]*')]],
      postalCode: ['', [Validators.required, Validators.pattern('[a-zA-Z0-9]*')]]
    });
  }

  calculateTax() {
    if (this.taxCalculatorForm.valid) {
      this.taxCalculationInput = Object.assign(
        {},
        this.taxCalculatorForm.value
      );
      this.taxCalcService.calculateTax( this.taxCalculationInput).subscribe(
        (response)  => {
          this.taxCalculationOutput = response;
        },
        error => {
          this.alertify.error(error);
        });
      } else {
      const controls = this.taxCalculatorForm.controls;
      Object.keys(controls).forEach(controlName =>
        controls[controlName].markAsTouched()
      );
      return false;
    }
  }
}
