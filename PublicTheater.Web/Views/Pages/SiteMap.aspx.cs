using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer.Core;

namespace PublicTheater.Web.Views.Pages
{
    public partial class SiteMap : Custom.Episerver.BaseClasses.PublicBasePage<PublicTheater.Custom.Episerver.Pages.SiteMapPageData>
    {
        private PageReference _indexRoot;

        /// <summary>
        /// Gets the page used as the root for the site map
        /// </summary>
        /// <remarks>If the IndexRoot page property is not set the start page will be used instead</remarks>
        public PageReference IndexRoot
        {
            get
            {
                if (PageReference.IsNullOrEmpty(_indexRoot))
                {
                    _indexRoot = CurrentPage.RootPage ?? ContentReference.StartPage;
                }
                return _indexRoot;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}