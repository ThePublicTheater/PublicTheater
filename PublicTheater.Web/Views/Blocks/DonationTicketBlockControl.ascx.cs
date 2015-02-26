using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using PublicTheater.Custom.CoreFeatureSet.Memberships;
using PublicTheater.Custom.Episerver.Blocks;
using TheaterTemplate.Shared.EpiServerConfig;
using TheaterTemplate.Shared.EpiServerProperties;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class DonationTicketBlockControl : Custom.Episerver.BaseClasses.PublicBaseBlockControl<Custom.Episerver.Blocks.DonationTicketBlockData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bindPage();
        }

        protected void bindPage()
        {
            if (CurrentData.SoldOut)
            {
                AvailablePanel.Visible = false;
                SoldOutPanel.Visible = true;
            }
            else
            {
                for (int i = 1; i <= CurrentData.MaxQty; i++)
                {
                    qtyddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }


        }

        protected void reserveButton_Click(object sender, EventArgs e)
        {
            var defaultFund = CurrentData.DefaultDonationFund;

            try
            {
                var contribution = new ContributionLineItem();
                int quantity = Convert.ToInt32(qtyddl.SelectedValue);
                decimal DonationAmount = Convert.ToDecimal(quantity*(CurrentData.Price));
                var lineItemId = contribution.Add(DonationAmount, defaultFund.FundId, defaultFund.AccountMethodId);
                if (lineItemId > 0)
                {
                    AttachCsiToLineItem(lineItemId, defaultFund, DonationAmount,quantity);
                    Response.Redirect(Config.GetValue(TheaterTemplate.Shared.Common.TheaterSharedAppSettings.CART_URL, "/cart/index.aspx"));
                }
            }
            catch (Exception ex)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("Add Contribution - {0} - {1}", ex.Message, ex.StackTrace), null);
            }
        }

           protected virtual void AttachCsiToLineItem(int lineItemId, DonationFundItem fundItem, decimal donationAmount,int quantity)
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
                        Quantity = quantity,
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