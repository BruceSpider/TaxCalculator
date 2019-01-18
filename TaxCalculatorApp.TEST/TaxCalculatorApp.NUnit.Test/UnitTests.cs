using NUnit.Framework;
using TaxCalculatorApp.NUnit.Test.Classes;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void Login_ShouldAuthenticateInvalidUser_ReturnsFalse()
        {
            // Arrange : initialize objects
            var auth = new Login();
            //Act
            var result = auth.SigIn("invalidUsername", "invalidPassword");
            // Assert
            Assert.AreEqual("Loggeed in", result);
        }

          [Test]
        public void Register_ShouldValidateUserInput_ReturnsTrue()
        {
            // Arrange : initialize objects
            var auth = new Register();
            //Act
            var result = auth.Register("validUsername", "validPassword");
            // Assert
            Assert.AreEqual("Registered successfuly", result);
        }

       
    }
}