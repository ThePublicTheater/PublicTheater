using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "cbcc8af0-ba14-4979-a2f6-1ffe28a2e3b1", DisplayName = "[Public Theater] Press Item", AvailableInEditMode = true, GroupName = Constants.ContentGroupNames.MediaGroupName)]
    public class PressItemPageData : PublicBasePageData
    {
        [Display(GroupName = SystemTabNames.Content, Name = "Release Date")]
        public virtual DateTime? ReleaseDate { get; set; }

        [UIHint(UIHint.Document)]
        [Display(GroupName = SystemTabNames.Content, Name = "Release Document")]
        public virtual Url ReleaseDocument { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Venue")]
        public virtual string VenueName { get; set; }
        
        public virtual string FormattedReleaseDate
        {
            get
            {
                return ReleaseDate.HasValue ? ReleaseDate.Value.ToShortDateString() : string.Empty;
            }
        }
    }
}
