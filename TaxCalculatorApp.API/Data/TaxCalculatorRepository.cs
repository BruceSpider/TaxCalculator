using System;
using System.Threading.Tasks;

namespace TaxCalculatorApp.API.Data {
        public class TaxCalculatorRepository : ITaxCalculatorRepository {

            private readonly DataContext _context;

            public TaxCalculatorRepository (DataContext context) {
                _context = context;
            }

            public decimal calculateProgressiveTax (decimal annualIncome, ref decimal calculatedValue) {
                switch (annualIncome) {
                    case decimal _annualIncome when _annualIncome >= 0 && _annualIncome <= 8350:
                        calculatedValue = Convert.ToDecimal((double)annualIncome * 0.10);
                        break;
                    case decimal _annualIncome when _annualIncome >= 8351 && _annualIncome <= 33950:
                        calculatedValue = Convert.ToDecimal((double)annualIncome * 0.15);
                        break;
                    case decimal _annualIncome when _annualIncome >= 33951 && _annualIncome <= 82250:
                        calculatedValue = Convert.ToDecimal((double)annualIncome * 0.25);
                        break;
                    case decimal _annualIncome when _annualIncome >= 82251 && _annualIncome <= 171550:
                        calculatedValue = Convert.ToDecimal((double)annualIncome * 0.28);
                        break;
                    case decimal _annualIncome when _annualIncome >= 171551 && _annualIncome <= 372950:
                       calculatedValue = Convert.ToDecimal((double)annualIncome * 0.33);
                        break;
                    case decimal _annualIncome when _annualIncome >= 372951:
                        calculatedValue = Convert.ToDecimal((double)annualIncome * 0.35);
                        break;
                }

                return calculatedValue;
            }

            public decimal calculateFlatValue (decimal annualIncome, ref decimal calculatedValue) {
                if (annualIncome >= 171551) {
                     return calculatedValue = 10000;
                } else {
                    return calculatedValue = Convert.ToDecimal((double)annualIncome * 0.05);
                }
            }

            public decimal calculateFlatRate (decimal annualIncome, ref decimal calculatedValue) {
                 return calculatedValue = Convert.ToDecimal((double)annualIncome * 0.175);
            }

            public void Add<T> (T entity) where T : class {
                _context.Add(entity);
            }

            public void Delete<T> (T entity) where T : class {
                _context.Remove (entity);
            }

            public async Task<bool> SaveAll() {
            return await _context.SaveChangesAsync() > 0;
            }

        }
    }