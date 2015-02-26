using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;
using TheaterTemplate.Shared.EPiServerPageTypes;
using EPiServer.Shell.ObjectEditing;
using PublicTheater.Custom.Episerver.Properties;
using System;
using System.Web;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "3157d8e5-599a-44ba-bc2e-2dab4555aef8", DisplayName = "[Public Theater] Calendar", GroupName=Constants.ContentGroupNames.SeasonGroupName)]
    public class CalendarPageData : CalendarConfigPageType
    {
        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Hero Image")]
        public virtual Url HeroImage { get; set; }

        #region site theme
        [BackingType(typeof(PropertyNumber))]
        [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<Enums.SiteTheme>))]
        [Display(GroupName = SystemTabNames.Settings, Name = "Site Theme")]
        public virtual Enums.SiteTheme SiteTheme { get; set; }

        #endregion

        #region SEO 
        [Display(Name = "Description", Description = "Description of the page", GroupName = "SEO")]
        [UIHint("textarea")]
        public virtual string SEODescription { get; set; }
        [UIHint("textarea")]
        [Display(Name = "SEOKeywords", Description = "Keywords for the page", GroupName = "SEO")]
        public virtual string SEOKeywords { get; set; }
        [UIHint("SEORobotsSource")]
        [Display(Name = "Robots", Description = "How web robots should handle the page", GroupName = "SEO")]
        public virtual string SEORobots { get; set; }
        [Display(Name = "SEO Title", Description = "The title of the pages", GroupName = "SEO")]
        [BackingType(typeof(PropertyString))]
        public virtual string SEOTitle { get; set; }
        #endregion
    }
}