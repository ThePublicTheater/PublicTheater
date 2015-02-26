using Adage.Tessitura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using PublicTheater.Custom.CoreFeatureSet.Memberships;
using PublicTheater.Custom.Episerver;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Custom.CoreFeatureSet.Helper
{
    public static class MembershipHelper
    {
        /// <summary>
        /// minimum amount qualifies for partners benefits
        /// </summary>
        public static decimal PartnersMinAmount
        {
            get { return Config.GetValue(Common.PublicAppSettings.PartnersMinAmount, 200m); }
        }

        /// <summary>
        /// minimum amount qualifies for young partners benefits
        /// </summary>
        public static decimal YoungPartnersMinAmount
        {
            get { return Config.GetValue(Common.PublicAppSettings.YoungPartnersMinAmount, 150m); }
        }

        /// <summary>
        /// partners benefits MOS
        /// </summary>
        public static int PartnersMOS
        {
            get { return Config.GetValue(Common.PublicAppSettings.PartnersMOS, 33); }
        }

        /// <summary>
        /// membership benefits mos
        /// </summary>
        public static int MembershipMOS
        {
            get { return Config.GetValue(Common.PublicAppSettings.MembershipMOS, 32); }
        }
        /// <summary>
        /// $150 summer ticket benefit no
        /// </summary>
        public static int BenefitNo_150SummerTicket
        {
            get { return Config.GetValue(Common.PublicAppSettings.BenefitNo_150SummerTicket, 406); }
        }
        /// <summary>
        /// member price ticket benefit no
        /// </summary>
        public static int BenefitNo_MemberPrice
        {
            get { return Config.GetValue(Common.PublicAppSettings.BenefitNo_MemberPrice, 405); }
        }
        /// <summary>
        /// 15% off production benefit no
        /// </summary>
        public static int BenefitNo_15OffProduction
        {
            get { return Config.GetValue(Common.PublicAppSettings.BenefitNo_15OffProduction, 404); }
        }

        /// <summary>
        /// memebership item unit price
        /// </summary>
        public static decimal MembershipUnitPrice
        {
            get { return Config.GetValue(Common.PublicAppSettings.MembershipUnitPrice, 60m); }
        }

        /// <summary>
        /// membership item fee unit price
        /// </summary>
        public static decimal MembershipUnitFee
        {
            get { return Config.GetValue(Common.PublicAppSettings.MembershipUnitFee, 5m); }
        }


        /// <summary>
        /// membership custom fee id in tessitura
        /// </summary>
        public static int MembershipFeeId
        {
            get { return Config.GetValue(Common.PublicAppSettings.MembershipFeeId, 156); }
        }

        /// <summary>
        /// Stored procedure id to call after purchase to grant immediate membership benefits
        /// </summary>
        public static int MembershipUpdateSP
        {
            get { return Config.GetValue(Common.PublicAppSettings.MembershipUpdateSP, 8045); }
        }

        /// <summary>
        /// Stored procedure id to call after purchase to grant immediate membership benefits
        /// </summary>
        public static int PartnerTicketCSI
        {
            get { return Config.GetValue(Common.PublicAppSettings.PartnerTicketCSI, 521); }
        }

        public static int MembershipRankType
        {
            get { return Config.GetValue(Common.PublicAppSettings.MembershipRankType, 2); }
        }

        public static int MembershipStartRank
        {
            get { return Config.GetValue(Common.PublicAppSettings.MembershipStartRank, 100); }
        }
        public static int MembershipEndRank
        {
            get { return Config.GetValue(Common.PublicAppSettings.MembershipEndRank, 1000); }
        }
        public static int PartnerRankType
        {
            get { return Config.GetValue(Common.PublicAppSettings.PartnerRankType, 2); }
        }
        public static int PartnerStartRank
        {
            get { return Config.GetValue(Common.PublicAppSettings.PartnerStartRank, 2000); }
        }
        public static int PartnerEndRank
        {
            get { return Config.GetValue(Common.PublicAppSettings.PartnerEndRank, 4000); }
        }
        public static int SummerSupporterRankType
        {
            get { return Config.GetValue(Common.PublicAppSettings.SummerSupporterRankType, 2); }
        }
        public static int SummerSupporterStartRank
        {
            get { return Config.GetValue(Common.PublicAppSettings.SummerSupporterStartRank, 1100); }
        }
        public static int SummerSupporterEndRank
        {
            get { return Config.GetValue(Common.PublicAppSettings.SummerSupporterEndRank, 1500); }
        }

        public static int MembershipConfigPageId
        {
            get { return Config.GetValue(Common.PublicAppSettings.MembershipConfigPageId, 0); }
        }

        public static int PartnersPaymentMethodID
        {
            get { return Config.GetValue(Common.PublicAppSettings.PartnersPaymentMethodID, 64); }
        }

        public static MembershipConfig GetMembershipConfig()
        {
            return MembershipConfig.GetMembershipConfig(MembershipConfigPageId); 
        }



        /// <summary>
        /// Check against member and partner mode of sale
        /// </summary>
        /// <param name="mos"></param>
        /// <returns></returns>
        public static bool IsMemberPartnerModeOfSale(int mos)
        {
            return mos != 0 && (mos == MembershipMOS || mos == PartnersMOS);
        }

        /// <summary>
        /// Check against member and partner current mode of sale
        /// </summary>
        /// <returns></returns>
        public static bool IsMemberPartnerModeOfSale()
        {
            var mos = Adage.Tessitura.TessSession.GetSession().ModeOfSale;
            return mos != 0 && (mos == MembershipMOS || mos == PartnersMOS);
        }

        /// <summary>
        /// Check immediate access to member/partner benefit by switching mos. with option to empty current shopping cart
        /// </summary>
        /// <param name="contributionType"></param>
        /// <param name="amount"></param>
        /// <param name="removeCartItem"></param>
        public static void ImmediateAccessToMemberBenefits(PublicContributionType contributionType, decimal amount, bool removeCartItem = false)
        {
            try
            {
                if (contributionType == PublicContributionType.GeneralDonation)
                {
                    //general donation and not asking for any benefits
                    return;
                }
                else if (contributionType == PublicContributionType.Membership)
                {
                    if (Repository.Get<TessSession>().ModeOfSale != MembershipMOS && amount >= MembershipUnitPrice)
                    {
                        if (removeCartItem)
                        {
                            RemoveCurrentCartItemsWithMosCheck();
                        }
                        Repository.Get<TessSession>().ChangeModeOfSale(MembershipMOS);
                    }
                }
                else if (contributionType == PublicContributionType.PartnersProgram)
                {
                    if (Repository.Get<TessSession>().ModeOfSale != PartnersMOS && (amount >= YoungPartnersMinAmount || amount >= PartnersMinAmount))
                    {
                        if (removeCartItem)
                        {
                            RemoveCurrentCartItemsWithMosCheck();
                        }
                        Repository.Get<TessSession>().ChangeModeOfSale(PartnersMOS);
                    }
                }
            }
            catch (Exception ex)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("ImmediateAccessToMemberBenefits - {0} - {1}", ex.Message, ex.StackTrace), null);
            }
        }

        /// <summary>
        /// remove all cart item and check mode of sale revert to original
        /// </summary>
        public static void RemoveCurrentCartItemsWithMosCheck()
        {
            var currentCart = Adage.Tessitura.Cart.GetCart();
            var checkMos = currentCart.CartLineItems.Any(item => item is ContributionLineItem);
            foreach (CartLineItem item in currentCart.CartLineItems)
            {
                item.Remove();
            }
            if (checkMos)
            {
                var currentSession = Adage.Tessitura.Repository.Get<Adage.Tessitura.TessSession>();
                if (currentSession.ModeOfSale != currentSession.OriginalMOS)
                {
                    currentSession.ChangeModeOfSale(currentSession.OriginalMOS);
                }
            }
        }


    }
}
