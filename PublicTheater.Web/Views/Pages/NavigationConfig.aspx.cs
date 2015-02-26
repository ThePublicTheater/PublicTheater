using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer;
using EPiServer.Core;
using PublicTheater.Custom.Episerver.Utility;

namespace PublicTheater.Web.Views.Pages
{
    public partial class NavigationConfig : Custom.Episerver.BaseClasses.PublicBasePage<PublicTheater.Custom.Episerver.Pages.NavigationConfig>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptJoesPub.DataSource = CurrentPage.MenuItems.Contents.OfType<PageData>().Where(p => p.VisibleInMenu && p.IsVisibleOnSite());
                rptJoesPub.DataBind();

                if (CurrentPage.SubHomePage != null)
                {
                    lnkSubNavLogo.NavigateUrl = Utility.GetFriendlyUrl(CurrentPage.SubHomePage);    
                }
                

                if (CurrentPage.LogoImage == null)
                {
                    lnkSubNavLogo.Visible = false;
                }
                else
                {
                    lnkSubNavLogo.ImageUrl = CurrentPage.LogoImage.ToString();
                }
            }
        }
    }
}