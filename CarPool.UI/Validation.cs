using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CarPool.UI
{
    //class contains methods used for validation purpose
    static class Validation
    {
        const string MatchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
       + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
       + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
       + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";
        const string MatchMobilePattern = @"^[0-9]{10}$";
        const string MatchNamePattern = @"^[a-zA-Z]{1,}$";
        const string MatchPassword = @"(?=.{8,})";
        const string MatchNumberPlate = @"^(([A-Za-z]){2,3}(|-)(?:[0-9]){1,2}(|-)(?:[A-Za-z]){2}(|-)([0-9]){1,4})|(([A-Za-z]){2,3}(|-)([0-9]){1,4})$";


        internal static  bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, MatchEmailPattern);
        }
        internal static bool IsValidMblNumber(string mblNumber)
        {
            return Regex.IsMatch(mblNumber, MatchMobilePattern);
        }
        internal static bool IsValidName(string name)
        {
            return Regex.IsMatch(name, MatchNamePattern);
        }
        internal static bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, MatchPassword);
        }
        internal static bool IsValidNumberPlate(string numberPlate)
        {
            return Regex.IsMatch(numberPlate, MatchNumberPlate);
        }
    }
}
