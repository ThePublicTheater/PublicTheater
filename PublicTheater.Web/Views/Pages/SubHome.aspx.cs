using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PublicTheater.Custom.Episerver;
using PublicTheater.Custom.Episerver.Utility;

namespace PublicTheater.Web.Views.Pages
{
    public partial class SubHome : Custom.Episerver.BaseClasses.PublicBasePage<Custom.Episerver.Pages.SubHomePageData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CurrentPage.UpcomingPerformances.NumberOfDaysToShow <= 0)
            {
                Property2.Visible = false;
            }
            if(!IsPostBack)
            {
                if(RequestedTheme != CurrentPage.SiteTheme )
                {
                    Response.Redirect(CurrentPage.GetSiteThemeUrl());
                }
            }
        }
    }
}