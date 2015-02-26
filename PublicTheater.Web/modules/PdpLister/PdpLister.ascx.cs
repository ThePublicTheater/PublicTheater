using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using EPiServer;
using EPiServer.Core;
using EPiServer.DynamicContent;
using EPiServer.Filters;
using PublicTheater.Custom.Episerver.Pages;

namespace PublicTheater.Web.modules.PdpLister
{
    [DynamicContentPlugIn(DisplayName = "PdpLister", ViewUrl = "~/modules/PdpLister/PdpLister.ascx")]
    public partial class PdpLister : UserControl
    {
        #region Editable Properties

        // Add your editable properties as normal .Net properties.
        // Supported property types are string, int, bool, 
        // EPiServer.Core.PageReference and any class
        // inheriting EPiServer.Core.PropertyData.

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            var crit = new ProductionsCriteria()
                           {
                               StartDate = DateTime.Now.Subtract(new TimeSpan(20, 0, 0, 0)),
                               EndDate = DateTime.Now.Add(new TimeSpan(900, 0, 0, 0))
                           };
            var prods = Productions.GetProductions(crit);

            var pdpPageCriteria = new PropertyCriteria()
            {
                Name = "PageTypeID",
                Condition = CompareCondition.Equal,
                Required = true,
                Type = PropertyDataType.PageType,
                Value = Config.GetValue<string>("PDP_PAGE_TYPE", string.Empty)
            };


            foreach (var prod in prods)
            {
                var cc = new PropertyCriteriaCollection();
                cc.Add(pdpPageCriteria);
                cc.Add(new PropertyCriteria()
                {
                    Name = "TessituraId",
                    Condition = CompareCondition.Equal,
                    Required = true,
                    Type = PropertyDataType.Number,
                    Value = prod.ProductionSeasonNumber.ToString()
                });
                var pdpPage = DataFactory.Instance.FindPagesWithCriteria(ContentReference.StartPage, cc).FirstOrDefault();
                
                if (pdpPage != null)
                {
                    ProdDiv.InnerHtml += "<a href='" + pdpPage.LinkURL + "'>" + ((PlayDetailPageData)pdpPage).Heading + "</a><br/>";
                    //ProdDiv.InnerHtml += "<div id=\"entry-" + prod.ProductionSeasonNumber + "\"><a href='/epi/Cms/#context=epi.cms.contentdata:///" + pdpPage.ContentLink.ID + "'>" + pdpPage.Name + "</a></div>";
                }

            }

        }
    }
}
