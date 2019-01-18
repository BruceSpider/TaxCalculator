using System.Threading.Tasks;

namespace TaxCalculatorApp.API.Data
{
    public interface ITaxCalculatorRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();

         decimal calculateProgressiveTax(decimal annualIncome, ref decimal calculatedTax);
         decimal calculateFlatValue(decimal annualIncome, ref decimal calculatedTax);
         decimal calculateFlatRate(decimal annualIncome, ref decimal calculatedTax);
    }
}