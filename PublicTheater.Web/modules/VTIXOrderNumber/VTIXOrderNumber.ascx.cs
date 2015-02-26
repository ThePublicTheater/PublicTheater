using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer;
using EPiServer.Core;
using EPiServer.DynamicContent;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using PublicTheater.Custom.VTIX;
using Adage.Tessitura;

namespace PublicTheater.Web.modules.VTIXOrderNumber
{
    [DynamicContentPlugIn(DisplayName = "VTixOrderNumber", ViewUrl = "~/modules/VTIXOrderNumber/VTIXOrderNumber.ascx")]
    public partial class VTIXOrderNumber : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = User.GetUser();
            if (user.LoggedIn && !user.IsAnonymousLogin)
            {
                var criteria = new Adage.Tessitura.UserObjects.TicketHistorySubLineItemCriteria();
                criteria.StartDate = DateTime.Today;
               

                Adage.Tessitura.UserObjects.TicketHistorySubLineItems items= Adage.Tessitura.UserObjects.TicketHistorySubLineItems.Get(criteria);
                foreach (var item in items)
                {
                    Adage.Tessitura.Performance p = Adage.Tessitura.Performance.GetPerformance(item.PerformanceId);
                    if (p.PerformanceTypeId == Convert.ToInt32(Adage.Tessitura.Config.GetValue("PerfType_Shakespeare")) && item.OrderDate.DayOfYear==DateTime.Today.DayOfYear && item.OrderDate.Year== DateTime.Today.Year)
                    {
                        lblOrderNum.Text += item.OrderNumber.ToString();
                        return;
                    }
                }
            }
        }
    }
}