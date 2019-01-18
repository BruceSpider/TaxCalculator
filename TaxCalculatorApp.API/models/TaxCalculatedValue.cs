using System;

namespace TaxCalculatorApp.API.models
{
    public class TaxCalculatedValue
    {
        public int Id { get; set; }
        public decimal ValueCalculated { get; set; }
        public DateTime DateCalculated { get; set; }
        public decimal AnnualIncome { get; set; }
        public string PostalCode { get; set; }
    }
}