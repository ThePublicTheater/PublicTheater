using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using PublicTheater.Custom.Episerver;
using PublicTheater.Custom.Episerver.Blocks;
using PublicTheater.Custom.Episerver.BaseClasses;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using PublicTheater.Custom.Episerver.Utility;
using TheaterTemplate.Shared.Common;
using TheaterTemplate.Shared.EpiServerConfig;
using EPiServer;
using EPiServer.Core;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class UpcomingPerformancesBlockControl : PublicBaseResponsiveBlockControl<UpcomingPerformancesBlockData>
    {

        public Enums.SiteTheme CurrentSiteTheme
        {
            get
            {
                var publicPage = CurrentPage as PublicBasePageData;
                if (publicPage != null)
                    return publicPage.SiteTheme;

                return Enums.SiteTheme.Default;
            }
        }

        private PlayDetailsCollection _playDetails;
        public PlayDetailsCollection PlayDetailCollection
        {
            get
            {
                if (_playDetails == null)
                    _playDetails = PlayDetailsCollection.GetPlayDetails();
                return _playDetails;
            }
        }

        private Dictionary<DateTime, List<Performance>> _upcomingPerformances = null;
        public virtual Dictionary<DateTime, List<Performance>> UpcomingPerformances
        {
            get
            {
                if (_upcomingPerformances == null)
                {
                    _upcomingPerformances = GetUpcomingPerformances();
                }
                return _upcomingPerformances;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            rptDayList.ItemDataBound += rptDayList_ItemDataBound;

            if (!IsPostBack)
            {
                BindPerformances();
                ltrHeader.Text = CurrentBlock.Header;
            }
        }

        private void BindPerformances()
        {
            litNoPerformance.Visible = !UpcomingPerformances.Any();
            rptDayList.DataSource = UpcomingPerformances;
            rptDayList.DataBind();
        }

        private Dictionary<DateTime, List<Performance>> GetUpcomingPerformances()
        {
            int numberOfDaysToShow = Config.GetValue(Custom.CoreFeatureSet.Common.PublicAppSettings.PERFORMANCE_LIST_DAYS_TO_SHOW, 7);

            int numberOfDaysToShowCMSOverride = CurrentBlock.NumberOfDaysToShow;

            if (numberOfDaysToShowCMSOverride > 0)
                numberOfDaysToShow = numberOfDaysToShowCMSOverride;

            var venuePerformances = PerformanceHelper.GetPerformancesBySiteThemeKeywords(CurrentSiteTheme, numberOfDaysToShow).ToList();
            
            //remove the ones with PDP set Hide From Calendar
            RemoveHideFromCalendarPerformances(venuePerformances);

            //performance date is the key, list of performances for that day is the value.
            var performancesGroupedByDate = venuePerformances.GroupBy(perf => perf.PerformanceDate.Date).OrderBy(item => item.Key);

            var performancesGroupedByDateDictionary = performancesGroupedByDate.ToDictionary(item => item.Key, item => item.ToList());

            return performancesGroupedByDateDictionary;
        }

        public void RemoveHideFromCalendarPerformances(List<Performance> performances)
        {
            var perfToRemove = new List<Performance>();
            foreach (Performance performance in performances)
            {
                var playDetail = PlayDetailCollection.PlayDetails.FirstOrDefault(pdp => pdp.TessituraId == performance.ProductionSeasonId);
                if (playDetail != null && playDetail.HideFromCalendar)
                {
                    perfToRemove.Add(performance);
                }
            }

            performances.RemoveAll(perfToRemove.Contains);
        }
        


        void rptDayList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem == null)
                return;

            var kvp = (KeyValuePair<DateTime, List<Performance>>)e.Item.DataItem;

            var performanceDate = kvp.Key;
            var performances = kvp.Value;

            var dayHeader = e.Item.FindControl("ltrDayName") as Literal;
            var rptPerformances = e.Item.FindControl("rptPerformanceList") as Repeater;

            string dayText = GetDayText(performanceDate);
            dayHeader.Text = dayText;

            rptPerformances.ItemDataBound += rptPerformances_ItemDataBound;
            rptPerformances.DataSource = performances;
            rptPerformances.DataBind();
        }

        void rptPerformances_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem == null)
                return;

            var performance = e.Item.DataItem as Performance;

            var ltrPerformanceTitle = e.Item.FindControl("ltrPerformanceTitle") as Literal;
            var ltrPerformanceTime = e.Item.FindControl("ltrPerformanceTime") as Literal;
            var lnkBuyTickets = e.Item.FindControl("lnkBuyTickets") as HyperLink;
            var lnkPDP = e.Item.FindControl("lnkPDP") as HyperLink;

         
            ltrPerformanceTitle.Text = performance.Title;
            ltrPerformanceTime.Text = performance.PerformanceDate.ToString("h:mm tt");
            if (performance.PerformanceDate.ToString("h:mm tt") == "11:59 PM")
            {
                ltrPerformanceTime.Text = "Midnight";
            }
            var playDetail = PlayDetailCollection.PlayDetails.FirstOrDefault(pdp => pdp.TessituraId == performance.ProductionSeasonId);
            
            var performanceImage = e.Item.FindControl("performanceImage") as Image;
            if (playDetail == null)
                return;

            //override with name on PDP
            string showName = OverrideShowName(playDetail);
            if (showName.Length > 0)
                ltrPerformanceTitle.Text = showName;
           

            performanceImage.ImageUrl = playDetail.ThumbnailUrl;

            var pdpPageRef = new PageReference(playDetail.PageId);
            var pdpPage = DataFactory.Instance.GetPage(pdpPageRef);
            lnkPDP.NavigateUrl = playDetail.PlayDetailLink;
           
            
            //hyperlink 
            if (pdpPage != null && playDetail.PlayDetailLink == pdpPage.GetSiteThemeUrl())
            {
                string ticketsUrl = string.Format(Config.GetValue(TheaterSharedAppSettings.RESERVE_PAGE_URL, "/reserve/index.aspx?performanceNumber={0}"), performance.PerformanceId);
                if( !IsSummerShow(playDetail))
                    lnkBuyTickets.NavigateUrl = ticketsUrl;
                else
                {
                    lnkBuyTickets.NavigateUrl = playDetail.PlayDetailLink;
                    lnkBuyTickets.Text = "Details";
                }

            }
            else
            {
                lnkBuyTickets.Visible = false;
            }
        }

        private string GetDayText(DateTime date)
        {
            var today = DateTime.Now;
            var tomorrow = today.AddDays(1);

            if (date == today)
                return "Today";
            if (date == tomorrow)
                return "Tomorrow";

            return date.ToString("dddd MMM d");
        }

        private bool IsSummerShow(PlayDetail playDetail)
        {
            PageData oPage;
            EPiServer.Core.PropertyBoolean isParkShow = EPiServer.Core.PropertyBoolean.Parse("false");
            oPage = EPiServer.DataFactory.Instance.GetPage(new PageReference(playDetail.PageId));
            isParkShow = (EPiServer.Core.PropertyBoolean)oPage.Property["IsParkShow"];
            return isParkShow.Boolean;
        }

        private string OverrideShowName(PlayDetail playDetail)
        {
            PageData oPage;
            EPiServer.Core.PropertyLongString showNameOverride = EPiServer.Core.PropertyLongString.Parse("");
            oPage = EPiServer.DataFactory.Instance.GetPage(new PageReference(playDetail.PageId));
            showNameOverride = (EPiServer.Core.PropertyLongString)oPage.Property["upcomingPeromanceName"];
            return showNameOverride.ToString();
        }
    }
}