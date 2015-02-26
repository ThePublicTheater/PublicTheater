using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Personalization.VisitorGroups;
using EPiServer.ServiceLocation;
using PublicTheater.Custom.Episerver.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheaterTemplate.Web.Models.Pages;

namespace PublicTheater.Web.WebUtility
{
    public static class EpiHelper
    {
        #region Injections
        private static Injected<IContentLoader> _contentLoader;
        private static Injected<PageTypeRepository> _pageTypeLoader;


        private static IContentLoader ContentLoader
        {
            get
            {
                return _contentLoader.Service;
            }
        }

        private static PageTypeRepository PageTypeLoader
        {
            get
            {
                return _pageTypeLoader.Service;
            }
        }
        #endregion

        public static HomePageData GetHomePage()
        {
            return ContentLoader.Get<HomePageData>(PageReference.StartPage);
        }

        public static bool IsInVisitorGroup(string roleName)
        {
            var visitorGroupHelp = new VisitorGroupHelper();
            return visitorGroupHelp.IsPrincipalInGroup(EPiServer.Security.PrincipalInfo.CurrentPrincipal, roleName);
        }
    }
}