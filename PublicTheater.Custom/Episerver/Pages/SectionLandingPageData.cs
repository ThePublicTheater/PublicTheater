using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "6ca187a9-5b1d-4f66-aacf-5d1ead1a3cfd", DisplayName = "[Public Theater] Section Landing (Blocks)", GroupName=Constants.ContentGroupNames.ContentGroupName)]
    public class SectionLandingPageData : PublicBasePageData
    {
        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Hero Image")]
        public virtual Url HeroImage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Block List")]
        public virtual ContentArea BlockList { get; set; }
    }
}