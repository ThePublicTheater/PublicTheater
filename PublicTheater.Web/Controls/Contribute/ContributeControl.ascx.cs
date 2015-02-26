using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using EPiServer.Core;
using PublicTheater.Custom.CoreFeatureSet.Memberships;
using PublicTheater.Custom.Episerver.Properties;
using TheaterTemplate.Shared.EpiServerConfig;
using TheaterTemplate.Shared.EpiServerProperties;
using TheaterTemplate.Shared.WebControls;

namespace PublicTheater.Web.Controls.Contribute
{
    /// <summary>
    /// Contribute Control - Control used for a full page donation that contains membership information and donations
    /// </summary>
    public partial class ContributeControl : TheaterTemplate.Web.Controls.ContributeControls.ContributeControl
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                if (DisableAcknowledgement)
                {
                    pnlAcknowledgement.Visible = false;
                    cbAnonDonor.Checked = true;
                }
            }
        }

        FundMappings CurrentFundMapping
        {
            get { return CurrentPage.Property["FundMapping"] as FundMappings; }
        }

        bool DisableAcknowledgement
        {
            get { return CurrentPage.Property["DisableAcknowledgement"] != null 
                && CurrentPage.Property["DisableAcknowledgement"].Value != null 
                && (bool)CurrentPage.Property["DisableAcknowledgement"].Value; }
        }

        protected override DonationFundItem GetSelectedFund()
        {
            if (CurrentFundMapping != null)
            {
                var foundMapping = CurrentFundMapping.FundMappingList.Where(p => p.StartLevel <= DonationAmount)
                    .OrderByDescending(p => p.StartLevel)
                    .FirstOrDefault();

                if (foundMapping != null)
                {
                    var fundListFound = ContributeConfiguration.FundList.FirstOrDefault(item => item.FundId == foundMapping.FundID);
                    if (fundListFound != null)
                    {
                        return fundListFound;
                    }
                }
            }
            return base.GetSelectedFund();
        }


        protected override string BuildCsi(DonationFundItem fundItem, decimal donationAmount)
        {
            var avaialbeLevels = ContributeConfiguration.MembershipLevelsList.SelectMany(list => list.PropertyCollection.OfType<MembershipLevelItem>()).ToList();
            var selectedLevel = avaialbeLevels.Where(item => donationAmount >= item.StartingLevel).OrderByDescending(item => item.StartingLevel).FirstOrDefault();

            var csiNote = new PublicContributionCsiNote
                            {
                                SourcePageID = CurrentPage.PageLink.ID,
                                ContributionType = PublicContributionType.PartnersProgram,
                                ProgramName = CurrentPage.PageName,
                                ProgramLevel = selectedLevel == null ? string.Empty : selectedLevel.Title,
                                DonorName = cbAnonDonor.Checked ? PublicContributionCsiNote.AnonymousString : txtDonorName.Text,
                                MatchCompany = txtCompanyName.Text,
                                DeclineBenefits = cbNoBenfits.Checked,
                                CommemorativeGift = cbCommemorativeGift.Checked,
                                FundTitle = fundItem.Title,
                                Quantity = 1,
                                TotalAmount = donationAmount
                            };

            if (cbCommemorativeGift.Checked)
            {
                var csiBuilder = new StringBuilder();
                csiBuilder.AppendFormat("{0}Honoree: {1}", Environment.NewLine, txtHonoree);
                if (!string.IsNullOrWhiteSpace(txtName.Text))
                {
                    csiBuilder.AppendFormat("{0}Honoree Name For Address: {1}", Environment.NewLine, txtName.Text);
                }
                if (!string.IsNullOrWhiteSpace(txtAddress.Text) && !string.IsNullOrWhiteSpace(txtAddress2.Text))
                {
                    csiBuilder.AppendFormat("{0}Honoree Address:{0}{1}{0}{2}", Environment.NewLine, txtAddress.Text, txtAddress2.Text);
                }
                if (!string.IsNullOrEmpty(txtCity.Text))
                {
                    csiBuilder.AppendFormat("{0}Honoree City: {1}", Environment.NewLine, txtCity.Text);
                }
                if (!string.IsNullOrWhiteSpace(txtState.Text))
                {
                    csiBuilder.AppendFormat("{0}Honoree State/Province: {1}", Environment.NewLine, txtState.Text);
                }
                if (!string.IsNullOrWhiteSpace(txtZip.Text))
                {
                    csiBuilder.AppendFormat("{0}Honoree Zip Code: {1}", Environment.NewLine, txtZip.Text);
                }

                csiNote.CommemorativeGiftDetails = csiBuilder.ToString();
            }

            return csiNote.ToString();
        }

        protected override void AttachCsiToLineItem(int lineItemId, DonationFundItem fundItem, decimal donationAmount)
        {
            try
            {
                Adage.Tessitura.Cart.Flush();
                var cartItem = Adage.Tessitura.Cart.GetCart().CartLineItems.First(item => item.ItemId == lineItemId);
                var csi = CSIConfig.GetCSIFromEpiServer(fundItem.CsiPage.ID);
                if (csi != null)
                {
                    csi.Notes = BuildCsi(fundItem, donationAmount);
                    csi.AttachTo(cartItem);
                }
            }
            catch (Exception e)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(e, string.Format("Contribution CSI - {0} - {1}", e.Message, e.StackTrace), null);
            }
        }


        protected override void btnAddDonation_Click(object sender, EventArgs e)
        {
            base.btnAddDonation_Click(sender, e);

            // disable immediate access to benefits for now. will be included in phase 1.5  -WZ @ 02/12/2014
            //pnlError.Visible = false;
            //DonationFundItem selectedFund = GetSelectedFund();

            //if (DonationAmount <= 0)
            //{
            //    SetErrorMessage(ContributeConfiguration.InvalidAmountError);
            //    return;
            //}

            //if (selectedFund == null)
            //{
            //    SetErrorMessage(ContributeConfiguration.MissingFundError);
            //    return;
            //}

            //bool successful = false;
            //try
            //{
            //    //try get membership benefits right away, if not decline benefits
            //    if(cbNoBenfits.Checked == false)
            //    {
            //        Custom.CoreFeatureSet.Helper.MembershipHelper.ImmediateAccessToMemberBenefits(PublicContributionType.PartnersProgram, DonationAmount);    
            //    }

            //    var contribution = new ContributionLineItem();
            //    var lineItemId = contribution.Add(DonationAmount, selectedFund.FundId, selectedFund.AccountMethodId);
            //    if (lineItemId <= 0)
            //    {
            //        SetErrorMessage(ContributeConfiguration.ErrorMessage);
            //    }
            //    else
            //    {
            //        AttachCsiToLineItem(lineItemId, selectedFund, DonationAmount);
            //        successful = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("Add Contribution - {0} - {1}", ex.Message, ex.StackTrace), null);
            //    SetErrorMessage(ContributeConfiguration.ErrorMessage);
            //}

            //if (successful)
            //{
            //    Response.Redirect(TheaterSharedConfig.GetValue(TheaterTemplate.Shared.Common.TheaterSharedAppSettings.CART_URL, "/cart/index.aspx"));
            //}
        }
    }
}