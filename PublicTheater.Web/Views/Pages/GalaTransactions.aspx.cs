using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using PublicTheater.Custom.CoreFeatureSet.Memberships;
using PublicTheater.Custom.Episerver.Properties;
using TheaterTemplate.Shared.EpiServerConfig;
using TheaterTemplate.Shared.EpiServerProperties;

namespace PublicTheater.Web.Views.Pages
{
    public partial class GalaTransactions : Custom.Episerver.BaseClasses.PublicBasePage<Custom.Episerver.Pages.GalaTransactionPageData>
    {
        #region properties

        protected const string FundNoAttribute = "FundNo";
        protected const string AmountAttribute = "Amount";
        private List<GalaLevelItem> _availableLevels;
        protected List<GalaLevelItem> AvailableLevels
        {
            get
            {
                return _availableLevels ?? (_availableLevels = CurrentPage.GetGalaLevelGroups()
                                                                 .SelectMany(g => g.GalaLevelList)
                                                                 .Where(item => !item.Disabled)
                                                                 .ToList());
            }
        }
        #endregion

        #region databind

        protected void Page_Load(object sender, EventArgs e)
        {
            btnAddToCart.Click += new EventHandler(btnAddToCart_Click);
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }



        protected virtual void BindData()
        {
            if (CurrentPage.BannerImage != null)
            {
                imgBannerImage.ImageUrl = CurrentPage.BannerImage.ToString();
            }
            rptGalaLevelGroups.ItemDataBound += new RepeaterItemEventHandler(rptGalaLevelGroups_ItemDataBound);
            rptGalaLevelGroups.DataSource = CurrentPage.GetGalaLevelGroups();
            rptGalaLevelGroups.DataBind();
        }

        protected virtual void rptGalaLevelGroups_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var galaLevelGroup = e.Item.DataItem as GalaLevelGroup;
                if (galaLevelGroup != null)
                {
                    var litGroupTitle = (Literal)e.Item.FindControl("litGroupTitle");

                    litGroupTitle.Text = galaLevelGroup.Title;

                    var rptGalaLevels = (Repeater)e.Item.FindControl("rptGalaLevels");
                    rptGalaLevels.ItemDataBound += new RepeaterItemEventHandler(rptGalaLevels_ItemDataBound);
                    rptGalaLevels.DataSource = galaLevelGroup.GalaLevelList;
                    rptGalaLevels.DataBind();
                }
            }


        }

        protected virtual void rptGalaLevels_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var galaLevelItem = e.Item.DataItem as GalaLevelItem;
                if (galaLevelItem != null)
                {
                    var litTitle = (Literal)e.Item.FindControl("litTitle");
                    var litAmountInfo = (Literal)e.Item.FindControl("litAmountInfo");
                    var litDescription = (Literal)e.Item.FindControl("litDescription");
                    var ddlQuantity = (DropDownList)e.Item.FindControl("ddlQuantity");

                    litTitle.Text = galaLevelItem.Title;
                    litAmountInfo.Text = string.Format("{0} ({1})", galaLevelItem.LevelAmount.ToString("C0"), galaLevelItem.CharitableDonation);
                    litDescription.Text = galaLevelItem.Description;

                    ddlQuantity.Attributes[FundNoAttribute] = galaLevelItem.FundNo.ToString();
                    ddlQuantity.Attributes[AmountAttribute] = galaLevelItem.LevelAmount.ToString();

                    ddlQuantity.DataSource = Enumerable.Range(0, 10);
                    ddlQuantity.DataBind();

                    ddlQuantity.Enabled = !galaLevelItem.Disabled;
                }
            }
        }
        #endregion

        #region Add item to cart
        /// <summary>
        /// Get selected gala levels
        /// </summary>
        /// <returns></returns>
        protected Dictionary<GalaLevelItem, int> GetSelectedGalaLevels()
        {
            var selectedLevels = new Dictionary<GalaLevelItem, int>();
            foreach (RepeaterItem item in rptGalaLevelGroups.Items)
            {
                var rptGalaLevels = item.FindControl("rptGalaLevels") as Repeater;
                if (rptGalaLevels != null)
                {
                    foreach (RepeaterItem levelItem in rptGalaLevels.Items)
                    {
                        var ddlQuantity = levelItem.FindControl("ddlQuantity") as DropDownList;
                        if (ddlQuantity != null && Convert.ToInt32(ddlQuantity.SelectedValue) > 0)
                        {
                            var galaItem = AvailableLevels.Single(level =>
                                level.LevelAmount.Equals(decimal.Parse(ddlQuantity.Attributes[AmountAttribute]))
                                && level.FundNo.Equals(int.Parse(ddlQuantity.Attributes[FundNoAttribute]))
                                );
                            selectedLevels.Add(galaItem, int.Parse(ddlQuantity.SelectedValue));
                        }
                    }
                }
            }
            return selectedLevels;
        }

        protected virtual bool AddGalaDonation(DonationFundItem fundItem, decimal donationAmount)
        {
            try
            {
                var contribution = new ContributionLineItem();
                var lineItemId = contribution.Add(donationAmount, fundItem.FundId, fundItem.AccountMethodId);
                if (lineItemId > 0)
                {


                    var csi = CSIConfig.GetCSIFromEpiServer(fundItem.CsiPage.ID);
                    if (csi != null)
                    {
                        var note = new Custom.CoreFeatureSet.Memberships.PublicContributionCsiNote
                        {
                            SourcePageID = CurrentPage.PageLink.ID,
                            ContributionType = PublicContributionType.AnnualGala,
                            ProgramName = CurrentPage.PageName,
                            ProgramLevel = "Contribution",
                            DonorName = cbAnonDonor.Checked ? PublicContributionCsiNote.AnonymousString : txtDonorName.Text,
                            MatchCompany = string.Empty,
                            DeclineBenefits = false,
                            CommemorativeGift = false,
                            CommemorativeGiftDetails = string.Empty,
                            FundTitle = string.Format("{0}-{1}", fundItem.Title, fundItem.FundId),
                            Quantity = 1,
                            TotalAmount = donationAmount
                        };
                        csi.Notes = note.ToString();

                        Adage.Tessitura.Cart.Flush();
                        var cartItem = Adage.Tessitura.Cart.GetCart().CartLineItems.First(item => item.ItemId == lineItemId);
                        csi.AttachTo(cartItem);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("Add GalaDonation Purchase Contribution - {0} - {1}", ex.Message, ex.StackTrace), null);
                return false;
            }
        }

        protected virtual bool AddGalaLevelItem(GalaLevelItem levelItem, int quantity)
        {
            var donationAmount = levelItem.LevelAmount * quantity;
            try
            {
                var contribution = new ContributionLineItem();
                int lineItemId=-1;
                if(levelItem.FundNo>0)
                    lineItemId = contribution.Add(donationAmount, levelItem.FundNo, 0);
                else if(levelItem.OnAcctId>0)
                    lineItemId = contribution.Add(donationAmount, 0, levelItem.OnAcctId);

                if (lineItemId > 0)
                {
                    var csi = CSIConfig.GetCSIFromEpiServer(levelItem.CsiPage.ID);
                    if (csi != null)
                    {
                        var note = new Custom.CoreFeatureSet.Memberships.PublicContributionCsiNote
                        {
                            SourcePageID = CurrentPage.PageLink.ID,
                            ContributionType = PublicContributionType.AnnualGala,
                            ProgramName = CurrentPage.PageName,
                            ProgramLevel = quantity > 1 ? string.Format("{0} X {1}", levelItem.Title, quantity) : levelItem.Title,
                            DonorName = cbAnonDonor.Checked ? PublicContributionCsiNote.AnonymousString : txtDonorName.Text,
                            MatchCompany = string.Empty,
                            DeclineBenefits = false,
                            CommemorativeGift = false,
                            CommemorativeGiftDetails = string.Empty,
                            FundTitle = string.Format("{0}-{1}", levelItem.Title, levelItem.FundNo),
                            Quantity = quantity,
                            TotalAmount = donationAmount
                        };
                        csi.Notes = note.ToString();

                        Adage.Tessitura.Cart.Flush();
                        var cartItem = Adage.Tessitura.Cart.GetCart().CartLineItems.First(item => item.ItemId == lineItemId);
                        csi.AttachTo(cartItem);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("Add Membership Purchase Contribution - {0} - {1}", ex.Message, ex.StackTrace), null);
                return false;
            }
        }

        protected virtual void btnAddToCart_Click(object sender, EventArgs e)
        {
            var selectedLevels = GetSelectedGalaLevels();
            decimal donationAmount;
            decimal.TryParse(txtDonationAmount.Text.Replace("$", string.Empty).Replace(",", string.Empty), out donationAmount);

            if (selectedLevels.Count <= 0 && donationAmount <= 0)
            {
                propInvalidLevelError.Visible = true;
                return;
            }

            bool allDonationAdded = true;

            foreach (var selectedLevel in selectedLevels)
            {
                allDonationAdded = allDonationAdded && AddGalaLevelItem(selectedLevel.Key, selectedLevel.Value);
            }
            if (donationAmount > 0)
            {
                allDonationAdded = allDonationAdded && AddGalaDonation(CurrentPage.AdditionalGalaDonationFund, donationAmount);
            }

            Response.Redirect(Config.GetValue(TheaterTemplate.Shared.Common.TheaterSharedAppSettings.CART_URL, "/cart/index.aspx"));

        }
        #endregion
    }
}