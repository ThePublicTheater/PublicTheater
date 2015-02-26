using System;
using System.Collections.Generic;
using EPiServer;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Hosting;
using EPiServer.Web.Routing;
using EPiServer.Web.WebControls;
//using System.ServiceModel.Syndication;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Web.Views.Pages
{
    [TemplateDescriptor(Path = "~/Views/Pages/PodcastFeed.aspx")]
    public partial class PodcastFeed : Custom.Episerver.BaseClasses.PublicBasePage<Custom.Episerver.Pages.PodcastFeedData>
    {
        static UnifiedFile getFile(string fileUrl)
        {
            try
            {
                string filePath = Url.Decode(fileUrl);

                var file = System.Web.Hosting.HostingEnvironment.VirtualPathProvider.GetFile(filePath) as UnifiedFile;
                return file;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            Response.ContentType = "application/rss+xml";
            
            var feed = CurrentPage.GetFeedItems();

            var validatedFeed = new List<object>();
            
            foreach (var item in feed)
            {
                var mp3 = item.Mp3Url;
                var file = getFile(mp3);
                if(file == null) {continue;}
                var validatedObject = new
                {
                    length = file.Length, item.Title,item.SubTitle, 
                    ImageUrl = item.ImageUrl ?? CurrentPage.CImageUrl,
                    item.Mp3Url, item.PublicationDate, item.Duration, item.Summary

                };
                validatedFeed.Add(validatedObject);

            }

            Repeater1.DataSource = validatedFeed;

            Repeater1.DataBind();

            


        }

        private string GetFullyQualifiedUrl(string url)
        {
            return string.Concat(Request.Url.GetLeftPart(UriPartial.Authority), ResolveUrl(url));
        }

    }

}