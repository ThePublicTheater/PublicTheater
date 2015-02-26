using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PublicTheater.Custom.Episerver.Utility
{
    public sealed class Validation
    {
        public static bool IsNullOrEmpty(string text)
        {
            return string.IsNullOrEmpty(text);
        }

        public static bool IsAlpha(string value)
        {
            return !Regex.IsMatch(value, "[^a-zA-Z]");
        }

        public static bool IsAlphaNumeric(string value)
        {
            return !Regex.IsMatch(value, "[^a-zA-Z0-9]");
        }

        public static bool IsCreditCardBigFour(string creditCardNumber)
        {
            return ((Regex.IsMatch(creditCardNumber, @"^(?:(?:[5][1-5])(?:\d{14}))$") || Regex.IsMatch(creditCardNumber, @"^(?:(?:[4])(?:\d{12}|\d{15}))$")) || (Regex.IsMatch(creditCardNumber, @"^(?:(?:[3][4|7])(?:\d{13}))$") || Regex.IsMatch(creditCardNumber, @"^(?:(?:6011)(?:\d{12}))$")));
        }

        public static bool IsCreditCardNumber(string creditCardNumber)
        {
            byte num;
            var builder = new StringBuilder();
            short num2 = 0;
            if (creditCardNumber == null)
            {
                return false;
            }
            var str = Regex.Replace(creditCardNumber, "([^0-9]+)", string.Empty);
            if ((str.Length < 13) || (str.Length > 0x13))
            {
                return false;
            }
            int num3 = str.Length - 1;
        Label_00AA:
            const int num6 = 0;
            if (num3 >= num6)
            {
                if ((((str.Length - num3) + 1) % 2) == 1)
                {
                    num = byte.Parse(Convert.ToString(str[num3]));
                    builder.Append(2 * num);
                }
                else
                {
                    builder.Append(str[num3]);
                }
                num3 += -1;
                goto Label_00AA;
            }
            var num5 = builder.Length - 1;
            for (var i = 0; i <= num5; i++)
            {
                num = byte.Parse(Convert.ToString(builder[i]));
                num2 = (short)(num2 + num);
            }
            return ((num2 % 10) == 0);
        }

        public static bool IsEmail(string value)
        {
            return Regex.IsMatch(value, "^([-_0-9a-zA-Z]+[-._+&])*[-_0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$");
        }

        public static bool IsNumericText(string value)
        {
            return Regex.IsMatch(value, @"^\d+$");
        }

        public static bool IsNonNegativeIntegerText(string value)
        {
            int iValue;
            return int.TryParse(value, out iValue) && iValue >= 0;
        }

        public static bool IsNonNegativeDecimalText(string value)
        {
            decimal iValue;
            return decimal.TryParse(value, out iValue) && iValue >= 0;
        }

        public static bool IsPhoneNumber(string value)
        {
            return Regex.IsMatch(value, @"^(1?(-?\d{3})-?)?(\d{3})(-?\d{4})$");
        }

        public static bool IsUSState(string state)
        {
            state = state.ToUpper();
            return new[] { 
                                 "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "HI", "ID", "IL", "IN", "IA", 
                                 "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", 
                                 "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", 
                                 "WV", "WI", "WY"
                             }.Any(str => str.Equals(state))
            ||
            new[] {
                           "ALABAMA", "ALASKA", "ARIZONA", "ARKANSAS", "CALIFORNIA", "COLORADO", "CONNECTICUT", "DELAWARE", "FLORIDA", "GEORGIA", "HAWAII", "IDAHO", "ILLINOIS", "INDIANA", "IOWA", "KANSAS", "KENTUCKY", "LOUISIANA", "MAINE", "MARYLAND", "MASSACHUSETTS", "MICHIGAN", "MINNESOTA", "MISSISSIPPI", "MISSOURI", "MONTANA", "NEBRASKA", "NEVADA", "NEW HAMPSHIRE", "NEW JERSEY", "NEW MEXICO", "NEW YORK", "NORTH CAROLINA", "NORTH DAKOTA", "OHIO", "OKLAHOMA", "OREGON", "PENNSYLVANIA", "RHODE ISLAND", "SOUTH CAROLINA", "SOUTHDAKOTA", "TENNESSEE", "TEXAS", "UTAH", "VERMONT", "VIRGINIA", "WASHINGTON", "WEST VIRGINIA", "WISCONSIN", "WYOMING"
                       }.Any(str2 => str2.Equals(state));
        }

        public static bool IsUSTaxId(string taxId, bool strictValidation)
        {
            if (taxId == null)
            {
                return false;
            }
            if (strictValidation)
            {
                return Regex.IsMatch(taxId, @"^\d{2}-\d{7}$");
            }
            return Regex.IsMatch(taxId, @"^\d{2}(-|\s)*\d{7}$");
        }

        public static bool IsWebsite(string url)
        {
            return Regex.IsMatch(url, @"^(ht|f)tp(s?)://([a-zA-Z]{2,}[^/]+[a-zA-Z]{2,})/?[a-zA-Z0-9-_.\/@&\?]*$");
        }

        public static bool IsZipCodeAny(string zipCode)
        {
            return Regex.IsMatch(zipCode, @"^\d{5}((-|\s)?\d{4})?$");
        }

        public static bool IsZipCodeFive(string zipCode)
        {
            return Regex.IsMatch(zipCode, @"^\d{5}$");
        }

        public static bool IsZipCodeFivePlusFour(string zipCode)
        {
            return Regex.IsMatch(zipCode, @"^\d{5}((-|\s)?\d{4})$");
        }
    }
}

