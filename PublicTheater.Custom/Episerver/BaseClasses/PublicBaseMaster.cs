using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adage.Common.ExtensionMethods.Generic;
using EPiServer;
using EPiServer.Core;

namespace PublicTheater.Custom.Episerver.BaseClasses
{
    public class PublicBaseMaster : System.Web.UI.MasterPage
    {
        #region site theme

        private Enums.SiteTheme? _requestTheme;

        protected Enums.SiteTheme RequestedTheme
        {
            get
            {
                if (_requestTheme.HasValue) return _requestTheme.Value;
                if (string.IsNullOrEmpty(Request.QueryString[Constants.QueryStringParameters.SiteTheme]) ==
                    false)
                {
                    
                    _requestTheme =
                        (Enums.SiteTheme)
                            Enum.Parse(typeof (Enums.SiteTheme),
                                Request.QueryString[Constants.QueryStringParameters.SiteTheme], true);

      
                        Session["requestTheme"] = _requestTheme;

                    return _requestTheme.Value;
                }
                if (inCartPath() && Session["requestTheme"] != null)
                {
                    _requestTheme = (Enums.SiteTheme) Session["requestTheme"];
                    return _requestTheme.Value;
                }
                try
                {
                    _requestTheme = (Enums.SiteTheme)(CurrentPage.Property["SiteTheme"].Value);
                    return _requestTheme.Value;
                }
                catch (Exception)
                {
                }
                _requestTheme = Enums.SiteTheme.Default;
                return _requestTheme.Value;
            }
        }

        #endregion

        private PageData _currentPage;

        protected virtual PageData CurrentPage
        {
            get
            {
                if (_currentPage == null)
                {
                    var pageBase = Page as PageBase;
                    _currentPage = pageBase != null ? (pageBase).CurrentPage : new PageData();
                }
                return _currentPage;
            }
        }

        private Pages.HomePageData _currentHomePage;

        protected virtual Pages.HomePageData CurrentHomePage
        {
            get
            {
                return _currentHomePage ??
                       (_currentHomePage =
                           DataFactory.Instance.GetPage(ContentReference.StartPage) as Pages.HomePageData);
            }
        }

        protected bool inCartPath()
        {
            string cartPathPages = Adage.Tessitura.Config.GetValue("cartPathPages");
            string[] arrCartPathStrings = cartPathPages.Split(';');
            return arrCartPathStrings.Contains(Convert.ToString((CurrentPage.PageLink.ID)));

        }
    }
}
