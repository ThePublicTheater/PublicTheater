using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using TheaterTemplate.Shared.EPiServerPageTypes;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(GUID = "cbf12ba0-0b12-47d7-8cd6-5633fcba8c47", DisplayName = "Upcoming Performances", GroupName = Constants.ContentGroupNames.UniversalBlocks)]
    public class UpcomingPerformancesBlockData : BaseClasses.PublicBaseResponsiveBlockData
    {
        [Display(GroupName = SystemTabNames.Content, Name = "Header")]
        public virtual string Header { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Number of days to show", Description="This will default to a week.")]
        public virtual int NumberOfDaysToShow { get; set; }

    }
}
