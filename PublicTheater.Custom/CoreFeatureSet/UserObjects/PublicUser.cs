using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Adage.Tessitura;
using Adage.Tessitura.UserObjects;

namespace PublicTheater.Custom.CoreFeatureSet.UserObjects
{
    public class PublicUser : Adage.Tessitura.User
    {
        protected override bool RunLogin(TessSession oSession, int loginType)
        {
            bool loginSuccessful = base.RunLogin(oSession, loginType);

            // if the login didn't succeed and it contains the @ sign then try it with the email specified instead
            if (loginSuccessful == false && string.IsNullOrEmpty(this.UserName) == false && this.UserName.Contains("@"))
            {
                loginSuccessful = Repository.Get<TessituraAPI>().LoginEx2(oSession.SessionKey, string.Empty, Password, loginType, this.PromotionCode,
                    this.UserName, string.Empty, string.Empty, 0, 0, string.Empty);
            }

            return loginSuccessful;
        }

        /// <summary>
        /// User attribute, if user opt-in NY Magazine Offer
        /// </summary>
        public virtual bool NYMagazineOffer
        {
            get
            {
                var attribute = UserAttributes.GetByKeywordNumber(NYMagazineAttributeKeywordNo);
                return attribute != null && attribute.AttributeValue.Equals("1");
            }
        }

        /// <summary>
        /// Save user attribute for NY Magazine offer
        /// </summary>
        /// <param name="subscribe"></param>
        public virtual void SubscribeNYMagazineOffer(bool subscribe)
        {
            try
            {
                var userAttribute = UserAttributes.GetByKeywordNumber(NYMagazineAttributeKeywordNo);
                if (subscribe)
                {
                    if (userAttribute == null)
                    {
                        userAttribute = UserAttributes.NewUserAttribute();
                    }
                    userAttribute.KeywordNumber = NYMagazineAttributeKeywordNo;
                    userAttribute.AttributeName = "NYMag Promo";
                    userAttribute.AttributeValue = "1";
                    userAttribute.Update();
                }
                else
                {
                    if (userAttribute == null)
                    {
                        //no need to create one if not exist
                        return;
                    }
                    userAttribute.KeywordNumber = NYMagazineAttributeKeywordNo;
                    userAttribute.AttributeName = "NYMag Promo";
                    userAttribute.AttributeValue = "0";
                    userAttribute.Update();
                }
            }
            catch (Exception ex)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("PulicUser - Set user NYMagazine attribute - {0} - {1}", ex.Message, ex.StackTrace), null);
            }
        }

        private int NYMagazineAttributeKeywordNo
        {
            get
            {
                return Config.GetValue(Common.PublicAppSettings.NYMagazine_KeyworkNumber, 393);
            }
        }

        /// <summary>
        /// is user a member by look at the ranking
        /// </summary>
        public override bool IsMember
        {
            get
            {
                return this.UserRankings.Any(ur => ur.RankType == Helper.MembershipHelper.MembershipRankType
                                                   && ur.RankValue >= Helper.MembershipHelper.MembershipStartRank
                                                   && ur.RankValue <= Helper.MembershipHelper.MembershipEndRank);
            }
        }

        /// <summary>
        /// is user a partner by look at the ranking
        /// </summary>
        public virtual bool IsPartner
        {
            get
            {
                return this.UserRankings.Any(ur => ur.RankType == Helper.MembershipHelper.PartnerRankType
                                                   && ur.RankValue >= Helper.MembershipHelper.PartnerStartRank
                                                   && ur.RankValue <= Helper.MembershipHelper.PartnerEndRank);
            }
        }


        /// <summary>
        /// is user a partner by look at the ranking
        /// </summary>
        public virtual bool IsSummerSupporter
        {
            get
            {
                return this.UserRankings.Any(ur => ur.RankType == Helper.MembershipHelper.SummerSupporterRankType
                                                   && ur.RankValue >= Helper.MembershipHelper.SummerSupporterStartRank
                                                   && ur.RankValue <= Helper.MembershipHelper.SummerSupporterEndRank);
            }
        }

        /// <summary>
        /// Default Shipping Address to be primary address from tess
        /// </summary>
        public virtual UserAddress ShippingAddress
        {
            get { return PrimaryAddress; }
            set { PrimaryAddress = value; }
        }

        /// <summary>
        /// Billing Address is pulling from tess with Address type ID 21
        /// </summary>
        public virtual UserAddress BillingAddress
        {
            get
            {
                GetAddresses();
                var billingAddress = UserAddresses.FirstOrDefault(ua => ua.AddressTypeId == BillingAddressTypeID && ua.Primary == false);
                if (billingAddress == null)
                {
                    if (string.IsNullOrEmpty(PrimaryAddress.State))
                    {
                        PrimaryAddress.State = "NY";
                    }
                    try
                    {
                        billingAddress = UserAddresses.NewUserAddress();
                        billingAddress.StreetAddress1 = PrimaryAddress.StreetAddress1;
                        billingAddress.StreetAddress2 = PrimaryAddress.StreetAddress2;
                        billingAddress.City = PrimaryAddress.City;
                        billingAddress.State = PrimaryAddress.State;
                        billingAddress.PostalCode = PrimaryAddress.PostalCode;
                        billingAddress.Country = PrimaryAddress.Country;
                        billingAddress.CountryId = PrimaryAddress.CountryId;
                        billingAddress.Primary = false;
                        billingAddress.AddressTypeId = BillingAddressTypeID;

                        billingAddress.Update(false, false);
                        GetAddresses();
                    }
                    catch (Exception exception)
                    {
                        Adage.Common.ElmahCustomError.CustomError.LogError(exception, "Create Billing Address" + PrimaryAddress, null);
                    }
                }

                return billingAddress;
            }
        }

        public virtual int BillingAddressTypeID
        {
            get { return Config.GetValue(Common.PublicAppSettings.BillingAddressTypeID, 21); }
        }


        public override bool Register()
        {
            return base.Register();
        }

        /// <summary>
        /// My Membership Benefits Summary
        /// </summary>
        public Memberships.MyBenefits.MyBenefitSummary MyBenefitSummary
        {
            get { return CoreFeatureSet.Memberships.MyBenefits.MyBenefitSummary.GetMyBenefitSummary(this.CustomerNumber); }
        }

        public static bool IsUSAAddress(UserAddress address)
        {
            return Config.GetValue(TheaterTemplate.Shared.Common.TheaterSharedAppSettings.COUNTRY_CODE_USA, -1) == address.CountryId;
        }

        public static bool IsPlaceHolderAddress(UserAddress address)
        {
            return address.StreetAddress1.Equals(Config.GetValue("DefaultAddress1", "425 Lafayette Street"));
        }

        /// <summary>
        /// Call SP to check email exsits in tessitura associate to an account
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public static bool AllowEmailRegister(string emailAddress)
        {
            var sqlParameters = new Dictionary<string, object>();
            sqlParameters.Add("@email_address", emailAddress);
            try
            {
                var dsResult = TessSession.GetSession().ExecuteLocalProcedure(Common.PublicAppSettings.EmailLookupSP, sqlParameters);
                if (dsResult != null && (dsResult.Tables.Count != 0 && dsResult.Tables[0].Rows.Count != 0))
                {
                    return (int)dsResult.Tables[0].Rows[0][0] == 1;
                }
            }
            catch (Exception exception)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(exception, "AllowEmailRegister", null);
            }
            return true;
        }
    }
}
