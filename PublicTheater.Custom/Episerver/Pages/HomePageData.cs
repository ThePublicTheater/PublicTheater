using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;
using System.Collections.Generic;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "97b92026-b744-4041-999b-cd739574e5fa", DisplayName = "[Public Theater] Home", GroupName=Constants.ContentGroupNames.ContentGroupName)]
    public class HomePageData : PublicBasePageData
    {
        #region content
        [BackingType(typeof(Properties.HeroImages))]
        [Editable(true)]
        [Display(GroupName = SystemTabNames.Content, Name = "Hero Images slider")]
        public virtual string HeroImages { get; set; }
   
        [Display(GroupName = SystemTabNames.Content, Name = "Block List")]
        public virtual ContentArea BlockList { get; set; }

        #endregion

        #region Site Global Settings

        [Display(GroupName = Constants.TabNames.SiteConfiguration, Name = "Open Table ID")]
        public virtual string OpenTableID { get; set; }

        [Display(GroupName = Constants.TabNames.SiteConfiguration, Name = "Site Footer")]
        public virtual XhtmlString SiteFooter { get; set; }

        [Display(GroupName = Constants.TabNames.SiteConfiguration, Name = "Static Media Tags")]
        public virtual XhtmlString MediaTags { get; set; }

        [UIHint(UIHint.Image)]
        [Display(GroupName = Constants.TabNames.SiteConfiguration, Name = "Global Hero Image")]
        public virtual Url GlobalHeroImage{ get; set; }

        [Display(GroupName = Constants.TabNames.SiteConfiguration, Name = "Sub Navigations")]
        public virtual ContentArea SubNavigations{ get; set; }

        [Display(GroupName = Constants.TabNames.SiteConfiguration, Name = "Search Page")]
        public virtual PageReference SearchResultPage { get; set; }

        [Display(GroupName = Constants.TabNames.SiteConfiguration, Name = "Search Engine Key", Order = 0)]
        public virtual string SearchEngineKey { get; set; }
        #endregion

        #region Methods

        List<Properties.HeroImage> _heroImages;
        public List<Properties.HeroImage> GetHeroImages()
        {
            if (_heroImages == null)
            {
                var images = this.Property["HeroImages"] as Properties.HeroImages;

                if (images == null)
                    _heroImages = new List<Properties.HeroImage>();
                else
                    _heroImages =images.HeroImageList;
            }
            return _heroImages;
        }
        #endregion
    }
}
