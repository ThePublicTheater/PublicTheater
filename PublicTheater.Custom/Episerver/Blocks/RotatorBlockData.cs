using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(GUID = "b8c01a8a-b131-450d-bf02-d53f15608e35", DisplayName = "Rotator Block", GroupName = Constants.ContentGroupNames.UniversalBlocks)]
    public class RotatorBlockData : BaseClasses.PublicBaseResponsiveBlockData
    {
        [Display(GroupName = SystemTabNames.Content, Name = "Title")]
        public virtual string RotatorTitle { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Rotator Items")]
        [BackingType(typeof(ImageLinks))]
        public virtual string RotatorItems { get; set; }

        public virtual List<ImageLink> GetImageLinks()
        {
            var imageLinks = Property["RotatorItems"] as ImageLinks;
            return imageLinks != null ? imageLinks.ImageLinkList : new List<ImageLink>();
        }
    }

}
