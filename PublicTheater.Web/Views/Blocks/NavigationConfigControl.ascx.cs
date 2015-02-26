using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Web;
using PublicTheater.Custom.Episerver;
using PublicTheater.Custom.Episerver.BaseClasses;
using PublicTheater.Custom.Episerver.Utility;
using PublicTheater.Web.WebUtility;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class NavigationConfigControl : ContentControlBase<Custom.Episerver.Pages.NavigationConfig>
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.GetRequestedTheme(CurrentPage) != CurrentData.SiteTheme)
            {
                this.Visible = false;
                return;
            }

            rptJoesPub.DataSource = CurrentData.MenuItems.Contents.OfType<PageData>().Where(p => p.VisibleInMenu && p.IsVisibleOnSite());
            rptJoesPub.DataBind();

            if (CurrentData.SubHomePage != null)
            {
                lnkSubNavLogo.NavigateUrl = Utility.GetFriendlyUrl(CurrentData.SubHomePage);
                lnkSubNavLogoMobile.NavigateUrl = Utility.GetFriendlyUrl(CurrentData.SubHomePage);
            }

            if (CurrentData.LogoImage == null)
            {
                lnkSubNavLogo.Visible = false;
            }
            else
            {
                lnkSubNavLogo.ImageUrl = CurrentData.LogoImage.ToString();
            }
            if (CurrentData.MobileLogoImage == null && CurrentData.LogoImage != null)
            {
                lnkSubNavLogoMobile.ImageUrl = CurrentData.LogoImage.ToString();
                lnkSubNavLogoMobile.Visible = true;
            }
            if (CurrentData.MobileLogoImage != null)
            {
                lnkSubNavLogoMobile.ImageUrl = CurrentData.MobileLogoImage.ToString();
                lnkSubNavLogoMobile.Visible = true;
            }
        }
        protected string GetCorrectLink(EPiServer.Core.PageData page)
        {
            if (Utility.GetSiteTheme(page) == Enums.SiteTheme.Default)
            {
                Enums.SiteTheme requestedTheme = Utility.GetRequestedTheme(); ;
                requestedTheme = requestedTheme == Enums.SiteTheme.Default ? Utility.GetSiteTheme(CurrentPage) : requestedTheme;
                if (requestedTheme != Enums.SiteTheme.Default)
                {
                    return Utility.GetSiteThemeURL(Utility.GetFriendlyUrl(page.PageLink), requestedTheme);
                }
            }
            var reqsturl = page.GetRequestedSiteThemeUrl();
            return reqsturl;
        }
    }
}