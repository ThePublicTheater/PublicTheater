using System;
using System.Linq;
using EPiServer;
using PublicTheater.Custom.Episerver.Pages;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class YouMayAlsoLikeBlockControl : Custom.Episerver.BaseClasses.PublicBaseBlockControl<Custom.Episerver.Blocks.YouMayAlsoLikeBlockData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindSuggestions();    
            }
        }

        private void BindSuggestions()
        {
            var playDetailsPage =  CurrentPage as PlayDetailPageData;
            if (playDetailsPage == null || playDetailsPage.ParentLink==null)
            {
                this.Visible = false;
                return;
            }

            var siblings = DataFactory.Instance.GetChildren(playDetailsPage.ParentLink);

            var pdpSiblings = siblings.Where(pdp => pdp is PlayDetailPageData && pdp.ContentLink.ID != playDetailsPage.ContentLink.ID);
            
            rptProductionSuggestions.DataSource = pdpSiblings;
            rptProductionSuggestions.DataBind();
        }
    }
}