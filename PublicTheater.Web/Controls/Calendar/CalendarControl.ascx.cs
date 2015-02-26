using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using Newtonsoft.Json;
using PublicTheater.Custom.CoreFeatureSet.Common;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using PublicTheater.Custom.Episerver;
using TheaterTemplate.Shared.Common;
using PublicTheater.Custom.Episerver.Utility;
using TheaterTemplate.Shared.EpiServerConfig;
using TheaterTemplate.Shared.PerformanceObjects;
using ScriptManager = System.Web.UI.ScriptManager;

namespace PublicTheater.Web.Controls.Calendar
{
    public partial class CalendarControl : TheaterTemplate.Web.Controls.CalendarControls.CalendarControl
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ((PublicMOSHelper)TheaterSharedMOSHelper.Get()).TriggerFlexPackageConflict();
        }

        public override void BindCalendar()
        {
            //clear current month avail venues
            _currentMonthThemes.Clear();

            base.BindCalendar();

            //rebind the venue filters
            BindVenueFilter();

            BindThemePerformanceIds();
            
            AddMonthToUrl();
        }

        protected virtual void AddMonthToUrl()
        {
            if (IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AddMonthToURL",
                    "addMonthToUrl('" + CurrentMonth.Value.Date.ToString("MM/yyyy") + "');", true);
            }
        }
        protected virtual void BindThemePerformanceIds()
        {
            var startDate = CurrentMonth.Value.AddMonths(-1).Date;
            var endDate = CurrentMonth.Value.AddMonths(2).Date;

            var currentMonthPerformances = new List<KeyValuePair<string, int>>();
            foreach (Enums.SiteTheme siteTheme in _currentMonthThemes.Distinct())
            {
                var perfs = PerformanceHelper.GetPerformancesBySiteThemeKeywords(siteTheme, startDate, endDate).ToList();
                currentMonthPerformances.AddRange(
                    perfs.Select(p => new KeyValuePair<string, int>(siteTheme.ToString(), p.PerformanceId)));
            }
            ThemePerformanceIds.Value = new JavaScriptSerializer().Serialize(currentMonthPerformances);
        }

        List<Enums.SiteTheme> _currentMonthThemes = new List<Enums.SiteTheme>();
        private void BindVenueFilter()
        {
            if (!IsPostBack)
            {
                //initial page load, set to requested theme
                var requestedTheme = Utility.GetRequestedTheme();
                if (_currentMonthThemes.Contains(requestedTheme) && requestedTheme!=Enums.SiteTheme.Default)
                {
                    SelectedTheme.Value = requestedTheme.ToString();
                }
            }
            else
            {
                //nav between months, check the selected theme exists in current month or not
                Enums.SiteTheme selectedEnum;
                if (Enum.TryParse(SelectedTheme.Value, true, out selectedEnum) && _currentMonthThemes.Contains(selectedEnum) == false)
                {
                    var requestedTheme = Utility.GetRequestedTheme();
                    SelectedTheme.Value = _currentMonthThemes.Contains(requestedTheme) ? requestedTheme.ToString() : Enums.SiteTheme.Default.ToString();
                }
            }

            //uncomment this if alway show all venues 
            //_currentMonthThemes = Enums.EnumToList<Enums.SiteTheme>().ToList();

            rptVenues.ItemDataBound += RptVenuesItemDataBound;
            rptVenues.DataSource = _currentMonthThemes.Distinct().OrderBy(theme => theme);
            rptVenues.DataBind();
        }

        void RptVenuesItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem == null)
                return;

            var venueName = (Enums.SiteTheme)e.Item.DataItem;

            var venueAnchor = e.Item.FindControl("lnkVenueFilter") as HyperLink;
            if (venueAnchor != null)
            {
                venueAnchor.Text = Enums.GetEnumDescription(venueName);
                venueAnchor.CssClass = venueName.ToString();
            }
        }

        private List<Performance> newyearseve;
        private Repeater newyearseverpt;
        private KeyValuePair<DayOfWeek, IndividualCalendarDay> newyearsevecalandardary;
        private KeyValuePair<DayOfWeek, IndividualCalendarDay> murrayhillcalandardary;
        Dictionary<Performance, Enums.SiteTheme> _currentDayPerformances = new Dictionary<Performance, Enums.SiteTheme>();
        protected override void BindDayCell(KeyValuePair<DayOfWeek, TheaterTemplate.Shared.PerformanceObjects.IndividualCalendarDay> calendarDay, RepeaterItem courseCell)
        {
            currentCalendarDay = calendarDay;
            _currentDayPerformances.Clear();

            var ltrDay = courseCell.FindControl("ltrDay") as Literal;
            if (ltrDay != null)
            {
                ltrDay.Text = calendarDay.Value.CurrentDate.Day.ToString(CultureInfo.InvariantCulture);
            }

            var calendarCell = courseCell.FindControl("calendarCell") as HtmlTableCell;
            if (calendarCell != null)
            {
                if (calendarDay.Value.IsPreviousMonth || calendarDay.Value.IsNextMonth)
                {
                    calendarCell.Attributes["class"] = "offDay";
                }
                if (calendarDay.Value.CurrentDate.Date == DateTime.Now.Date)
                {
                    calendarCell.Attributes["class"] = string.Format("{0} {1}", calendarCell.Attributes["class"], "today");
                }
                if (calendarDay.Value.CurrentDate.Date < DateTime.Now.Date)
                {
                    calendarCell.Attributes["class"] = string.Format("{0} {1}", calendarCell.Attributes["class"], "prevDay");
                }
            }


            if (calendarDay.Value.Performances.Count > 0)
            {
                if ((calendarDay.Value.IsPreviousMonth && !Config.GetValue(TheaterSharedAppSettings.CALENDAR_SHOW_PREVIOUS, true)) ||
                    (calendarDay.Value.IsNextMonth && !Config.GetValue(TheaterSharedAppSettings.CALENDAR_SHOW_NEXT, true)))
                {
                    return;
                }

                //If calendar is passed the current date and configured to hide, then skip
                if (calendarDay.Value.CurrentDate < DateTime.Today && !Config.GetValue(TheaterSharedAppSettings.CALENDAR_SHOW_PAST_PERFORMANCES, true))
                    return;
                
                var rptDayVenues = courseCell.FindControl("rptDayVenues") as Repeater;
                if (rptDayVenues != null)
                {
                    rptDayVenues.ItemDataBound += RptDayVenuesItemDataBound;
                    
                    foreach (Performance performance in currentCalendarDay.Value.Performances)
                    {
                        var detail = CurrentSeasonPlayDetails.GetPlayDetail(performance.ProductionSeasonId);
                        if (detail == null || detail.HideFromCalendar == false)
                        {
                            var theme = SiteThemeHelper.GetSiteTheme(performance);
                            _currentDayPerformances.Add(performance, theme);
                        }
                    }

                    var availableVenues = _currentDayPerformances.Values.Distinct().OrderBy(theme => theme);


                    _currentMonthThemes.AddRange(availableVenues);
                    rptDayVenues.DataSource = availableVenues;
                    rptDayVenues.DataBind();

                    
                }
            }
        }

        void RptDayVenuesItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.DataItem == null)
                return;

            var currentTheme = (Enums.SiteTheme)e.Item.DataItem;

            var ltrTheme = e.Item.FindControl("ltrThemeName") as Literal;
            if (ltrTheme != null)
            {
                ltrTheme.Text = Enums.GetEnumDescription(currentTheme);
            }

            var filterPanel = e.Item.FindControl("pnlVenueFilter") as Panel;
            if (filterPanel != null)
            {
                filterPanel.CssClass = currentTheme.ToString();
            }

            var rptDayPerformances = e.Item.FindControl("rptDayPerformances") as Repeater;
            if (rptDayPerformances != null)
            {
                rptDayPerformances.ItemDataBound += rptDayPerformances_ItemDataBound;

                var themePerfs = _currentDayPerformances.Where(p => p.Value == currentTheme && p.Key.PerformanceId != 26922).Select(p => p.Key).OrderBy(p => p.PerformanceDate);
                var murrayshow = _currentDayPerformances.Where(p => p.Value == currentTheme && p.Key.PerformanceId == 26922).Select(p => p.Key).OrderBy(p => p.PerformanceDate);

                if (murrayshow.Any() && newyearseve != null && newyearseverpt !=null)
                {
                    murrayhillcalandardary = currentCalendarDay;
                    currentCalendarDay = newyearsevecalandardary;
                    var corrected = newyearseve.ToList();
                    corrected.Add(murrayshow.FirstOrDefault());
                    var newcorrected = corrected.OrderBy(p => p.PerformanceDate);
                    newyearseverpt.DataSource = newcorrected;
                    newyearseverpt.DataBind();

                }
                var t = themePerfs.FirstOrDefault();
                if (t != null && t.PerformanceDate.Month == 12 && t.PerformanceDate.Day == 31 && t.PerformanceType == "Joe's Pub")
                {
                    newyearseverpt = rptDayPerformances;
                    newyearseve = themePerfs.ToList();
                    newyearsevecalandardary = currentCalendarDay;

                }
                rptDayPerformances.DataSource = themePerfs;
                rptDayPerformances.DataBind();
            }
        }
        private void murrayhillPopulatePerformanceDetails(Performance currentPerformance, RepeaterItem repeaterRow){
            PlayDetail detail = CurrentSeasonPlayDetails.GetPlayDetail(currentPerformance.ProductionSeasonId);
            Panel pnlImage = repeaterRow.FindControl("pnlImage") as Panel;
            Image imgPerformance = repeaterRow.FindControl("imgPerformance") as Image;
            Literal ltrPerformanceTitle = repeaterRow.FindControl("ltrPerformanceTitle") as Literal;
            Literal ltrPerformanceDate = repeaterRow.FindControl("ltrPerformanceDate") as Literal;
            HyperLink lnkBuyTicket = repeaterRow.FindControl("lnkBuyTicket") as HyperLink;
            HyperLink lnkViewDetails = repeaterRow.FindControl("lnkViewDetails") as HyperLink;
            Label lblPerformanceTime = repeaterRow.FindControl("lblPerformanceTime") as Label;
            Label lblPerformanceTitle = repeaterRow.FindControl("lblPerformanceTitle") as Label;
            Literal ltrProductionDetails = repeaterRow.FindControl("ltrProductionDetails") as Literal;
            HtmlGenericControl gncFriendsCharm = repeaterRow.FindControl("gncFriendsCharm") as HtmlGenericControl;

            if (detail != null)
            {
                if (detail.HideFromCalendar)
                {
                    repeaterRow.Visible = false;
                    return;
                }

                if (string.IsNullOrEmpty(detail.ThumbnailUrl))
                    pnlImage.Visible = false;
                else
                    imgPerformance.ImageUrl = detail.ThumbnailUrl;

                if (string.IsNullOrEmpty(detail.Heading) == false)
                {
                    ltrPerformanceTitle.Text = detail.Heading;
                    lblPerformanceTitle.Text = detail.Heading;
                }
                else
                {
                    ltrPerformanceTitle.Text = currentPerformance.Title;
                    lblPerformanceTitle.Text = currentPerformance.Title;
                }

                if (string.IsNullOrEmpty(detail.CalendarSynopsis) == false)
                    ltrProductionDetails.Text = detail.CalendarSynopsis;
                else if (string.IsNullOrEmpty(detail.ProductionSynopsis) == false)
                    ltrProductionDetails.Text = detail.ProductionSynopsis;

                if (string.IsNullOrEmpty(detail.PlayDetailLink) == false)
                    lnkViewDetails.NavigateUrl = detail.PlayDetailLink;
                else
                    lnkViewDetails.Visible = false;
            }
            else
            {
                ltrPerformanceTitle.Text = currentPerformance.Title;
                lblPerformanceTitle.Text = currentPerformance.Title;
                lnkViewDetails.Visible = false;
            }

            ltrPerformanceDate.Text = string.Format("{0} - {1}",
                                                     currentPerformance.PerformanceDate.ToString(Config.GetValue(TheaterSharedAppSettings.PERFORMANCE_DATE_DISPLAY_FORMAT, "dddd, MMMM d")),
                                                     currentPerformance.PerformanceDate.ToString(Config.GetValue(TheaterSharedAppSettings.PERFORMANCE_TIME_FORMAT, "h:mm tt")));

            lblPerformanceTime.Text = currentPerformance.PerformanceDate.ToString("h:mm tt");
            gncFriendsCharm.Attributes["data-pid"] = currentPerformance.PerformanceId.ToString();

            try
            {
                if (currentCalendarDay.Value.OnSale(currentPerformance).CalendarOnSale)
                {
                    lnkBuyTicket.NavigateUrl = string.Format(Config.GetValue(TheaterSharedAppSettings.RESERVE_PAGE_URL, "/reserve/index.aspx?performanceNumber={0}"),
                                                             currentPerformance.PerformanceId);
                }
                else
                {
                    lnkBuyTicket.Visible = false;
                }
            }
            catch (ApplicationException ex)
            {
                if (currentCalendarDay.Equals(newyearsevecalandardary))
                {
                    try
                    {
                        if (murrayhillcalandardary.Value.OnSale(currentPerformance).CalendarOnSale)
                        {
                            lnkBuyTicket.NavigateUrl =
                                string.Format(
                                    Config.GetValue(TheaterSharedAppSettings.RESERVE_PAGE_URL,
                                        "/reserve/index.aspx?performanceNumber={0}"),
                                    currentPerformance.PerformanceId);
                            return;
                        }
                        else
                        {
                            lnkBuyTicket.Visible = false;
                            return;
                        }
                    }
                    catch (ApplicationException ex2)
                    {
                        
                    }
                }
                //OnSale(...) throws an exception if on-sale information is not found. Which will occur if it doesn't have the correct on-sale information
                //This means the collection PerformancesOnSale had trouble loading from the GetPerformances call
                lnkBuyTicket.Visible = false;
                int perf = currentPerformance != null ? currentPerformance.PerformanceId : 0;
                Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("Calendar Control - OnSale Performance Failed - {0} - {1}", perf, ex.StackTrace), null);
            }
        }

        protected override void PopulatePerformanceDetails(Performance currentPerformance, RepeaterItem repeaterRow)
        {
            //base.PopulatePerformanceDetails(currentPerformance, repeaterRow);
            //Temporary - remove after Murray hill!!

            murrayhillPopulatePerformanceDetails(currentPerformance, repeaterRow);

            // /end Temporary 



            var detail = CurrentSeasonPlayDetails.GetPlayDetail(currentPerformance.ProductionSeasonId) as PublicPlayDetail;
            if (detail != null && string.IsNullOrEmpty(detail.BuyTicketLinkText) == false)
            {
                HyperLink lnkBuyTicket = repeaterRow.FindControl("lnkBuyTicket") as HyperLink;
                lnkBuyTicket.Text = detail.BuyTicketLinkText;
            }
            else if (PerformanceHelper.IsSummerParkPerformance(currentPerformance))
            {
                HyperLink lnkBuyTicket = repeaterRow.FindControl("lnkBuyTicket") as HyperLink;
                lnkBuyTicket.Text = "Reserve Your Seats";
            }
            if (detail != null && detail.HideBuyTicketLinkFromCalendar)
            {
                HyperLink lnkBuyTicket = repeaterRow.FindControl("lnkBuyTicket") as HyperLink;
                lnkBuyTicket.Visible = false;
            }
            Label lblPerformanceTime = repeaterRow.FindControl("lblPerformanceTime") as Label;
            if (currentPerformance.PerformanceDate.ToString("h:mm tt") == "11:59 PM")
            {
                lblPerformanceTime.Text = "Midnight";
                Literal ltrPerformanceDate = repeaterRow.FindControl("ltrPerformanceDate") as Literal;
                ltrPerformanceDate.Text = string.Format("{0} - Midnight",
                                                     currentPerformance.PerformanceDate.ToString(Config.GetValue(TheaterSharedAppSettings.PERFORMANCE_DATE_DISPLAY_FORMAT, "dddd, MMMM d")));
            }
            else if (currentPerformance.PerformanceDate.ToString("h:mm tt") == "11:58 PM")
            {
                lblPerformanceTime.Text = "Midnight";
                Literal ltrPerformanceDate = repeaterRow.FindControl("ltrPerformanceDate") as Literal;
                ltrPerformanceDate.Text = string.Format("{0} - " + Config.GetValue("1158_mask", "1:00 AM"),
                                                     currentPerformance.PerformanceDate.ToString(Config.GetValue(TheaterSharedAppSettings.PERFORMANCE_DATE_DISPLAY_FORMAT, "dddd, MMMM d")));
            }


            var colorBox = repeaterRow.FindControl("colorBox") as HtmlGenericControl;
            colorBox.Attributes["data-pid"] = currentPerformance.PerformanceId.ToString();
        }

    }
}
