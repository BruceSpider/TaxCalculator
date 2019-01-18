using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxCalculatorApp.API.Data;
using TaxCalculatorApp.API.DTOs;
using TaxCalculatorApp.API.models;

namespace TaxCalculatorApp.API.Controllers {
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase {
        private readonly ITaxCalculatorRepository _repo;

        public TaxCalculatorController (ITaxCalculatorRepository repo) {
            _repo = repo;
        }

        [HttpPost ("calculateTax")]
        public async Task<IActionResult> calculateTaxAsync (TaxToCalculateDTO taxToCalculateDTO) {
            decimal calculatedTax = 0;
            var populatedTaxCalculatedValues = new TaxCalculatedValue ();
            switch (taxToCalculateDTO.PostalCode) {
                case string _postalCode when taxToCalculateDTO.PostalCode == "7441":
                    calculatedTax = _repo.calculateProgressiveTax (taxToCalculateDTO.AnnualIncome, ref calculatedTax);
                    populatedTaxCalculatedValues = PopulateTaxCalculatedValue (calculatedTax, taxToCalculateDTO);
                    _repo.Add (populatedTaxCalculatedValues);
                    if (await _repo.SaveAll ()) {
                        return Ok (populatedTaxCalculatedValues);
                    }
                    break;
                case string _postalCode when taxToCalculateDTO.PostalCode == "A100":
                    calculatedTax = _repo.calculateFlatValue (taxToCalculateDTO.AnnualIncome, ref calculatedTax);
                    populatedTaxCalculatedValues = PopulateTaxCalculatedValue (calculatedTax, taxToCalculateDTO);
                    _repo.Add (populatedTaxCalculatedValues);
                    if (await _repo.SaveAll ()) {
                        return Ok (populatedTaxCalculatedValues);
                    }
                    break;
                case string _postalCode when taxToCalculateDTO.PostalCode == "7000":
                    calculatedTax = _repo.calculateFlatRate (taxToCalculateDTO.AnnualIncome, ref calculatedTax);
                    populatedTaxCalculatedValues = PopulateTaxCalculatedValue (calculatedTax, taxToCalculateDTO);
                    _repo.Add (populatedTaxCalculatedValues);
                    if (await _repo.SaveAll ()) {
                        return Ok (populatedTaxCalculatedValues);
                    }
                    break;
                case string _postalCode when taxToCalculateDTO.PostalCode == "1000":
                    calculatedTax = _repo.calculateProgressiveTax (taxToCalculateDTO.AnnualIncome, ref calculatedTax);
                    populatedTaxCalculatedValues = PopulateTaxCalculatedValue (calculatedTax, taxToCalculateDTO);
                    _repo.Add (populatedTaxCalculatedValues);
                    if (await _repo.SaveAll ()) {
                        return Ok (populatedTaxCalculatedValues);
                    }
                    break;
            }

            return Ok (calculatedTax);
        }

        private TaxCalculatedValue PopulateTaxCalculatedValue (decimal calculatedTax, TaxToCalculateDTO taxToCalculateDTO) {
            return new TaxCalculatedValue {
                ValueCalculated = calculatedTax,
                    DateCalculated = DateTime.Now,
                    AnnualIncome = taxToCalculateDTO.AnnualIncome,
                    PostalCode = taxToCalculateDTO.PostalCode
            };
        }

    }
}