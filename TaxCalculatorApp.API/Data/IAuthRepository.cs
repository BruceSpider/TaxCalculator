using System.Threading.Tasks;
using TaxCalculatorApp.API.models;

namespace TaxCalculatorApp.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<User> GetUser(string username);
         Task<bool> UserExists(string username);
    }
}