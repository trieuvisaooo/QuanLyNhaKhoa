using System.Text.RegularExpressions;

namespace QuanLyNhaKhoa.Helpers
{
    public class LogInHelper
    {
        public enum Role
        {
            Customer = 0,
            Receptionist = 1,
            Dentist = 2,
            Admin = 3,
        }    
        public Role selectedRole = Role.Customer;

        public string phoneNumber = "";
        public string password = "";
        public static string NormalizeVietnamesePhoneNumber(string phoneNumber)
        {
            // Remove all non-digit characters except for '+', '(', ')', and spaces:
            phoneNumber = Regex.Replace(phoneNumber, @"[^\d+()\s]", "");

            // Format the number according to Vietnamese conventions:
            phoneNumber = Regex.Replace(phoneNumber, @"\s+", ""); // Remove any extra spaces
            phoneNumber = Regex.Replace(phoneNumber, @"^\+", "00"); // Replace '+' with '00'
            phoneNumber = Regex.Replace(phoneNumber, @"^\(0084\)", "0"); // Remove unnecessary '(0084)' prefix
            phoneNumber = Regex.Replace(phoneNumber, @"^0*(84)", "0"); // Simplify '0084' to '0'
            phoneNumber = Regex.Replace(phoneNumber, @"^0+(1)", "0"); // Simplify '01' to '0'
            phoneNumber = Regex.Replace(phoneNumber, @"^0(2|3|4|5|6|7|8)(\d{8})$", "0$1$2");

            // Ensure length and prefix validity:
            if (phoneNumber.Length < 10 || phoneNumber.Length > 11 || !phoneNumber.StartsWith("0"))
            {
                return phoneNumber;

            }

            return phoneNumber;
        }
    }
}
