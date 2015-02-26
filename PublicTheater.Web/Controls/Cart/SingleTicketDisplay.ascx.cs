using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Adage.Tessitura.CartObjects;
using EPiServer.Core;
using PublicTheater.Custom.Episerver.Pages;
using TheaterTemplate.Shared.BaseControls;
using TheaterTemplate.Shared.PerformanceObjects;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Web.Controls.Cart
{
    public class SingleTicketDisplay : TheaterTemplate.Web.Controls.CartControls.SingleTicketDisplay
    {
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl ulCartHeader;

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
        public void HideHeader()
        {
            if (ulCartHeader != null)
            {
                ulCartHeader.Visible = false;
            }
        }

        protected override void BindGroupedTicketItem(RepeaterItem repeaterItem, string sectionDescription, List<Ticket> tickets)
        {
            base.BindGroupedTicketItem(repeaterItem, sectionDescription, tickets);

            Literal ltrPriceTypes = repeaterItem.FindControl("ltrPriceTypes") as Literal;   
            var playDetails=PlayDetailsCollection.GetPlayDetail(
                Adage.Tessitura.Performance.GetPerformance(tickets[0].PerformanceId).ProductionSeasonId);
            if (playDetails == null)
                return;//no pdp
            var oPage = EPiServer.DataFactory.Instance.GetPage(new PageReference(playDetails.PageId));
            var isParkShow = (EPiServer.Core.PropertyBoolean)oPage.Property["IsParkShow"];
            if (isParkShow != null && ltrPriceTypes != null && isParkShow.Boolean)
            {
                ltrPriceTypes.Text = ltrPriceTypes.Text.Replace("ticket", "reserved seat");
            }
            
        }
    }
}