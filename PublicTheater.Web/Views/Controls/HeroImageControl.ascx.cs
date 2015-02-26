using System;
using System.Collections.Generic;
using Adage.Theater.RelationshipManager.Helpers;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Hosting;
using LinqToTwitter;
using Microsoft.Ajax.Utilities;
using PublicTheater.Custom.Episerver;
using PublicTheater.Custom.Episerver.BaseClasses;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Web.Views.Controls
{
    public partial class HeroImageControl : PublicBaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgHero.ImageUrl = GetHeroImage();
            }
        }

        /// <summary>
        /// Get hero image for current page
        /// </summary>
        /// <returns></returns>
        private string GetHeroImage()
        {

            if (CurrentPage != null && CurrentPage.Property["HeroImagePool"] != null)
            {
                try
                {
                    UnifiedDirectory dir =
                        System.Web.Hosting.HostingEnvironment.VirtualPathProvider.GetDirectory(
                            Url.Decode(CurrentPage.Property["HeroImagePool"].ToString())) as UnifiedDirectory;
                    System.Collections.IEnumerable allFiles = dir.Files;
                    var pool = new List<string>();
                    foreach (UnifiedFile file in allFiles)
                    {
                        pool.Add(file.VirtualPath);
                    }


                    if (pool.Count >= 1)
                    {
                        return
                            pool[System.DateTime.Now.Millisecond%pool.Count];
                    }
                }
                catch (Exception e)
                {
                }
                
            }
            //current page has valid hero image, use it
            if (CurrentPage != null && CurrentPage.Property["HeroImage"] != null && !string.IsNullOrEmpty(CurrentPage.Property["HeroImage"].ToString()))
            {
                return CurrentPage.Property["HeroImage"].ToString();
            }
            //if site theme has hero image
            else if (PublicTheater.Custom.Episerver.Utility.Utility.GetRequestedTheme(CurrentPage) != Enums.SiteTheme.Default)
            {
                var siteThemeHero =
                    Adage.Tessitura.Config.GetValue(
                        PublicTheater.Custom.Episerver.Utility.Utility.GetRequestedTheme(CurrentPage).ToString() + "SiteThemeImageURL");
                if (!siteThemeHero.IsNullOrWhiteSpace())
                {
                    return siteThemeHero;
                }
            }
            //if no valid define for current page, use defult one defined in home page
            return DataFactory.Instance.GetPage(ContentReference.StartPage).Property["GlobalHeroImage"].ToString();
        }
    }
}