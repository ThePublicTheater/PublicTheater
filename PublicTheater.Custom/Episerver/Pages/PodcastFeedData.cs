using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using PublicTheater.Custom.Episerver.BaseClasses;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "863d6cf9-8f67-4b63-be0f-fee63afa32d7", DisplayName = "[Public Theater] Podcast", GroupName = Constants.ContentGroupNames.MediaGroupName)]
    public class PodcastFeedData : PageData
    {
        [Display(Name = "Podcast Title", Description = "", GroupName = SystemTabNames.Content, Order = 1)]
        public virtual string PodcastTitle { get; set; }
        
        [Display(Name = "Podcast SubTitle", Description = "", GroupName = SystemTabNames.Content, Order = 2)]
        public virtual string PodcastSubtitle { get; set; }
        
        [Display(Name = "Podcast Summary", Description = "", GroupName = SystemTabNames.Content, Order = 3)]
        [UIHint(UIHint.Textarea)]
        public virtual string PodcastSummary { get; set; }

        [Display(Name = "Podcast Link Url", Description = "", GroupName = SystemTabNames.Content, Order = 4)]
        public virtual Url PodcastLinkUrl { get; set; }

        [Display(Name = "Copyright", Description = "", GroupName = SystemTabNames.Content, Order = 5)]
        public virtual string Copyright { get; set; }

        [Display(Name = "Author", Description = "", GroupName = SystemTabNames.Content, Order = 6)]
        public virtual string Author { get; set; }

        [Display(Name = "Image", Description = "", GroupName = SystemTabNames.Content, Order = 7)]
        [UIHint(UIHint.Image)]
        public virtual Url CImageUrl { get; set; }

        //[Display(Name = "Explicit podcast?", Description = "", GroupName = SystemTabNames.Content, Order = 8)]
        //public virtual bool Explicit { get; set; }

        [Display(Name = "itunes Owner Name", Description = "", GroupName = SystemTabNames.Content, Order = 9)]
        public virtual string ItunesOwnerName { get; set; }

        [Display(Name = "itunes Owner Email", Description = "", GroupName = SystemTabNames.Content, Order = 10)]
        public virtual string ItunesOwnerEmail { get; set; }
        
        [Display(Name = "itunes Category", Description = "", GroupName = SystemTabNames.Content, Order = 11)]
        public virtual string ItunesCategory { get; set; }

        [Display(Name = "itunes Sub-Category", Description = "", GroupName = SystemTabNames.Content, Order = 12)]
        public virtual string ItunesSubCategory { get; set; }

        [Display(Name = "podcast episodes", Description = "", GroupName = SystemTabNames.Content, Order = 1)]
        [BackingType(typeof(PodcastItems))]

        public virtual string FeedItems  { get; set; }

        public virtual List<PodcastItem> GetFeedItems()
        {
            var feedLinks = Property["FeedItems"] as PodcastItems;

            if (feedLinks != null)
                return feedLinks.PodcastItemList;

            return new List<PodcastItem>();
        }



        
    }

}
