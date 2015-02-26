using System;
using System.Text.RegularExpressions;


namespace PublicTheater.Custom.Episerver.Utility
{
    public static class CreditCard
    {
        public static CreditCardType GetCardType(string cardNumber)
        {
            CreditCardType undefined = CreditCardType.Undefined;
            cardNumber = Regex.Replace(cardNumber, @"\D", string.Empty);
            if (((cardNumber.Length >= 10) && (cardNumber.Length <= 0x13)) &&
                Validation.IsCreditCardNumber(cardNumber))
            {
                switch (Convert.ToInt32(cardNumber.Substring(0, 2)))
                {
                    case 0x22:
                    case 0x25:
                        return CreditCardType.AmericanExpress;

                    case 0x24:
                        return CreditCardType.DinersClub;

                    case 0x26:
                        return CreditCardType.CarteBlanche;

                    case 0x33:
                    case 0x34:
                    case 0x35:
                    case 0x36:
                    case 0x37:
                        return CreditCardType.MasterCard;
                }
                int num2 = Convert.ToInt32(cardNumber.Substring(0, 4));
                if ((num2 == 0x7de) || (num2 == 0x865))
                {
                    undefined = CreditCardType.EnRoute;
                }
                else
                {
                    if ((num2 == 0x853) || (num2 == 0x708))
                    {
                        return CreditCardType.Jcb;
                    }
                    if (num2 == 0x177b)
                    {
                        undefined = CreditCardType.Discover;
                    }
                    else
                    {
                        switch (Convert.ToInt32(cardNumber.Substring(0, 3)))
                        {
                            case 300:
                            case 0x12d:
                            case 0x12e:
                            case 0x12f:
                            case 0x130:
                            case 0x131:
                                return CreditCardType.DinersClub;
                        }
                        switch (Convert.ToInt32(cardNumber.Substring(0, 1)))
                        {
                            case 3:
                                return CreditCardType.Jcb;

                            case 4:
                                return CreditCardType.Visa;
                        }
                    }
                }
            }
            return undefined;
        }

        public static string GetCardTypeString(string cardNumber)
        {
            CreditCardType cardType = GetCardType(cardNumber);
            switch (((int)cardType))
            {
                case 0:
                    return "Unknown";

                case 1:
                    return "American Express";

                case 2:
                    return "Diners Club";

                case 3:
                    return "Carte Blanche";

                case 4:
                    return "Discover";

                case 5:
                    return "En Route";

                case 6:
                    return "JCB";

                case 7:
                    return "MasterCard";

                case 8:
                    return "Visa";
            }
            throw new NotImplementedException("This card type doesn't have a string representation: " +
                                              Convert.ToString((int)cardType));
        }
        
        public enum CreditCardType
        {
            Undefined,
            AmericanExpress,
            DinersClub,
            CarteBlanche,
            Discover,
            EnRoute,
            Jcb,
            MasterCard,
            Visa
        }
    }
}

