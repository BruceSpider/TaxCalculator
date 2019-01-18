namespace TaxCalculatorApp.API.models
{
    public class PostalCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int? TaxCalculationTypeId { get; set; }
        public TaxCalculationType TaxCalculationType { get; set; }
    }
}