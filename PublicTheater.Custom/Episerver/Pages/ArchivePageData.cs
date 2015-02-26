using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "863d6cf9-8f67-4b63-be0f-fee63aaa32d7", DisplayName = "[Public Theater] Archive", GroupName=Constants.ContentGroupNames.HistoryGroupName)]
    public class ArchivePageData : PublicBasePageData
    {
        #region Properties
        [Display(GroupName = SystemTabNames.Content, Name = "Archive Post Production Hours ", 
            Description = "Archive play detail after certain period pf time after the show stopped on sale.")]
        public virtual int ArchivePostProductionHours { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Archive Start Year")]
        public virtual int ArchiveStartYear { get; set; }

        [UIHint(UIHint.Textarea)]
        [Display(GroupName = SystemTabNames.Content, Name = "Comma separated Genre List")]
        public virtual string GenraList { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Max Count - Public Theater")]
        public virtual int MaxCountPublicTheater { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Max Count - Joe's Pub")]
        public virtual int MaxCountJoesPub { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Max Count - Shakespeare in the Park")]
        public virtual int MaxCountShakespeare { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Max Count - Under the Radar Festival")]
        public virtual int MaxCountUndertheRadar { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Max Count - Public Forum")]
        public virtual int MaxCountPublicForum{ get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Max Count - Emerging Writers Group")]
        public virtual int MaxCountEmergingWritersGroup { get; set; }
        #endregion
    }
}