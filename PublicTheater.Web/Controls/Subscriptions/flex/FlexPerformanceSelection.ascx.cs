using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using Adage.Tessitura.PerformanceObjects;
using EPiServer;
using EPiServer.Core;
using EPiServer.Filters;
using Microsoft.Ajax.Utilities;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using TheaterTemplate.Shared.Common;
using TheaterTemplate.Shared.EpiServerConfig;
using TheaterTemplate.Shared.EpiServerProperties;
using TheaterTemplate.Shared.PerformanceObjects;

namespace PublicTheater.Web.Controls.Subscriptions.flex
{
    public partial class FlexPerformanceSelection : TheaterTemplate.Web.Controls.Subscriptions.FlexControls.FlexPerformanceSelection
    {

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            maximumPerformances.Value = CurrentFlexPackage.MaxPerformances.ToString();
            var config = CurrentPackageConfig as PublicPackageDetailConfig;
            if (config != null)
            {
                flexHeader.InnerHtml = config.PerformanceHeading;
            }
            if (!IsPostBack)
            {

            }
        }

        protected override void BindClientProductionLevelData(int productionSeasonNumber, RepeaterItem productionRow)
        {
            base.BindClientProductionLevelData(productionSeasonNumber, productionRow);

        }
        private VenueConfigCollection _venueConfigurations;
        /// <summary>
        /// Returns the Venue Collections setup in EpiServer
        /// </summary>
        protected virtual VenueConfigCollection VenueConfigurations
        {
            get
            {
                if (_venueConfigurations == null)
                    _venueConfigurations = VenueConfigCollection.GetVenues(Config.GetValue(TheaterSharedAppSettings.VENUE_CONFIGURATIONS, 0));

                return _venueConfigurations;
            }
        }
        protected override void BindProductionCollections(Performance performance, RepeaterItem productionRow)
        {



            base.BindProductionCollections(performance, productionRow);
            List<Performance> performances = FlexPackageHelper.GetPerformances(CurrentFlexPackage, performance.ProductionSeasonId);
            var config = CurrentPackageConfig as PublicPackageDetailConfig;

            if (config != null &&
                performances.Any(t => t.PerformanceId == config.AutoSelectPerf))
            {
                autoSelectPerf.Value = Convert.ToString(config.AutoSelectPerf);

            }


            var detail = (PublicPlayDetail)CurrentSeasonPlayDetails.GetPlayDetail(performance.ProductionSeasonId);
            var ltrDateRange = (Literal)productionRow.FindControl("ltrDateRange");
            var ltrVenueName = (Literal)productionRow.FindControl("ltrVenueName");
            var ltrProductionData = (Literal)productionRow.FindControl("ltrProductionData");
            var ltrProductionName = (Literal)productionRow.FindControl("ltrProductionName");
            var ltrHiddenCopy = (Literal)productionRow.FindControl("ltrHiddenCopy");
            var productionSeasonNumber = performance.ProductionSeasonId;
            var currentProduction = Production.GetProduction(productionSeasonNumber);

            var hdnProductionSeasonNumber = (HtmlInputHidden)productionRow.FindControl("hdnProductionSeasonNumber");
            hdnProductionSeasonNumber.Value = productionSeasonNumber.ToString();

            if (detail != null)
            {
                ltrProductionData.Text = !string.IsNullOrEmpty(detail.ExtraShortDescription)
                    ? detail.ExtraShortDescription
                    : detail.CalendarSynopsis;

                ltrDateRange.Text = detail.DateRange;
                
                ltrHiddenCopy.Text = detail.AdditionalHiddenPackageText;
                if (!string.IsNullOrEmpty(detail.Heading))
                {
                    ltrProductionName.Text = detail.Heading;
                }
                else
                {
                    var pdpPageCriteria = new PropertyCriteria()
                    {
                        Name = "PageTypeID",
                        Condition = CompareCondition.Equal,
                        Required = true,
                        Type = PropertyDataType.PageType,
                        Value = Config.GetValue<string>("PDP_PAGE_TYPE", string.Empty)
                    };

                    var cc = new PropertyCriteriaCollection();
                    cc.Add(pdpPageCriteria);
                    cc.Add(new PropertyCriteria()
                    {
                        Name = "TessituraId",
                        Condition = CompareCondition.Equal,
                        Required = true,
                        Type = PropertyDataType.Number,
                        Value = productionSeasonNumber.ToString()
                    });
                    var pdpPage = DataFactory.Instance.FindAllPagesWithCriteria(ContentReference.StartPage, cc, "en", new LanguageSelector("en")).FirstOrDefault();

                    if (pdpPage != null && !string.IsNullOrEmpty(pdpPage.PageName))
                    {
                        ltrProductionName.Text = pdpPage.PageName;
                    }
                }

            }
            else if (currentPerformanceList != null && currentPerformanceList.Any())
            {
                var startDate = currentPerformanceList.Min(p => p.PerformanceDate);
                var endDate = currentPerformanceList.Max(p => p.PerformanceDate);
                ltrDateRange.Text = Custom.Episerver.Utility.Utility.GetDateDescription(startDate, endDate);
            }

            var currentVenue = VenueConfigurations.GetVenueConfig(performance.VenueId);
            if (currentVenue != null)
            {
                ltrVenueName.Text = currentVenue.VenueName; 
            }
            else
            {
                ltrVenueName.Text = performance.Venue;
            }
            if(currentProduction.FirstPerformance.Date==currentProduction.LastPerformance.Date)
                ltrDateRange.Text = string.Format("{0:MMMM d yyyy}", currentProduction.FirstPerformance);
            else
            {
                ltrDateRange.Text = string.Format("{0:MMMM d yyyy} - {1:MMMM d yyyy}", currentProduction.FirstPerformance, currentProduction.LastPerformance);
            }
           

        }


        protected override void BindProductionDisplayElements(Performance performance, RepeaterItem productionRow)
        {
            base.BindProductionDisplayElements(performance, productionRow);


        }
        protected override List<Performance> GetUniqueProductions()
        {
            List<Tuple<int, Performance>> sortList = new List<Tuple<int, Performance>>();
            List<Performance> uniqueProductions = new List<Performance>();
            List<int> uniqueProductionIDs = new List<int>();

            foreach (FlexPerformanceGroup performanceGroup in CurrentFlexPackage.PerformanceGroups)
            {
                List<int> currentProductionIds = performanceGroup.Performances.Select(eachperf => eachperf.ProductionSeasonId).Distinct().ToList();
                uniqueProductionIDs.AddRange(currentProductionIds);
            }

            uniqueProductionIDs = uniqueProductionIDs.Distinct().ToList();

            foreach (int productionID in uniqueProductionIDs)
            {
                foreach (FlexPerformanceGroup performanceGroup in CurrentFlexPackage.PerformanceGroups)
                {
                    Performance uniquePerf = performanceGroup.Performances.FirstOrDefault(performance => performance.ProductionSeasonId == productionID &&
                                                                                                         performance.PerformanceDate >= DateTime.Now);
                    if (uniquePerf != null && uniqueProductions.Exists(perf => perf.ProductionSeasonId == uniquePerf.ProductionSeasonId) == false)
                    {
                        //uniqueProductions.Add(uniquePerf);
                        int outcome;
                        var webcontent = uniquePerf.WebContent;
                        if(int.TryParse(webcontent["Package Listing"], out outcome))
                            sortList.Add(new Tuple<int, Performance>(outcome,uniquePerf));
                        else
                        {
                            //try again to get web content 
                            Performance p = Adage.Tessitura.Performance.GetPerformance(uniquePerf.PerformanceId);
                            if (int.TryParse(p.WebContent["Package Listing"], out outcome))
                                sortList.Add(new Tuple<int, Performance>(outcome, uniquePerf));
                            else
                                sortList.Add(new Tuple<int, Performance>(100, uniquePerf));

                        }
                        break;
                    }
                }
            }
            sortList.Sort((a, b) => a.Item1.CompareTo(b.Item1));
            foreach(var a in sortList)
            {
                uniqueProductions.Add(a.Item2);
            }
            return uniqueProductions;
        }
    }
}