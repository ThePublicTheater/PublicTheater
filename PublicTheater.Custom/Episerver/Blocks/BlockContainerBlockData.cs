using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(GUID = "a8c01a8a-a131-450d-bf02-d53f15608e35",  DisplayName = "Block Container Block", GroupName = Constants.ContentGroupNames.ContentGroupName)]
    public class BlockContainerBlockData : BaseClasses.PublicBaseBlockData
    {
        [Display(GroupName = SystemTabNames.Content, Name = "Blocks Go Here")]
        public virtual ContentArea Underbody { get; set; }
    }
}
