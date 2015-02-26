using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;
using PublicTheater.Custom.Episerver.Blocks;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "3035b73b-1fae-409f-a785-0e85292acccd", DisplayName = "[Public Theater] Sub-Home Page", GroupName=Constants.ContentGroupNames.ContentGroupName)]
    public class SubHomePageData : PublicBasePageData
    {
        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Hero Image", Order = 10)]
        public virtual Url HeroImage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Upcoming Performances", Order = 12)]
        public virtual UpcomingPerformancesBlockData UpcomingPerformances { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "What's On", Order = 11)]
        public virtual CaptionedRotatorBlockData CaptionedRotatorItems { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Right Side Block List", Order = 13)]
        public virtual ContentArea BlockList { get; set; }
    }
}