using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Web;
using PublicTheater.Custom.Episerver;
using PublicTheater.Custom.Episerver.BaseClasses;
using PublicTheater.Custom.Episerver.Utility;

namespace PublicTheater.Web.Views.Blocks
{
    [TemplateDescriptor(Tags = new[] { Constants.RenderingTags.Ticket })]
    public partial class TicketBlockControl : ContentControlBase<Custom.Episerver.Pages.PlayDetailPageData>
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            GetPerformanceAvailabilityFromPDP();
        }

        public virtual void GetPerformanceAvailabilityFromPDP()
        {
            if (CurrentData.TessituraProduction != null && CurrentData.AvailablePerformances.Any())
            {
                tixLink.Visible = popupData.Visible = true;
                propNotAvail.Visible = propSoldOut.Visible = false;
                
                tixLink.InnerText = CurrentData.GetBuyTicketText();
                var ctrl = FindControl("popupData") as HtmlGenericControl;

                var availablePerfs = CurrentData.AvailablePerformances
                    .Where(perf =>
                    {
                        var availSections =
                            perf.PerformancePricing.Sections.Where(
                                section => section.IsAvailable && section.AvailableCount > 0);
                        var priceTypes = perf.PerformancePricing.PriceTypes.Where(
                                pt => availSections.Any(section => section.PriceTypeId == pt.PriceTypeId) && pt.PriceTypeId!=29);
                        return priceTypes.Select(priceType => availSections.FirstOrDefault(sec => sec.PriceTypeId == priceType.PriceTypeId)).Any(section => section != null);
                    })
                    .Select(perf =>
                        new
                        {
                            PerformanceId = perf.PerformanceId,
                            PerformanceDate = perf.PerformanceDate.ToString()
                        })
                        .ToList();

                ctrl.InnerText = new JavaScriptSerializer().Serialize(availablePerfs);
            }
            else
            {
                tixLink.Visible = popupData.Visible = false;
                tixLink.InnerText = "Details";
                tixLink.HRef = CurrentData.GetRequestedSiteThemeUrl();
                if (CurrentData.IsSoldOut)
                {
                    propSoldOut.Visible = true;
                }
                else
                {
                    
                    propNotAvail.Visible = true;
                }
            }
        }
    }
}
