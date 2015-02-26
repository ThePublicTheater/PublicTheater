#region Copyright
// Copyright © EPiServer AB.  All rights reserved.
// 
// This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published August 20, 2007. 
// See http://www.episerver.com/Specific_License_Conditions for details.
#endregion

using System;
using System.Web.UI.HtmlControls;
using EPiServer.Core;

namespace PublicTheater.Web.Views.Controls
{
    /// <summary>
    /// The header of the website. Generates metadata tags, rss and css links.
    /// </summary>
    public partial class MetaDataHeader : EPiServer.UserControlBase
    {


        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            CreateMetaData();
        }

        /// <summary>
        /// Creates the metadata tags for the website
        /// </summary>
        private void CreateMetaData()
        {
            if (!(Page is EPiServer.PageBase))
                return;

            // Title - use property if it exists
            string seoTitle = GetPropertyString("SEOTitle", CurrentPage);
            if (!String.IsNullOrEmpty(seoTitle))
            {
                Page.Title = Server.HtmlEncode(string.Format("{0}", seoTitle, EPiServer.Configuration.Settings.Instance.SiteDisplayName));
            }
            else
            {
                Page.Title = Server.HtmlEncode(string.Format("{0}", CurrentPage.PageName, EPiServer.Configuration.Settings.Instance.SiteDisplayName));
            }

            // Keywords
            CreateMetaTag("keywords", "SEOKeywords", CurrentPage, false);

            // Description
            CreateMetaTag("description", "SEODescription", CurrentPage, false);

            // Robots
            CreateMetaTag("robots", "SEORobots", CurrentPage, false);

            // Revisit each month
            CreateMetaTag("revisit-after", "4 weeks", false);

            // Generator
            CreateMetaTag("generator", "EPiServer", false);

            // Charset
            CreateMetaTag("Content-Type", string.Format("text/html; charset={0}", "UTF-8"), true);

            // Created - GMT format
            if (CurrentPage.Created != DateTime.MinValue)
            {
                CreateMetaTag("creation_date", CurrentPage.Created.ToString("R"), false);
            }
            // Last modified data, in GMT format - Note, same as revised
            if (CurrentPage.Changed != DateTime.MinValue)
            {
                CreateMetaTag("last-modified", CurrentPage.Changed.ToString("R"), false);
            }
            // Revised - GMT format
            if (CurrentPage.Changed != DateTime.MinValue)
            {
                CreateMetaTag("revised", CurrentPage.Changed.ToString("R"), false);
            }
            CreateMetaTag("Content-Language", CurrentPage.LanguageBranch, true);
            
            // Set favicon
            var siteUrl = EPiServer.Configuration.Settings.Instance.SiteUrl.ToString();
            var url = siteUrl + (!siteUrl.EndsWith("/") ? "/favicon.ico" : "favicon.ico");
            CreateLinkTag("icon", url);
        }

        /// <summary>
        /// Adds a link tag to the header.
        /// </summary>
        /// <param name="rel">The value of the rel attribute</param>
        /// <param name="href">The value of the href attribute</param>
        private void CreateLinkTag(string rel, string href)
        {
            var linkTag = new HtmlLink();
            linkTag.Attributes.Add("rel", rel);
            linkTag.Href = href;
            LinkArea.Controls.Add(linkTag);
        }

        /// <summary>
        /// Adds a meta tag control to the page header
        /// </summary>
        /// <param name="name">The name of the meta tag</param>
        /// <param name="content">The content of the meta tag</param>
        /// <param name="httpEquiv">True if the meta tag should be HTTP-EQUIV</param>
        private void CreateMetaTag(string name, string content, bool httpEquiv)
        {
            var tag = new HtmlMeta();
            if (httpEquiv)
            {
                tag.HttpEquiv = name;
            }
            else
            {
                tag.Name = name;
            }
            tag.Content = content;
            MetadataArea.Controls.Add(tag);
        }

        /// <summary>
        /// Adds a meta tag control to the page header
        /// </summary>
        /// <param name="name">The name of the meta tag</param>
        /// <param name="propertyName">The name of the me tag page property</param>
        /// <param name="pageData">The page from where to get the property</param>
        /// <param name="httpEquiv">True if the meta tag should be HTTP-EQUIV</param>
        private void CreateMetaTag(string name, string propertyName, PageData pageData, bool httpEquiv)
        {
            var property = pageData[propertyName] as string;
            if (property != null)
            {
                CreateMetaTag(name, property, httpEquiv);
            }
        }

        /// <summary>
        /// Gets a page property as a string
        /// </summary>
        /// <param name="propertyName">The name of the property to get</param>
        /// <param name="pageData">The page from where to get the property</param>
        /// <returns>A string representation of the page property</returns>
        private static string GetPropertyString(string propertyName, ContentData pageData)
        {
            return pageData[propertyName] as string ?? String.Empty;
        }
    }
}