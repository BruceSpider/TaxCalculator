namespace TaxCalculatorApp.NUnit.Test.Classes {
    public class Auth {
        public bool Login (string username, string password) {
            if (string.IsNullOrEmpty (username) && string.IsNullOrEmpty (password))
                return false;
           
            return true;
        }

        private bool UserExists (string username) {
            if (username == "admin")
                return true;

            return false;
        }

        private bool Register (string username, string email, string password) {
            return true;

        }

        public class ActionResult { }

        public class NotFound : ActionResult { }

        public class BadRequest : ActionResult { }

        public class Ok : ActionResult { }
    }

}