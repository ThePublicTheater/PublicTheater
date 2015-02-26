using Adage.EpiServer.Library.PageTypes.Templates;
using Adage.Tessitura;
using EPiServer;
using EPiServer.Core;
using PublicTheater.Custom.CoreFeatureSet.Memberships.MyBenefits;
using PublicTheater.Custom.CoreFeatureSet.UserObjects;
using PublicTheater.Custom.Episerver.BaseClasses;
using PublicTheater.Custom.Episerver.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheaterTemplate.Shared.Common;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Web.Views.Pages
{
    public partial class Benefits : PublicBasePage<BenefitsPageData>
    {
        /// <summary>
        /// Current User in the session
        /// </summary>
        protected PublicUser CurrentUser { get { return Adage.Tessitura.User.GetUser() as PublicUser; } }

        private MyBenefitSummary _currentBenefitSummary;
        /// <summary>
        /// current user's benefits summary
        /// </summary>
        protected MyBenefitSummary CurrentBenefitSummary
        {
            get
            {
                return _currentBenefitSummary ?? (_currentBenefitSummary = CurrentUser.MyBenefitSummary);
            }
        }

        private PlayDetailsCollection _playDetailsCollection;
        /// <summary>
        /// PDPs defined in CMS
        /// </summary>
        protected PlayDetailsCollection PlayDetailsCollection
        {
            get
            {
                return _playDetailsCollection ?? (_playDetailsCollection = PlayDetailsCollection.GetPlayDetails());
            }
        }

        private Performances _performanceCollection;
        /// <summary>
        /// performances loaded from tessitura
        /// </summary>
        protected Performances PerformanceCollection
        {
            get
            {
                return _performanceCollection ?? (_performanceCollection = Performances.GetPerformances(new PerformancesCriteria
                                                        {
                                                            StartDate = DateTime.Today,
                                                            EndDate = DateTime.Today.AddYears(1),
                                                            ModeOfSale = Adage.Tessitura.TessSession.GetSession().ModeOfSale
                                                        }));
            }
        }

        /// <summary>
        /// Page load method
        /// </summary>
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (!CurrentUser.LoggedIn || CurrentUser.IsAnonymousLogin)
            {
                CurrentUser.RequirePermanentLogin(Request.Url.PathAndQuery);
            }

            if (!IsPostBack)
            {
                LoadUserBenefits();
            }

        }

        /// <summary>
        /// Populate benefits details
        /// </summary>
        protected virtual void LoadUserBenefits()
        {
            var myBenefitSummary = CurrentBenefitSummary;

            BindGeneralMembershipInfo(myBenefitSummary.MembershipStatus);

            BindDowntownOverview(myBenefitSummary.DTBenefits);
            BindPendingDowntownOverview(myBenefitSummary.PendingDTBenefits);

            BindDowntownDetails(myBenefitSummary.DTBenefitPriceTypes, myBenefitSummary.DTBenefitProductions);
            BindPendingDowntownDetails(myBenefitSummary.PendingDTBenefitPriceTypes, myBenefitSummary.PendingDTBenefitProductions);

            BindSITPDetails(myBenefitSummary.SIPTBenefits);
            BindPendingSITPDetails(myBenefitSummary.PendingSIPTBenefits);

            BindAvailableProductions(myBenefitSummary.AvailableProductions);
        }

        /// <summary>
        /// Bind available prods can redeem benefits from
        /// </summary>
        /// <param name="availableProductions"></param>
        protected virtual void BindAvailableProductions(AvailableProductions availableProductions)
        {
            if (availableProductions.Any())
            {
                rptAvailableProductions.DataSource = availableProductions;
                rptAvailableProductions.ItemDataBound += rptAvailableProductions_ItemDataBound;
                rptAvailableProductions.DataBind();
            }
            else
            {
                pnlAvailableProductions.Visible = false;

                propNoUpcomingPerformancesMessage.Visible = true;

            }

            if (CurrentBenefitSummary.ShowBenefitsMessage == false)
            {
                propNoUpcomingPerformancesMessage.Visible = pnlAvailableProductions.Visible = false;
            }
        }

        protected virtual void rptAvailableProductions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem == null)
                return;

            var prodSeasonNo = (int)e.Item.DataItem;
            var lnkPDP = (HyperLink)e.Item.FindControl("lnkPDP");
            PopulatePlayDetailLink(prodSeasonNo, lnkPDP);
        }

        /// <summary>
        /// Bind Shakespeare in the park benefits
        /// </summary>
        /// <param name="sITPBenefits"></param>
        protected virtual void BindSITPDetails(BenefitUsages sITPBenefits)
        {
            if (sITPBenefits.Any())
            {
                rptSITPBenefits.DataSource = sITPBenefits;
                rptSITPBenefits.ItemDataBound += new RepeaterItemEventHandler(BenefitUsageItemDataBound);
                rptSITPBenefits.DataBind();
            }
            else
            {
                pnlSITPBenefits.Visible = false;
            }
        }
        /// <summary>
        /// Bind Shakespeare in the park benefits
        /// </summary>
        /// <param name="sITPBenefits"></param>
        protected virtual void BindPendingSITPDetails(BenefitUsages sITPBenefits)
        {
            if (sITPBenefits.Any())
            {
                rptPendingSITPBenefits.DataSource = sITPBenefits;
                rptPendingSITPBenefits.ItemDataBound += new RepeaterItemEventHandler(BenefitUsageItemDataBound);
                rptPendingSITPBenefits.DataBind();
            }
            else
            {
                rptPendingSITPBenefits.Visible = false;
            }
        }

        protected virtual void BenefitUsageItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var benefitUsage = e.Item.DataItem as BenefitUsage;
            if (benefitUsage == null)
                return;

            var litBenefits = (Literal)e.Item.FindControl("litBenefits");
            litBenefits.Text = benefitUsage.Benefit;

            var litTotal = (Literal)e.Item.FindControl("litTotal");
            litTotal.Text = benefitUsage.Total.ToString();

            var litUsed = (Literal)e.Item.FindControl("litUsed");
            litUsed.Text = benefitUsage.Used.ToString();

            var litRemaining = (Literal)e.Item.FindControl("litRemaining");
            litRemaining.Text = benefitUsage.Remaining.ToString();
        }

        /// <summary>
        /// Bind downtown per production benefit details
        /// </summary>
        /// <param name="dTBenefitPriceTypes"></param>
        /// <param name="dTBenefitProductions"></param>
        protected virtual void BindDowntownDetails(DTBenefitPriceTypes dTBenefitPriceTypes, DTBenefitProductions dTBenefitProductions)
        {
            rptDTBenefitPriceTypes.DataSource = dTBenefitPriceTypes;
            rptDTBenefitPriceTypes.ItemDataBound += rptDTBenefitPriceTypes_ItemDataBound;
            rptDTBenefitPriceTypes.DataBind();

            if (dTBenefitProductions.Any())
            {
                rptDTBenefitProductions.DataSource = dTBenefitProductions.Select(b => b.ProductionSeasonId).Distinct();
                rptDTBenefitProductions.ItemDataBound += rptDTBenefitProductions_ItemDataBound;
                rptDTBenefitProductions.DataBind();
            }
            else
            {
                rptDTBenefitProductions.Visible = false;
                if (CurrentBenefitSummary.ShowBenefitsMessage)
                {
                    propNoDTUsage.Visible = true;
                }
                else
                {
                    pnlDTBenefitProductions.Visible = false;
                }
            }
        }

        /// <summary>
        /// Bind downtown per production benefit details
        /// </summary>
        /// <param name="dTBenefitPriceTypes"></param>
        /// <param name="dTBenefitProductions"></param>
        protected virtual void BindPendingDowntownDetails(DTBenefitPriceTypes dTBenefitPriceTypes, DTBenefitProductions dTBenefitProductions)
        {
            rptPendingDTBenefitPriceTypes.DataSource = dTBenefitPriceTypes;
            rptPendingDTBenefitPriceTypes.ItemDataBound += rptDTBenefitPriceTypes_ItemDataBound;
            rptPendingDTBenefitPriceTypes.DataBind();

            if (dTBenefitProductions.Any())
            {
                rptPendingDTBenefitProductions.DataSource = dTBenefitProductions.Select(b => b.ProductionSeasonId).Distinct();
                rptPendingDTBenefitProductions.ItemDataBound += rptDTBenefitProductions_ItemDataBound;
                rptPendingDTBenefitProductions.DataBind();
            }
            else
            {
                rptPendingDTBenefitProductions.Visible = false;
                if (CurrentBenefitSummary.ShowBenefitsMessage)
                {
                    propNoPendingDTUsage.Visible = true;
                }
                else
                {
                    pnlPendingDTBenefitProductions.Visible = false;
                }
            }
        }

        protected int _currentProdSeasonId;
        protected virtual void rptDTBenefitProductions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem == null)
                return;

            var prodSeasonId = (int)e.Item.DataItem;

            _currentProdSeasonId = prodSeasonId;

            var lnkProduction = (HyperLink)e.Item.FindControl("lnkProduction");
            PopulatePlayDetailLink(prodSeasonId, lnkProduction);

            var rptDTBenefitProductionPriceTypes = (Repeater)e.Item.FindControl("rptDTBenefitProductionPriceTypes");
            rptDTBenefitProductionPriceTypes.DataSource = CurrentBenefitSummary.DTBenefitPriceTypes;
            rptDTBenefitProductionPriceTypes.ItemDataBound += rptDTBenefitProductionPriceTypes_ItemDataBound;
            rptDTBenefitProductionPriceTypes.DataBind();
        }

        protected virtual void rptPendingDTBenefitProductions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem == null)
                return;

            var prodSeasonId = (int)e.Item.DataItem;

            _currentProdSeasonId = prodSeasonId;

            var lnkProduction = (HyperLink)e.Item.FindControl("lnkProduction");
            PopulatePlayDetailLink(prodSeasonId, lnkProduction);

            var rptDTBenefitProductionPriceTypes = (Repeater)e.Item.FindControl("rptDTBenefitProductionPriceTypes");
            rptDTBenefitProductionPriceTypes.DataSource = CurrentBenefitSummary.PendingDTBenefitPriceTypes;
            rptDTBenefitProductionPriceTypes.ItemDataBound += rptPendingDTBenefitProductionPriceTypes_ItemDataBound;
            rptDTBenefitProductionPriceTypes.DataBind();
        }

        void rptPendingDTBenefitProductionPriceTypes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var benefitPriceType = e.Item.DataItem as DTBenefitPriceType;
            if (benefitPriceType == null)
                return;

            var benefitProduction = CurrentBenefitSummary.PendingDTBenefitProductions.FirstOrDefault(
                    p => p.ProductionSeasonId == _currentProdSeasonId
                        && p.PriceTypeId == benefitPriceType.PriceTypeId);

            var litUsed = (Literal)e.Item.FindControl("litUsed");
            litUsed.Text = benefitProduction == null || benefitProduction.Used < 0 ? "-" : benefitProduction.Used.ToString();

            var litRemaining = (Literal)e.Item.FindControl("litRemaining");
            litRemaining.Text = benefitProduction == null || benefitProduction.Remaining < 0 ? "-" : benefitProduction.Remaining.ToString();
        }
        void rptDTBenefitProductionPriceTypes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var benefitPriceType = e.Item.DataItem as DTBenefitPriceType;
            if (benefitPriceType == null)
                return;

            var benefitProduction = CurrentBenefitSummary.DTBenefitProductions.FirstOrDefault(
                    p => p.ProductionSeasonId == _currentProdSeasonId
                        && p.PriceTypeId == benefitPriceType.PriceTypeId);

            var litUsed = (Literal)e.Item.FindControl("litUsed");
            litUsed.Text = benefitProduction == null || benefitProduction.Used < 0 ? "-" : benefitProduction.Used.ToString();

            var litRemaining = (Literal)e.Item.FindControl("litRemaining");
            litRemaining.Text = benefitProduction == null || benefitProduction.Remaining < 0 ? "-" : benefitProduction.Remaining.ToString();
        }

        /// <summary>
        /// populate a pdp link with production season id
        /// </summary>
        /// <param name="prodSeasonId"></param>
        /// <param name="lnkProduction"></param>
        private void PopulatePlayDetailLink(int prodSeasonId, HyperLink lnkProduction)
        {
            if (prodSeasonId <= 0)
            {
                lnkProduction.Visible = false;
                return;
            }

            var playDetail = PlayDetailsCollection.GetPlayDetail(prodSeasonId);
            if (playDetail != null)
            {
                lnkProduction.Text = playDetail.Heading;
                lnkProduction.NavigateUrl = playDetail.PlayDetailLink;
            }
            else
            {
                var perf = PerformanceCollection.FirstOrDefault(p => p.ProductionSeasonId == prodSeasonId);
                if (perf != null)
                {
                    lnkProduction.Text = perf.Title;
                    lnkProduction.NavigateUrl = string.Format(Config.GetValue(TheaterSharedAppSettings.RESERVE_PAGE_URL, "/reserve/index.aspx?performanceNumber={0}"), perf.PerformanceId);
                }
                else
                {
                    var production = Production.GetProduction(prodSeasonId);
                    lnkProduction.Text = production.Title;
                    lnkProduction.Enabled = false;
                }
            }
        }

        protected virtual void rptDTBenefitPriceTypes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var benefitPriceType = e.Item.DataItem as DTBenefitPriceType;
            if (benefitPriceType == null)
                return;

            var litPriceTypeName = (Literal)e.Item.FindControl("litPriceTypeName");
            litPriceTypeName.Text = benefitPriceType.PriceTypeName;

            var lbPriceTypeDesc = (Label)e.Item.FindControl("lbPriceTypeDesc");
            lbPriceTypeDesc.Text = benefitPriceType.Description;
        }
        //protected virtual void rptPendingDTBenefitPriceTypes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    var benefitPriceType = e.Item.DataItem as DTBenefitPriceType;
        //    if (benefitPriceType == null)
        //        return;

        //    var litPriceTypeName = (Literal)e.Item.FindControl("litPriceTypeName");
        //    litPriceTypeName.Text = benefitPriceType.PriceTypeName;

        //    var lbPriceTypeDesc = (Label)e.Item.FindControl("lbPriceTypeDesc");
        //    lbPriceTypeDesc.Text = benefitPriceType.Description;
        //}

        /// <summary>
        /// Bind Downtown total complimentary info
        /// </summary>
        /// <param name="dTBenefits"></param>
        protected virtual void BindDowntownOverview(BenefitUsages dTBenefits)
        {
            rptDTBenefits.DataSource = dTBenefits;
            rptDTBenefits.ItemDataBound += BenefitUsageItemDataBound;
            rptDTBenefits.DataBind();

            pnlDTOverview.Visible = dTBenefits.Any();
            if (pnlDTOverview.Visible)
            {
                litDTBenefitProductionsTitle.Text = string.Empty;
            }
        }
        /// <summary>
        /// Bind Downtown total complimentary info
        /// </summary>
        /// <param name="dTBenefits"></param>
        protected virtual void BindPendingDowntownOverview(BenefitUsages dTBenefits)
        {
            rptPendingDTBenefits.DataSource = dTBenefits;
            rptPendingDTBenefits.ItemDataBound += BenefitUsageItemDataBound;
            rptPendingDTBenefits.DataBind();

            pnlPendingDTOverview.Visible = dTBenefits.Any();
            if (pnlPendingDTOverview.Visible)
            {
                litPendingDTBenefitProductionsTitle.Text = string.Empty;
            }
        }
        /// <summary>
        /// Bind general member info and membership status
        /// </summary>
        /// <param name="membershipStatus"></param>
        protected void BindGeneralMembershipInfo(MembershipStatus membershipStatus)
        {
            CurrentUser.GetAccountInfo();
            litCustomerName.Text = CurrentUser.FullName;
            litCustomerNo.Text = CurrentUser.CustomerNumber.ToString();
            if (string.IsNullOrEmpty(membershipStatus.ContentMessageId))
            {
                propMessage.Visible = false;
            }
            else
            {
                propMessage.Visible = true;
                propMessage.PropertyName = membershipStatus.ContentMessageId;
            }

            if (CurrentBenefitSummary.MembershipStatus.StatusBar == false)
            {
                //not a member, no status to show
                pnlMemberStatus.Visible = false;
            }
            else
            {
                //populate details
                litMemberLevel.Text = membershipStatus.ProgramLevel;
                if (membershipStatus.ExpDate != DateTime.MinValue)
                {
                    litExpDate.Text = string.Format("Exp: {0}", membershipStatus.ExpDate.ToString("MM/dd/yy"));
                }
                if (string.IsNullOrEmpty(membershipStatus.Status) == false)
                {
                    litMemberStatus.Text = string.Format("Status: {0}", membershipStatus.Status);
                }

                lnkRenewBtnBenefits.Visible = membershipStatus.AllowRenew;
                if (membershipStatus.AllowRenew)
                {
                    //set renewal link based on membership program
                    var renewalLinks = CurrentPage.GetProgramRenewalLinks();
                    if (renewalLinks.ContainsKey(membershipStatus.ProgramName))
                    {
                        lnkRenewBtnBenefits.NavigateUrl = renewalLinks[membershipStatus.ProgramName];
                    }
                }

                //already renewed? display renewal details
                if (string.IsNullOrEmpty(membershipStatus.RenewalLevel) == false)
                {
                    //use default formatter if not customized in CMS
                    var renewedMessage = CurrentPage.RenewedMessageFormat == null
                        ? "Thank you for renewing at the {0} level. Your new benefits will begin on {1:MM/dd/yy}."
                        : CurrentPage.RenewedMessageFormat.ToHtmlString();
                    litRenewed.Text = string.Format(renewedMessage, membershipStatus.RenewalLevel, membershipStatus.RenewalStartDate);
                    litRenewed.Visible = true;
                    const string pendingMessage = "Benefits for Membership beginning {0:MM/dd/yy}";
                    PendingHeader.Text = string.Format(pendingMessage,membershipStatus.RenewalStartDate);

                }
                else
                {
                    pendingPanel.Visible = false;
                }
            }
        }
    }
}