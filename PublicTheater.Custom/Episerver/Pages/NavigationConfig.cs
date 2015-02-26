using System;
using System.ComponentModel.DataAnnotations;
using Adage.EpiServer.Library.PageTypes.Templates;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using PublicTheater.Custom.Episerver.BaseClasses;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(
        GUID = "ecd7d9b0-deb7-4d39-917a-b4874e19f08a", 
        DisplayName = "[Public Theater] Navigation Config", 
        GroupName = Constants.ContentGroupNames.ConfigurationGroupName,
        AvailableInEditMode = true)]
    public class NavigationConfig : AdageBase
    {
        [BackingType(typeof(PropertyNumber))]
        [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<Enums.SiteTheme>))]
        [Display(GroupName = SystemTabNames.Content, Name = "Site Theme")]
        public virtual Enums.SiteTheme SiteTheme { get; set; }

        [UIHint(UIHint.Image)]
        [Display(GroupName =SystemTabNames.Content , Name = "Logo Image")]
        public virtual Url LogoImage { get; set; }

        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Mobile Logo Image")]
        public virtual Url MobileLogoImage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "SubHome Page")]
        public virtual PageReference SubHomePage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Menu Items")]
        public virtual ContentArea MenuItems { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "CSS Overide", Order = 30)]
        [UIHint(UIHint.Textarea)]
        public virtual String cssOveride { get; set; }
    }
}
