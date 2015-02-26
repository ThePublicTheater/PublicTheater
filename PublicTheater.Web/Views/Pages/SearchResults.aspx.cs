using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PublicTheater.Custom.Episerver;

namespace PublicTheater.Web.Views.Pages
{
    public partial class SearchResults : Custom.Episerver.BaseClasses.PublicBasePage<Custom.Episerver.Pages.SearchResultsPageData>
    {
        
        protected string RequestUrl
        {
            get
            {
                if (RequestedTheme == Enums.SiteTheme.Default)
                {
                    return string.Empty;
                }
                else
                {
                    return string.Empty;
                    //return Request.UrlReferrer == null ? Request.Url.ToString() : Request.UrlReferrer.ToString();    
                }
            }
        }
        protected string SearchEngineKey
        {
            get
            {
                return WebUtility.EpiHelper.GetHomePage().SearchEngineKey;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}