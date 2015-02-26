using System;
using System.Collections.Generic;
using System.Linq;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using Adage.Theater.RelationshipManager.Helpers;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using PublicTheater.Custom.CoreFeatureSet.Memberships;
using PublicTheater.Custom.CoreFeatureSet.UserObjects;
using TheaterTemplate.Shared.Common;

namespace PublicTheater.Web.Views.Controls
{
    public partial class MemberMosCheck : Custom.Episerver.BaseClasses.PublicBaseUserControl
    {
        private PublicUser _currentUser;
        protected virtual PublicUser CurrentUser
        {
            get { return _currentUser ?? (_currentUser = User.GetUser() as PublicUser); }
        }

        protected virtual Custom.CoreFeatureSet.EpiServerConfig.MembershipConfig CurrentConfig
        {
            get { return Custom.CoreFeatureSet.Helper.MembershipHelper.GetMembershipConfig(); }
        }

        protected virtual bool EnableMosCheck
        {
            get
            {
                if (CurrentPage is Custom.Episerver.BaseClasses.PublicBasePageData)
                    return ((Custom.Episerver.BaseClasses.PublicBasePageData)CurrentPage).EnableMosCheck;

                if (CurrentPage.Property["EnableMosCheck"] is EPiServer.Core.PropertyBoolean)
                {
                    var enableMosCheck = (EPiServer.Core.PropertyBoolean)CurrentPage.Property["EnableMosCheck"];
                    return enableMosCheck.Boolean;
                }

                return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lbOK.Click += new EventHandler(lbOK_Click);
            if (!IsPostBack)
            {
                CheckMembershipMosConfig();
            }
        }

        private void lbOK_Click(object sender, EventArgs e)
        {
            mdlChangeAddress.Hide();
            Response.Redirect(Config.GetValue(TheaterSharedAppSettings.CART_URL, "~/Cart/index.aspx"));
        }

        private void CheckMembershipMosConfig()
        {
            //enabled?
            if (!EnableMosCheck)
            {
                return;
            }

            //member or partner not in expected MOS, dump the cart and switch mos
            if (ViolateMemberPartnerMOS())
            {
                return;
            }

            if (ViolateBenefitsRestriction())
            {
                return;
            }

            //can not purchase more than one membership items at once
            if (ViolateCartDuplications())
            {
                return;
            }
        }

        /// <summary>
        /// check member benefits restriction for switching between programs
        /// </summary>
        /// <returns></returns>
        protected bool ViolateBenefitsRestriction()
        {
            //logged in? no need to check for anonymous user
            if (CurrentUser.IsAnonymousLogin || CurrentUser.LoggedIn == false)
            {
                return false;
            }
            var currentCart = Adage.Tessitura.Cart.GetCart();
            var cartContributions = currentCart.CartLineItems.OfType<ContributionLineItem>().ToList();
            if (cartContributions.Any() == false)
            {
                //no membership item in cart
                return false;
            }

            var benefitRestrictions = BenefitRestrictions.GetBenefitRestrictions(CurrentUser.CustomerNumber);

            if (benefitRestrictions.Any() == false)
            {
                //no restrictions found for this user
                return false;
            }

            var foundViolatedItem = false;
            foreach (ContributionLineItem lineItem in cartContributions)
            {

                if (benefitRestrictions.Any(rest =>
                    rest.FundId == lineItem.ContributionFundId
                    && rest.StartLevel <= lineItem.Amount
                    && rest.EndLevel >= lineItem.Amount))
                {
                    //match restriction? remove it!
                    lineItem.Remove();
                    
                    var membershipNotes = PublicContributionCsiNote.GetFor(lineItem);
                    if (membershipNotes != null && membershipNotes.ContributionType == PublicContributionType.Membership)
                    {
                        //it's a membership item, remove associated fees
                        foreach (var qty in Enumerable.Range(0, membershipNotes.Quantity))
                        {
                            var associatedFee = currentCart.CartLineItemFees.FirstOrDefault(fee => fee.FeeNumber == MembershipHelper.MembershipFeeId);
                            if (associatedFee != null)
                            {
                                associatedFee.Remove();
                            }
                        }
                    }

                    foundViolatedItem = true;
                }
            }

            if (foundViolatedItem)
            {
                //show dialog explain what happened to your cart
                litContent.Text = CurrentConfig.BenefitRestrictionsMessage;
                if (!string.IsNullOrEmpty(CurrentConfig.BenefitRestrictionsConfirmButton))
                {
                    lbOK.Text = CurrentConfig.BenefitRestrictionsConfirmButton;
                }
                mdlChangeAddress.Show();
            }

            return foundViolatedItem;
        }

        /// <summary>
        /// check if violate cart duplications
        /// </summary>
        /// <returns></returns>
        protected bool ViolateCartDuplications()
        {
            //disabled
            if (CurrentConfig.CartDuplicationCheckEnabled == false)
            {
                return false;
            }


            var currentCart = Adage.Tessitura.Cart.GetCart();
            var contributionLineItems = currentCart.CartLineItems.OfType<ContributionLineItem>().ToList();
            if (contributionLineItems.Any() == false)
            {
                return false;
            }

            var membershipItems = new Dictionary<CartLineItem, PublicContributionType>();
            foreach (ContributionLineItem item in contributionLineItems)
            {
                var membershipNotes = PublicContributionCsiNote.GetFor(item);
                if (membershipNotes != null)
                {

                    if (membershipNotes.ContributionType == PublicContributionType.PartnersProgram
                    || membershipNotes.ContributionType == PublicContributionType.Membership)
                    {
                        membershipItems.Add(item, membershipNotes.ContributionType);
                    }
                }
            }

            if (membershipItems.Any() == false)
            {
                return false;
            }

            // if all are membership items, it is fine as long as they are not cross programs
            if (membershipItems.All(kv => kv.Value == PublicContributionType.Membership))
            {
                return false;
            }

            // remove extra line items from cart if there were more than one member program items in cart
            if (membershipItems.Count > 1)
            {
                bool firstItemSkipped = false;
                foreach (var membershipItem in membershipItems)
                {
                    if (firstItemSkipped)
                    {
                        membershipItem.Key.Remove();
                    }
                    else
                    {
                        firstItemSkipped = true;
                    }
                }

                litContent.Text = CurrentConfig.CartDuplicationCheckMessage;
                if (!string.IsNullOrEmpty(CurrentConfig.CartDuplicationCheckConfirmaButton))
                {
                    lbOK.Text = CurrentConfig.CartDuplicationCheckConfirmaButton;
                }
                mdlChangeAddress.Show();
                return true;
            }
            return false;
        }

        /// <summary>
        /// check if member or parter in the proper mode of sale
        /// </summary>
        /// <returns></returns>
        protected bool ViolateMemberPartnerMOS()
        {
            //mos check disabled
            if (CurrentConfig.MemberMosCheckEnabled == false)
            {
                return false;
            }
            //logged in? no need to check for anonymous user
            if (CurrentUser.IsAnonymousLogin || CurrentUser.LoggedIn == false)
            {
                return false;
            }
            //user is not a member or partner
            if (!CurrentUser.IsMember && !CurrentUser.IsPartner)
            {
                return false;
            }

            //force switch to member or partner mode of sale
            var mosTo = CurrentUser.IsMember ? MembershipHelper.MembershipMOS : MembershipHelper.PartnersMOS;
            var currentSession = Adage.Tessitura.Repository.Get<Adage.Tessitura.TessSession>();
            if (currentSession.ModeOfSale != mosTo)
            {
                var currentCart = Adage.Tessitura.Cart.GetCart();
                foreach (CartLineItem item in currentCart.CartLineItems)
                {
                    item.Remove();
                }
                currentSession.ChangeModeOfSale(mosTo);
                litContent.Text = CurrentConfig.MemberMosCheckMessage;
                if (!string.IsNullOrEmpty(CurrentConfig.MemberMosCheckConfirmButton))
                {
                    lbOK.Text = CurrentConfig.MemberMosCheckConfirmButton;
                }
                mdlChangeAddress.Show();
                return true;
            }
            return false;
        }
    }
}