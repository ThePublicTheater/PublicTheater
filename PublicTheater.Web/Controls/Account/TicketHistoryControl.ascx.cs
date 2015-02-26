using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Adage.Tessitura.UserObjects;
using Adage.Tessitura;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Web.Controls.Account
{
    public class TicketHistoryControl : TheaterTemplate.Web.Controls.AccountControls.TicketHistoryControl
    {

        protected global::System.Web.UI.WebControls.Repeater rptPastTicketHistory;

        #region  Properties
        private ExchangeManager _exchangeManager;
        protected override ExchangeManager TicketExchangeManager
        {
            get
            {
                return base.TicketExchangeManager ?? _exchangeManager;
            }
        }
        #endregion
        

        protected override void Page_Load(object sender, EventArgs e)
        {
            rptPastTicketHistory.ItemDataBound += new RepeaterItemEventHandler(rptTicketHistory_ItemDataBound);
            base.Page_Load(sender, e);
        }

        protected override void BindPerformanceData(System.Web.UI.WebControls.RepeaterItem itemRow)
        {
            base.BindPerformanceData(itemRow);

            BindPublicPerformanceData(itemRow, this);

            var lnkPerformanceTitle = itemRow.FindControl("lnkPerformanceTitle") as HyperLink;
            if (lnkPerformanceTitle != null)
            {
                var historyItems = (KeyValuePair<Performance, List<TicketHistorySubLineItem>>)itemRow.DataItem;
                var individualItem = historyItems.Value.First();
                var playDetail = CurrentSeasonPlayDetails.GetPlayDetail(individualItem.ProductionSeasonId);
                if (playDetail != null && string.IsNullOrWhiteSpace(playDetail.Heading) == false)
                {
                    lnkPerformanceTitle.Text = playDetail.Heading;
                    lnkPerformanceTitle.NavigateUrl = playDetail.PlayDetailLink;
                    lnkPerformanceTitle.Visible = true;

                    var ltrPerformanceTitle = itemRow.FindControl("ltrPerformanceTitle") as Literal;
                    ltrPerformanceTitle.Visible = false;
                }
                else
                {
                    lnkPerformanceTitle.Visible = false;
                }

            }
        }

        protected override void DisplaySeats(RepeaterItem itemRow, Dictionary<string, List<string>> sectionAndSeats)
        {
            //don;t show seats details, just number of tix, similar to GA tix
            //base.DisplaySeats(itemRow, sectionAndSeats);

            var historyItems = (KeyValuePair<Performance, List<TicketHistorySubLineItem>>)itemRow.DataItem;
            DisplaySeatCountOnly(itemRow, historyItems.Value);
        }

        private static void BindPublicPerformanceData(System.Web.UI.WebControls.RepeaterItem itemRow, TicketHistoryControl control)
        {
            var printTickets = itemRow.FindControl("printTickets") as LinkButton;
            if (printTickets != null)
            {
                //print and barcode is not setup yet
                printTickets.Visible = false;
            }

            var exchangeTickets = itemRow.FindControl("exchangeTickets") as LinkButton;
            if (exchangeTickets != null)
            {
                //exchange is not setup yet
                exchangeTickets.Visible = false;
            }
        }

        protected override void BindTicketHistory()
        {
            try
            {
                TicketHistorySubLineItems ticketHistoryItems = TicketHistorySubLineItems.Get(GetCriteria());
                _exchangeManager = ticketHistoryItems.GetExchangeManager();
                var performanceItems = GetGroupedPerformanceHistoryItems(ticketHistoryItems);

                var futurePerformanceItems = performanceItems.Where(p => p.Key.PerformanceDate > DateTime.Now).ToList();
                var pastPerformanceItems = performanceItems.Where(p => p.Key.PerformanceDate <= DateTime.Now).ToList();

                if (performanceItems.Count > 0)
                {
                    if (futurePerformanceItems.Count > 0)
                    {
                        
                        rptTicketHistory.DataSource = futurePerformanceItems;
                        rptTicketHistory.DataBind();
                    }
                    else
                    {
                        rptTicketHistory.Visible = false;
                    }
                    if (pastPerformanceItems.Count > 0)
                    {
                        var config = TicketHistoryConfigPage as PublicTicketHistoryConfig;
                        if (config != null)
                        {
                            if (config.PastPerformanceShowEarliestFirst)
                            {
                                pastPerformanceItems = pastPerformanceItems.OrderBy(p => p.Key.PerformanceDate).ToList();
                            }
                            else
                            {
                                pastPerformanceItems = pastPerformanceItems.OrderByDescending(item => item.Key.PerformanceDate).ToList();
                            }
                        }

                        rptPastTicketHistory.DataSource = pastPerformanceItems;
                        rptPastTicketHistory.DataBind();
                    }
                    else
                    {
                        rptPastTicketHistory.Visible = false;
                    }
                }
                else
                {
                    rptTicketHistory.Visible = false;
                    rptPastTicketHistory.Visible = false;
                    NoTicketHistory.Visible = true;
                }

                var addOnItems = GetGroupedAddOnItems(ticketHistoryItems);
                if (addOnItems.Count > 0)
                {
                    pnlAddOns.Visible = true;
                    rptAddOns.DataSource = addOnItems;
                    rptAddOns.DataBind();
                }
            }
            catch (Exception ex)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("Ticket History - {0} - {1}", ex.Message, ex.StackTrace), null);
                rptTicketHistory.Visible = false;
                rptPastTicketHistory.Visible = false;

                NoTicketHistory.Visible = true;
            }
        }




    }
}