using System.ComponentModel.DataAnnotations;

namespace TaxCalculatorApp.API.DTOs
{
    public class PostalCodeDTO
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int TaxCalculationTypeId { get; set; }
    }
}