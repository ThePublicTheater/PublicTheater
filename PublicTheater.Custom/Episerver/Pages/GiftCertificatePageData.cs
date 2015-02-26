using EPiServer.Core;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;
using EPiServer.Shell.ObjectEditing;
using PublicTheater.Custom.Episerver.Properties;
using TheaterTemplate.Shared.EPiServerPageTypes;
using PublicTheater.Custom.Episerver.Blocks;
using Adage.Theater.RelationshipManager.PlugIn;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "7ee2615e-fa5b-4281-9f8b-3d3859c22fd5", DisplayName = "[Public Theater] Gift Certificates", GroupName = Constants.ContentGroupNames.CheckoutGroupName)]
    public class GiftCertificatePageData : TheaterTemplate.Shared.EPiServerPageTypes.GiftCertificatePageType
    {
        [BackingType(typeof(PropertyNumber))]
        [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<Enums.SiteTheme>))]
        [Display(GroupName = SystemTabNames.Settings, Name = "Site Theme")]
        public virtual Enums.SiteTheme SiteTheme { get; set; }

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
