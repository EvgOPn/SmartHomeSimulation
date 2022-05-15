using System.Linq;

namespace UserManagement.Other
{
    public static class FieldValidator
    {
        private const int NameMinLength = 3;
        private const int EmailMinLength = 3;
        private const int PasswordMinLength = 6;
        
        public static bool ValidateEmail(string email)
        {
            return email.Length >= EmailMinLength && email.Contains("@") && email.Contains(".");
        }
        
        public static bool ValidatePassword(string password)
        {
            return password.Length >= PasswordMinLength && password.Where(char.IsUpper).Any();
        }
        
        public static bool ValidateName(string displayName)
        {
            return  displayName.Length >= NameMinLength;
        }
    }
}