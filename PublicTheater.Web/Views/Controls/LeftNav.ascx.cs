using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using EPiServer;
using EPiServer.Core;
using PublicTheater.Custom.Episerver;
using PublicTheater.Web.Views.MasterPages;
using TheaterTemplate.Shared.Common;
using PublicTheater.Web.WebUtility;

namespace PublicTheater.Web.Views.Controls
{
    public partial class LeftNav : Custom.Episerver.BaseClasses.PublicBaseUserControl
    {
        private IEnumerable<PageData> _mainNavDataSource;
        protected IEnumerable<PageData> MainNavDataSource
        {
            get
            {
                if (_mainNavDataSource == null)
                    _mainNavDataSource = DataFactory.Instance.GetChildren(ContentReference.StartPage).Where(p => p.VisibleInMenu && p.IsVisibleOnSite());
                return _mainNavDataSource;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rptFirstTier.ItemDataBound += rptFirstTier_ItemDataBound;
            if (!IsPostBack)
            {
                rptFirstTier.DataSource = MainNavDataSource;
                rptFirstTier.DataBind();


                PopulateSubSiteThemeControls();

            }

        }

        private void PopulateSubSiteThemeControls()
        {
            if (RequestedTheme != Enums.SiteTheme.Default || CurrentPage is Custom.Episerver.Pages.WatchListenPageData)
            {
                navHeader.Attributes["class"] = navHeader.Attributes["class"] + " collapsed hasSubNav";
                var liBackToSubNav = rptFirstTier.FindControl("liBackToSubNav");
                if (liBackToSubNav != null)
                {
                    liBackToSubNav.Visible = true;
                }
                divToggle.Visible = true;

                if (RequestedTheme != Enums.SiteTheme.Default)
                {
                    ucSearch.Visible = utilityNav1.Visible = false;
                }
            }
           
        }

        void rptFirstTier_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                var lnkLogin = e.Item.FindControl("lnkLogin") as HyperLink;
                var hfNoTess = this.Parent.FindControl("hfNoTess") as HiddenField;
               
                if (lnkLogin != null && hfNoTess==null)
                {
                    var currentUser = Adage.Tessitura.User.GetUser();
                    if (currentUser.IsAnonymousLogin == false && currentUser.LoggedIn)
                    {
                        lnkLogin.NavigateUrl = Config.GetValue(TheaterSharedAppSettings.LOGIN_PAGE_URL, "/account/login");
                        lnkLogin.Text = "Log Out";
                    }
                    else
                    {
                        lnkLogin.NavigateUrl = Config.GetValue(TheaterSharedAppSettings.LOGIN_PAGE_URL, "/account/login");
                        lnkLogin.Text = "Login";
                    }
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var rptSecondTier = e.Item.FindControl("rptSecondTier") as Repeater;
                var rptMobileTier = e.Item.FindControl("rptMobileTier") as Repeater;
                var liNav = e.Item.FindControl("liNav") as System.Web.UI.HtmlControls.HtmlGenericControl;
                var page = e.Item.DataItem as PageData;
                if (rptMobileTier != null && rptSecondTier != null && page != null && liNav != null)
                {
                    var secondTierPages = DataFactory.Instance.GetChildren(page.PageLink).Where(p => p.VisibleInMenu && p.IsVisibleOnSite());

                    rptMobileTier.Visible = rptSecondTier.Visible = secondTierPages.Any();


                    if (rptSecondTier.Visible)
                    {
                        liNav.Attributes["class"] = "has-dropdown";

                        rptSecondTier.DataSource = secondTierPages;
                        rptSecondTier.DataBind();

                        rptMobileTier.DataSource = secondTierPages;
                        rptMobileTier.DataBind();
                    }

                }
            }
        }
    }
}