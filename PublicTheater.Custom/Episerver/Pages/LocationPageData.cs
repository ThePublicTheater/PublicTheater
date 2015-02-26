using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "f17489da-7041-4cd8-abcf-2a5bbb9dc1db", DisplayName = "[Public Theater] Section Landing (Location)", GroupName=Constants.ContentGroupNames.ContentGroupName)]
    public class LocationPageData : PublicBasePageData
    {
        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Hero Image")]
        public virtual Url HeroImage { get; set; }

        [Display(Name = "Location", GroupName = SystemTabNames.Content, Order = 10)]
        public virtual string Location { get; set; }

        [Display(Name = "Address", GroupName = SystemTabNames.Content, Order = 11)]
        public virtual XhtmlString Address { get; set; }

        [Display(Name = "Direction Link", GroupName = SystemTabNames.Content, Order = 12)]
        public virtual Url DirectionLink { get; set; }

        [Display(Name = "Map Link", GroupName = SystemTabNames.Content, Order = 13)]
        public virtual Url MapLink { get; set; }

        [Display(Name = "Map Image - Large", GroupName = SystemTabNames.Content, Order = 14)]
        [UIHint(UIHint.Image)]
        public virtual Url MapImageLarge { get; set; }

        [Display(Name = "Map Image - Small", GroupName = SystemTabNames.Content, Order = 15)]
        [UIHint(UIHint.Image)]
        public virtual Url MapImageSmall { get; set; }

        [CultureSpecific]
        [Editable(true)]
        [Display(GroupName = SystemTabNames.Content, Name = "Main Body", Order = 16)]
        public virtual XhtmlString MainBody { get; set; }

    }
}