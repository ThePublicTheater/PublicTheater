using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using Adage.Tessitura.Constants;
using EPiServer;
using EPiServer.Core;
using LinqToTwitter;
using PublicTheater.Custom.CoreFeatureSet.Common;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using PublicTheater.Custom.CoreFeatureSet.PerformanceObjects;
using PublicTheater.Custom.Episerver.Pages;
using TheaterTemplate.Shared.Common;
using TheaterTemplate.Shared.EpiServerConfig;
using ScriptManager = EPiServer.ClientScript.ScriptManager;
using System.Web.Script.Serialization;

namespace PublicTheater.Web.Controls.Reserve
{
    public class ReserveControl : TheaterTemplate.Web.Controls.ReserveControls.ReserveControl
    {
        protected Panel seatSelectionPanel;
        protected Panel testing;
        protected global::System.Web.UI.WebControls.Panel pnlProductionThumbnail;
        private PlayDetail _currentPlayDetail;
        private string _productionId;
        private Production _currentProduction;
        private VenueConfig _currentVenueConfig;
        protected global::System.Web.UI.WebControls.Label pricePage;
        protected global::System.Web.UI.HtmlControls.HtmlAnchor changePerformance;

        protected override bool IsSyosEnabled
        {
            get
            {
                //return true;
                return this.CurrentPerformance != null && this.CurrentPerformance.SyosEnabled && this.CurrentPlayDetail != null && this.CurrentPlayDetail.GetSYOSRootPage(this.CurrentPerformance) != 0 && this.IsReserveEnabled;
            }
        }
        public override VenueConfig CurrentVenueConfig
        {
            get
            {
                if (this.CurrentPerformance == null)
                {
                    List<Venue> venues = Venues.Get();
                    foreach (var venue in venues)
                    {
                        if (venue.TheatreDescription == CurrentProduction.Venue)
                            this._currentVenueConfig = this.VenueConfigurations.GetVenueConfig(venue.ID);
                    }
                }
                else
                {
                    if (this._currentVenueConfig == null)
                        this._currentVenueConfig =
                            this.VenueConfigurations.GetVenueConfig(this.CurrentPerformance.VenueId);

                }
                return this._currentVenueConfig;
            }
        }
        protected override Performance CurrentPerformance
        {
            get
            {
                return string.IsNullOrEmpty((_productionId)) ? base.CurrentPerformance : null;
            }
        }
        protected virtual Production CurrentProduction
        {
            get
            {
                return this._currentProduction ??
                       (this._currentProduction = Production.GetProduction(Int32.Parse(_productionId)));
            }
        }
        protected override PlayDetail CurrentPlayDetail
        {
            get
            {
                return this._currentPlayDetail ??
                       (this._currentPlayDetail =
                           this.CurrentSeasonPlayDetails.GetPlayDetail(_productionId != null
                               ? int.Parse(_productionId)
                               : this.CurrentPerformance.ProductionSeasonId));
            }
        }
        protected override int PerformanceNumber
        {
            get
            {
                int result;
                if (int.TryParse(this.HfSelectedPerformanceId.Value, out result))
                    return result;
                //   this.Response.Redirect(Config.GetValue<string>("CALENDAR_URL", "~/calendar/index.aspx"), true);
                return -1;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ((PublicMOSHelper)TheaterSharedMOSHelper.Get()).TriggerFlexPackageConflict();
        }

        protected override void BindPerformanceControls()
        {
            //base.BindPerformanceControls();

            this.displayDateFormat.Value = Config.GetValue<string>("PERFORMANCE_DATE_DISPLAY_FORMAT", "dddd, MMMM d");
            pricePage.Text = GetPriceRange(CurrentPerformance);
            string str = string.Empty;

            if (CurrentPerformance != null)
            {
                this.choiceToggle.Visible = this.IsSyosEnabled;
                this.SYOSMainBody.Visible = this.IsSyosEnabled;
                this.HfIsSyosEnabled.Value = this.IsSyosEnabled ? "1" : "0";

            }
            if (this.CurrentVenueConfig != null)
            {
                this.VenueName.Text = this.CurrentVenueConfig.VenueName;
                str = this.CurrentVenueConfig.ReservePageContent;
            }
            if (this.CurrentPlayDetail != null)
            {
                this.ProductionThumbnail.ImageUrl = this.CurrentPlayDetail.ThumbnailUrl ?? "";
                this.ProductionTitle.Text = this.CurrentPlayDetail.Heading;
                if (string.IsNullOrEmpty(this.CurrentPlayDetail.Heading))
                {
                    try
                    {
                        this.ProductionTitle.Text =
                            DataFactory.Instance.GetPage(new PageReference(this.CurrentPlayDetail.PageId)).PageName;
                    }
                    catch (Exception e)
                    {
                    }
                }
                this.ProductionTitle.NavigateUrl = this.CurrentPlayDetail.PlayDetailLink;

                if (this.CurrentPerformance != null)
                {
                    this.SYOSSpecificMainBody.PageLink =
                        new PageReference(this.CurrentPlayDetail.GetSYOSRootPage(this.CurrentPerformance));
                    this.HtmlSyosControl.ConfigPageID = this.CurrentPlayDetail.GetSYOSRootPage(this.CurrentPerformance);
                }
                this.ReservePageContent.Text = string.IsNullOrEmpty(this.CurrentPlayDetail.ReserveMessageOverride) ? str : this.CurrentPlayDetail.ReserveMessageOverride;
            }
            else
            {
                if (CurrentPerformance != null)
                    this.ProductionTitle.Text = CurrentPerformance.Title;
                else
                    this.ProductionTitle.Text = this.CurrentProduction.Title;
                this.ProductionThumbnail.Visible = false;
                this.ReservePageContent.Text = str;
                this.SYOSSpecificMainBody.Visible = false;
            }

            pnlProductionThumbnail.Visible = CurrentPlayDetail != null && !string.IsNullOrEmpty(CurrentPlayDetail.ThumbnailUrl);
        }

        private string GetPriceRange(Performance CurrentPerformance)
        {

            string returnValue = string.Empty;
            PageData oPage;
            EPiServer.Core.PropertyBoolean showDoorPrice = EPiServer.Core.PropertyBoolean.Parse("false");
            EPiServer.Core.PropertyBoolean showMemberPrice = EPiServer.Core.PropertyBoolean.Parse("false");
            EPiServer.Core.PropertyBoolean isParkShow = EPiServer.Core.PropertyBoolean.Parse("false");
            string stbPriceOverride = "", memPriceOverride = "", masterPriceOverride = "";
            if (CurrentPlayDetail != null)
            {
                oPage = EPiServer.DataFactory.Instance.GetPage(new PageReference(CurrentPlayDetail.PageId));
                showDoorPrice = (EPiServer.Core.PropertyBoolean)oPage.Property["ShowDoorPrice"];
                showMemberPrice = (EPiServer.Core.PropertyBoolean)oPage.Property["ShowMemberPrice"];
                isParkShow = (EPiServer.Core.PropertyBoolean)oPage.Property["IsParkShow"];
                stbPriceOverride = (string)((EPiServer.Core.PropertyLongString)oPage.Property["stbPriceOverride"]).Value;
                memPriceOverride = (string)((EPiServer.Core.PropertyLongString)oPage.Property["memPriceOverride"]).Value;
                var masterPriceOverrideXHTML = ((XhtmlString)((EPiServer.Core.PropertyLongString)oPage.Property["masterPriceOverride"]).Value);
                if (masterPriceOverrideXHTML != null)
                    masterPriceOverride = masterPriceOverrideXHTML.ToHtmlString();

            }


            if (isParkShow.Boolean && String.IsNullOrEmpty(masterPriceOverride))
                pricePage.Visible = false;
            else
            {
                var webFullPriceTypeID = Config.GetValue(Custom.CoreFeatureSet.Common.PublicAppSettings.WebFullPriceTypeID, 119);
                var webMemPriceTypeID = Config.GetValue(Custom.CoreFeatureSet.Common.PublicAppSettings.WebFullPriceTypeID, 33);
                var webDoorPriceTypeID = Config.GetValue(Custom.CoreFeatureSet.Common.PublicAppSettings.WebFullPriceTypeID, 29);

                if (CurrentPerformance == null)
                    return string.Empty;
                var performancePricings = ((PublicPerformance)CurrentPerformance).WebStandardPricing.Sections
                   .Where(sec => sec.PriceTypeId == webFullPriceTypeID)
                   .Select(sec => sec.Price)
                   .ToList();
                var memberPricings = ((PublicPerformance)CurrentPerformance).MembershipPricing.Sections
                   .Where(sec => sec.PriceTypeId == webMemPriceTypeID)
                   .Select(sec => sec.Price)
                   .ToList();


                Decimal minPrice, maxPrice, minMemPrice, maxMemPrice, minDoorPrice, maxDoorPrice;

                if (!string.IsNullOrEmpty(masterPriceOverride))
                {
                    returnValue = masterPriceOverride;
                }
                else
                {
                    if (performancePricings.Any() == true)
                    {
                        if (string.IsNullOrEmpty(stbPriceOverride))
                        {
                            minPrice = performancePricings.Min();
                            maxPrice = performancePricings.Max();
                            if (minPrice == maxPrice)
                                returnValue = string.Format("Ticket Price: {0:c}", minPrice);
                            else
                                returnValue = System.String.Format("Ticket Price: {0:c} - {1:c}", minPrice, maxPrice);
                        }
                        else
                        {
                            returnValue = "Ticket Price: " + stbPriceOverride;
                        }
                    }


                    if (memberPricings.Any() == true && showMemberPrice.Boolean)
                    {
                        if (string.IsNullOrEmpty(memPriceOverride))
                        {
                            minMemPrice = memberPricings.Min();
                            maxMemPrice = memberPricings.Max();
                            if (minMemPrice == maxMemPrice)
                                returnValue += string.Format("<br>Member Price: {0:c}", minMemPrice);
                            else
                                returnValue += System.String.Format("<br>Member Price: {0:c} - {1:c}", minMemPrice, maxMemPrice);
                        }
                        else
                        {
                            returnValue += "<br>Member Price: " + memPriceOverride;
                        }
                    }

                    if (showDoorPrice.Boolean)
                    {
                        string doorPriceString = GetDoorPriceRange(CurrentPerformance.PerformanceId);
                        if (!String.IsNullOrEmpty(doorPriceString)) returnValue += string.Format("<br>" + doorPriceString);
                    }
                }
            }

            return returnValue;

        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            //base.Page_Load(sender, e);


            if (!base.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["performanceNumber"]))
                    base.HfSelectedPerformanceId.Value = base.Request.QueryString["performanceNumber"];
                else
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["productionNumber"]))
                    {
                        _productionId = Request.QueryString["productionNumber"];
                        base.PromoBox.Visible = false;
                    }
                    else if (!string.IsNullOrEmpty(Request.QueryString["prodSeas"]))
                    {
                        _productionId = Request.QueryString["prodSeas"];
                        base.PromoBox.Visible = false;
                    }
                    else
                        throw new Exception("Must specify performance or production season using one of the following query string keys: performanceNumber, productionNumber, prodSeas.");
                }
                TheaterSharedMOSHelper.Get().VerifySingleTicketMOS();
            }
            if (!string.IsNullOrEmpty(base.ReserveJavaScriptFile))
            {
                System.Web.UI.ScriptManager.RegisterClientScriptInclude((Control)this, base.GetType(), "ReserveJavaScript", base.ReserveJavaScriptFile);
            }
            base.PromoBox.PromoCodeUpdated += new EventHandler(base.PromoBox_PromoCodeUpdated);

            this.BestAvailableControl.CurrentPerformance = this.CurrentPerformance;
            this.BestAvailableControl.CurrentVenueConfig = this.CurrentVenueConfig;
            this.HandlePerformanceErrors();
            this.HtmlSyosControl.Enabled = this.IsSyosEnabled;

        }

        protected override void HandlePerformanceErrors()
        {
            this.PerformanceNotAvailable.Visible = false;
            this.PerformanceSoldOut.Visible = false;
            string str = string.Empty;
            if (CurrentPerformance == null)
                return;
            else
            {
                if (!this.CurrentPerformance.PerformancePricing.OnSale)
                {
                    this.PerformanceNotAvailable.Visible = true;
                    str = "Performance Not On Sale in this MOS";
                }
                else if (!Enumerable.Any<PriceType>((IEnumerable<PriceType>)this.CurrentPerformance.PerformancePricing.PriceTypes))
                {
                    this.PerformanceNotAvailable.Visible = true;
                    str = "No price types from tessitura";
                }
                else if (!Enumerable.Any<Section>((IEnumerable<Section>)this.CurrentPerformance.PerformancePricing.Sections))
                {
                    this.PerformanceNotAvailable.Visible = true;
                    str = "No sections from tessitura";
                }
                else if (!this.CurrentPerformance.PerformancePricing.SectionsAvailable && Enumerable.Any<PriceType>((IEnumerable<PriceType>)this.CurrentPerformance.PerformancePricing.PriceTypes))
                {
                    this.PerformanceSoldOut.Visible = true;
                    str = "No sections marked as available";
                }
                else if (!this.CurrentPerformance.PerformancePricing.SectionsAvailableSeats)
                {
                    this.PerformanceSoldOut.Visible = true;
                    str = "No sections have available seats";
                }

                else
                {
                    var availSections =
                     this.CurrentPerformance.PerformancePricing.Sections.Where(
                         section => section.IsAvailable && section.AvailableCount > 0);
                    var priceTypes = this.CurrentPerformance.PerformancePricing.PriceTypes.Where(
                        pt =>
                            availSections.Any(section => section.PriceTypeId == pt.PriceTypeId) && pt.PriceTypeId != 29);
                    var purchasableSeatsAvailable =
                        priceTypes.Select(
                            priceType => availSections.FirstOrDefault(sec => sec.PriceTypeId == priceType.PriceTypeId))
                            .Any(section => section != null);
                    if (!purchasableSeatsAvailable)
                    {
                        this.PerformanceNotAvailable.Visible = true;
                        str = "No access to purchasable price types";
                    }
                    else
                    {
                        this.IsReserveEnabled = true;
                    }
                }
                this.BestAvailableControl.Visible = this.IsReserveEnabled;
                this.HfDataIssue.Value = str;
            }

        }
        protected override void Page_PreRender(object sender, EventArgs e)
        {
            if (CurrentPerformance != null)
                base.Page_PreRender(sender, e);
            else
            {
                seatSelectionPanel.Visible = false;
                LoadPerformanceContent();
            }
        }
        protected override void LoadPerformanceContent()
        {
            var performances = this.GetPerformances();
            this.BindPerformanceControls();
            var firstPerformance = performances.FirstOrDefault();
            if (firstPerformance != null)
            {
                base.CreateCalendarInfo((IEnumerable<Performance>)performances, firstPerformance);
            }

            base.PopulatePromoPerfDropDown((IEnumerable<Performance>)performances);
            changePerformance.Visible = performances.Count > 1;
        }

        protected List<Performance> GetPerformancesByProduction(Production TessituraProduction)
        {
            var performancesWithStatus = new Dictionary<Performance, PerformanceHelper.PerformanceAvailable>();
            if (TessituraProduction != null)
            {
                foreach (Performance performance in TessituraProduction.Performances)
                {
                    var status = PerformanceHelper.PerformanceAvailableStatus(performance);
                    performancesWithStatus.Add(performance, status);
                }
            }
            return
                    performancesWithStatus
                    .Where(p => p.Value == PerformanceHelper.PerformanceAvailable.Available)
                    .Select(p => p.Key)
                    .ToList();


        }
        protected override List<Performance> GetPerformances()
        {


            Production production;
            Production linkedProduction = null;
            if (this.CurrentPerformance != null)
            {
                production = Production.GetProduction(this.CurrentPerformance.ProductionSeasonId);
            }
            else
            {
                production = CurrentProduction;
            }
            var linkedSeasonId = production.WebContent["Linked Season ID"];
            if (linkedSeasonId != null)
            {
                linkedProduction = Production.GetProduction(Int32.Parse(linkedSeasonId));
            }
            List<Performance> performances;
            if (linkedProduction != null)
                performances = GetPerformancesByProduction(production).Concat(GetPerformancesByProduction(linkedProduction)).ToList();
            else
                performances = GetPerformancesByProduction(production);

            if (!Config.GetValue<bool>("MINI_CAL_USE_CALENDAR_PERFORMANCES_ONLY", true))
                return performances;
            IEnumerable<int> performanceId = Enumerable.Select(Performances.GetPerformances(CalendarConfig.GetCalendarConfig(Config.GetValue("CalendarConfigId", 0)).GetPerformancesCriteria()), (perf => perf.PerformanceId));
            return Enumerable.ToList<Performance>(Enumerable.Where<Performance>((IEnumerable<Performance>)performances, (Func<Performance, bool>)(perf => Enumerable.Contains<int>(performanceId, perf.PerformanceId))));
        }
        public static string GetDoorPriceRange(int performanceId)
        {


            var doorPriceTypeID = Config.GetValue(Custom.CoreFeatureSet.Common.PublicAppSettings.DoorPriceTypeID, 29);

            var sqlParameters = new Dictionary<string, object>();
            sqlParameters.Add("@perf_no", performanceId);

            DataSet ptSet = TessSession.GetSession().ExecuteLocalProcedure("GetPriceTypesSP", sqlParameters);
            if (ptSet != null && (ptSet.Tables.Count != 0 && ptSet.Tables[0].Rows.Count != 0))
            {
                DataTable table = ptSet.Tables[0];
                DataRow[] rows = table.Select("price_type=" + doorPriceTypeID);
                if (rows.Length == 0)
                    return string.Empty;
                int minPrice = Convert.ToInt32(rows[0]["min_price"]);
                int maxPrice = Convert.ToInt32(rows[0]["max_price"]);
                if (minPrice == maxPrice)
                    return string.Format("Door Price: {0:c}", minPrice);
                else
                {
                    return System.String.Format("Door Price: {0:c} - {1:c}", minPrice, maxPrice);
                }
            }
            return string.Empty;
        }
    }
}