using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adage.Common.ElmahCustomError;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using PublicTheater.Custom.CoreFeatureSet.Memberships;
using TheaterTemplate.Shared.EpiServerConfig;
using TheaterTemplate.Shared.EpiServerProperties;

namespace PublicTheater.Web.Views.Pages
{
    public partial class Donate : Custom.Episerver.BaseClasses.PublicBasePage<Custom.Episerver.Pages.DonatePageData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptMembershipLevelCategories.DataSource = CurrentPage.DefaultMembershipLevelItemList;
                rptMembershipLevelCategories.ItemDataBound += donationAmountOptions_ItemDataBound;
                rptMembershipLevelCategories.DataBind();
            }
        }

        void donationAmountOptions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var categoryLevelList = e.Item.DataItem as MembershipLevelItemList;
                if (categoryLevelList != null)
                {
                    var rptMembershipLevels = (Repeater)e.Item.FindControl("rptMembershipLevels");
                    rptMembershipLevels.ItemDataBound += new RepeaterItemEventHandler(rptMembershipLevels_ItemDataBoundCustom);
                    rptMembershipLevels.DataSource = categoryLevelList.PropertyCollection.Where(item => item is MembershipLevelItem);
                    rptMembershipLevels.DataBind();
                }
            }
        }

        /// <summary>
        /// Handles each membership level bound to the funds repeater by populating fields
        /// </summary>
        protected void rptMembershipLevels_ItemDataBoundCustom(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var levelItem = e.Item.DataItem as MembershipLevelItem;
                if (levelItem != null)
                {
                    RadioButton donationRadioBtn = (RadioButton)e.Item.FindControl("btnStartingDonationLevel");
                    donationRadioBtn.Text = "$" + levelItem.StartingLevel;

                    if (e.Item.ItemIndex == 0)
                    {
                        donationRadioBtn.Checked = true;
                        DonationAmount = levelItem.StartingLevel;
                    }
                }
            }
        }

        protected virtual string BuildCsi(DonationFundItem fundItem, decimal donationAmount)
        {
            if (DonationAmount > 0m)
            {
                var builder = new StringBuilder();
                builder.AppendFormat("Fund: {1}-{0}Amount: {2}", new object[] { fundItem.FundId, fundItem.FundDescription, DonationAmount });

                return builder.ToString();
            }

            return string.Empty;
        }

        protected void txtDonationAmt_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = DonationAmount > 0;
        }

        public virtual decimal DonationAmount
        {
            get
            {
                decimal num = 0M;
                decimal.TryParse(tboxDonationAmount.Text.Replace("$", string.Empty).Replace(",", string.Empty), out num);
                return num;
            }
            set
            {
                tboxDonationAmount.Text = value.ToString();
            }
        }

        protected void btnAddDonation_Click(object sender, EventArgs e)
        {
            var defaultFund = CurrentPage.DefaultDonationFund;

            try
            {
                var contribution = new ContributionLineItem();
                var lineItemId = contribution.Add(DonationAmount, defaultFund.FundId, defaultFund.AccountMethodId);
                if (lineItemId > 0)
                {
                    AttachCsiToLineItem(lineItemId, defaultFund, DonationAmount);
                    Response.Redirect(Config.GetValue(TheaterTemplate.Shared.Common.TheaterSharedAppSettings.CART_URL, "/cart/index.aspx"));
                }
            }
            catch (Exception ex)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("Add Contribution - {0} - {1}", ex.Message, ex.StackTrace), null);
            }
        }

        protected virtual void AttachCsiToLineItem(int lineItemId, DonationFundItem fundItem, decimal donationAmount)
        {
            try
            {
                Cart.Flush();
                var cartItem = Cart.GetCart().CartLineItems.First(item => item.ItemId == lineItemId);
                
                var currentUser = Adage.Tessitura.User.GetUser();
                var csi = CSIConfig.GetCSIFromEpiServer(fundItem.CsiPage.ID);
                if (csi != null)
                {
                    var note = new Custom.CoreFeatureSet.Memberships.PublicContributionCsiNote
                    {
                        SourcePageID = CurrentPage.PageLink.ID,
                        ContributionType = PublicContributionType.GeneralDonation,
                        ProgramName = CurrentPage.PageName,
                        ProgramLevel = string.Empty,
                        DonorName = currentUser.IsAnonymousLogin == false && currentUser.LoggedIn ? currentUser.FullName : PublicContributionCsiNote.AnonymousString,
                        MatchCompany = string.Empty,
                        DeclineBenefits = true,
                        CommemorativeGift = false,
                        CommemorativeGiftDetails = string.Empty,
                        FundTitle = fundItem.Title,
                        Quantity = 1,
                        TotalAmount = donationAmount
                    };
                    csi.Notes = note.ToString();
                    csi.AttachTo(cartItem);
                }
            }
            catch (Exception e)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(e, string.Format("Contribution CSI - {0} - {1}", e.Message, e.StackTrace), null);
            }
        }
    }
}