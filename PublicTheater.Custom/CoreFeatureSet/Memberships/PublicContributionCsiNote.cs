using System;
using System.Collections.Generic;
using System.Linq;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;

namespace PublicTheater.Custom.CoreFeatureSet.Memberships
{
    public enum PublicContributionType
    {
        GeneralDonation,
        PartnersProgram,
        Membership,
        AnnualGala
    }
    [Serializable]
    public class PublicContributionCsiNote
    {
        private const string IdentityTag = "#Public Theater Contribution#";

        public PublicContributionType ContributionType { get; set; }

        public int SourcePageID { get; set; }
        public string ProgramName { get; set; }
        public string ProgramLevel { get; set; }

        public string DonorName { get; set; }
        public string MatchCompany { get; set; }

        public bool DeclineBenefits { get; set; }
        public bool CommemorativeGift { get; set; }
        public string CommemorativeGiftDetails { get; set; }

        public string FundTitle { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }



        public bool IsAnonymous
        {
            get { return DonorName.Equals(AnonymousString); }
            set { DonorName = AnonymousString; }
        }

        public const string AnonymousString = "Anonymous";

        public override string ToString()
        {
            var properties = new List<string>
                                 {
                                     IdentityTag,
                                     string.Format("ContributionType:{0}", ContributionType),
                                     string.Format("ProgramName:{0}", ProgramName),
                                     string.Format("ProgramLevel:{0}", ProgramLevel),
                                     string.Format("DonorName:{0}", DonorName),
                                     string.Format("MatchCompany:{0}", MatchCompany),
                                     string.Format("DeclineBenefits:{0}", DeclineBenefits),
                                     string.Format("CommemorativeGift:{0}", CommemorativeGift),
                                     string.Format("CommemorativeGiftDetails:{0}", CommemorativeGiftDetails),
                                     string.Format("FundTitle:{0}", FundTitle),
                                     string.Format("Quantity:{0}", Quantity),
                                     string.Format("TotalAmount:{0}", TotalAmount),
                                     string.Format("SourcePageID:{0}", SourcePageID),
                                 };
            return string.Join("|", properties);
        }


        public static PublicContributionCsiNote Parse(string noteString)
        {
            try
            {
                var properties = noteString.Split("|".ToCharArray(), StringSplitOptions.None);
                if (properties.Length > 0 && properties[0] == IdentityTag)
                {
                    var keyValues = properties.Where(prop => prop.Split(":".ToArray()).Length > 1)
                        .ToDictionary(p => p.Split(":".ToArray())[0], p => p.Split(":".ToArray())[1]);

                    var publicPartnersCsiNote = new PublicContributionCsiNote
                    {
                        ContributionType = (PublicContributionType)Enum.Parse(typeof(PublicContributionType), keyValues["ContributionType"], true),
                        ProgramName = keyValues["ProgramName"],
                        ProgramLevel = keyValues["ProgramLevel"],
                        DonorName = keyValues["DonorName"],
                        MatchCompany = keyValues["MatchCompany"],
                        DeclineBenefits = bool.Parse(keyValues["DeclineBenefits"]),
                        CommemorativeGift = bool.Parse(keyValues["CommemorativeGift"]),
                        CommemorativeGiftDetails = keyValues["CommemorativeGiftDetails"],
                        FundTitle = keyValues["FundTitle"],
                        Quantity = int.Parse(keyValues["Quantity"]),
                        TotalAmount = decimal.Parse(keyValues["TotalAmount"]),
                        SourcePageID = int.Parse(keyValues["SourcePageID"])
                    };
                    return publicPartnersCsiNote;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public static PublicContributionCsiNote GetFor(CartLineItem lineItem)
        {
            if (lineItem is ContributionLineItem)
            {
                var csi = CustomerServiceIssue.GetFor(lineItem);
                return csi != null ? Parse(csi.Notes) : null;
            }
            return null;
        }

    }
}
