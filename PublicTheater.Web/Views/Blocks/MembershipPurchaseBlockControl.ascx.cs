using System;
using System.Linq;
using System.Text;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using EPiServer.Web;
using PublicTheater.Custom.CoreFeatureSet.Memberships;
using TheaterTemplate.Shared.EpiServerConfig;
using TheaterTemplate.Shared.EpiServerProperties;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class MembershipPurchaseBlockControl : Custom.Episerver.BaseClasses.PublicBaseResponsiveBlockControl<Custom.Episerver.Blocks.MembershipPurchaseBlockData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnAdd.Click += new EventHandler(btnAdd_Click);
            if (!IsPostBack)
            {
                ddlQuantity.DataSource = Enumerable.Range(1, 10);
                ddlQuantity.DataBind();

                if (string.IsNullOrEmpty(CurrentData.AddToCartText) == false)
                {
                    btnAdd.Text = CurrentData.AddToCartText;
                }
            }
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            var defaultFund = CurrentData.DefaultDonationFund;
            var quantity = int.Parse(ddlQuantity.SelectedValue);
            var donationAmount = Custom.CoreFeatureSet.Helper.MembershipHelper.MembershipUnitPrice * quantity;

            try
            {
                //try get membership benefits right away!
                // disable immediate access to benefits for now. will be included in phase 1.5  -WZ @ 02/12/2014
                //Custom.CoreFeatureSet.Helper.MembershipHelper.ImmediateAccessToMemberBenefits(PublicContributionType.Membership, donationAmount);

                var contribution = new ContributionLineItem();
                var lineItemId = contribution.Add(donationAmount, defaultFund.FundId, defaultFund.AccountMethodId);
                if (lineItemId > 0)
                {
                    AttachFeeandCsiToLineItem(lineItemId, defaultFund, quantity, donationAmount);
                    Response.Redirect(Config.GetValue(TheaterTemplate.Shared.Common.TheaterSharedAppSettings.CART_URL, "/cart/index.aspx"));
                }
            }
            catch (Exception ex)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("Add Membership Purchase Contribution - {0} - {1}", ex.Message, ex.StackTrace), null);
            }
        }

        protected virtual void AttachFeeandCsiToLineItem(int lineItemId, DonationFundItem fundItem, int quantity, decimal donationAmount)
        {
            try
            {
                Cart.Flush();
                var cartItem = Cart.GetCart().CartLineItems.First(item => item.ItemId == lineItemId);
                /*
                 * No membership fee in the new fiscal year
                foreach (var quantityFee in Enumerable.Range(0,quantity))
                {
                    var fee = Cart.GetCart().CartLineItemFees.AddCartLineItemFee(Custom.CoreFeatureSet.Helper.MembershipHelper.MembershipFeeId);
                    if (fee.Amount.Equals(0)) { fee.Remove(); }
                }
               /* */
                var currentUser = Adage.Tessitura.User.GetUser();
                
                var csi = CSIConfig.GetCSIFromEpiServer(fundItem.CsiPage.ID);
                if (csi != null)
                {
                    var note = new Custom.CoreFeatureSet.Memberships.PublicContributionCsiNote
                                   {
                                       SourcePageID = CurrentPage.PageLink.ID,
                                       ContributionType = PublicContributionType.Membership,
                                       ProgramName = "Membership",
                                       ProgramLevel = string.Format("Membership X {0}",quantity),
                                       DonorName = currentUser.IsAnonymousLogin == false && currentUser.LoggedIn? currentUser.FullName: PublicContributionCsiNote.AnonymousString,
                                       MatchCompany = string.Empty,
                                       DeclineBenefits = false,
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