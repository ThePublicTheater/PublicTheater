using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "da67b920-1cca-437a-96e7-4b7a9cbfa13e", DisplayName = "[Public Theater] Membership Purchase", GroupName = Constants.ContentGroupNames.ContributeGroupName)]
    public class MembershipPurchasePageData : PublicBasePageData
    {
        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Hero Image")]
        public virtual Url HeroImage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Main Body")]
        public virtual XhtmlString MainBody { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Membership Purchase", Order = 12)]
        public virtual Blocks.MembershipPurchaseBlockData MembershipPurchaseBlock { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Fine Print")]
        public virtual XhtmlString FinePrint { get; set; }
    }
}