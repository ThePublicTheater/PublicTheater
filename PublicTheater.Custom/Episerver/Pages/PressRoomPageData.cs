using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "0df8f456-ab25-486f-9a55-659721ea6da6", DisplayName = "[Public Theater] Press Room", GroupName = Constants.ContentGroupNames.MediaGroupName)]
    public class PressRoomPageData : PublicBasePageData
    {
        [Display(GroupName = SystemTabNames.Content, Name = "Heading", Order = 0)]
        public virtual string Heading { get; set; }

        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Hero Image", Order = 1)]
        public virtual Url HeroImage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Contact", Order = 2)]
        public virtual XhtmlString Contact { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Press Images Title", Order = 3)]
        public virtual string PressImagesTitle { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Press Images Box", Order = 4)]
        public virtual XhtmlString PressImagesDesc { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Password", Order = 5)]
        public virtual string Password { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Password Invalid Message", Order = 6)]
        public virtual XhtmlString PasswordInvalidMessage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Press Photo", Order = 7)]
        public virtual PageReference PressPhotoPage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "More Info", Order = 8)]
        public virtual ContentArea MoreInfo { get; set; }
    }
}