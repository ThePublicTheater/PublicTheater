using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer;
using PublicTheater.Custom.Episerver.Pages;

namespace PublicTheater.Web.Views.Pages
{
    public partial class Home : Custom.Episerver.BaseClasses.PublicBasePage<HomePageData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                rptSlideShowCaptions.DataSource = CurrentPage.GetHeroImages();
                rptSlideShowCaptions.DataBind();    
                    
                rptSlideShowImage.DataSource = CurrentPage.GetHeroImages();
                rptSlideShowImage.DataBind();

                
            }
            
        }
    }
}