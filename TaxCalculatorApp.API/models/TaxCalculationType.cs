using System.Collections.Generic;

namespace TaxCalculatorApp.API.models
{
    public class TaxCalculationType 
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<PostalCode> PostalCodes { get; set; }
    }
}