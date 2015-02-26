using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "d9a8b2a1-f5ba-4c7c-8a14-92ccdb98d426", DisplayName = "[Public Theater] History", GroupName=Constants.ContentGroupNames.HistoryGroupName)]
    public class HistoryPageData : PublicBasePageData
    {
        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Hero Image")]
        public virtual Url HeroImage { get; set; }
    }
}