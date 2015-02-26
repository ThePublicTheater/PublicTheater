using System.ComponentModel.DataAnnotations;
using Adage.Theater.RelationshipManager.PlugIn;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.UI.Admin;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;
using PublicTheater.Custom.Episerver.Blocks;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "6c27f9a8-a115-4e18-8eb5-8e29916f7eda", DisplayName = "[Public Theater] Leadership")]
    public class LeadershipPageData : PublicBasePageData
    {
        #region Properties

        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Hero Image")]
        public virtual Url HeroImage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Group 1")]
        public virtual ArtistGroupBlockData Group1 { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Group 2")]
        public virtual ArtistGroupBlockData Group2 { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Group 3")]
        public virtual ArtistGroupBlockData Group3 { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Group 4")]
        public virtual ArtistGroupBlockData Group4 { get; set; }

        #endregion
    }
}