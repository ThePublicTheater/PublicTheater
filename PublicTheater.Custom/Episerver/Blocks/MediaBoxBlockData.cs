using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using PublicTheater.Custom.Episerver.Properties;
using EPiServer.Core;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(GUID = "a8c01a8a-b131-450d-bf02-d53f15608e35", DisplayName = "Media Box Block", GroupName = Constants.ContentGroupNames.MediaGroupName)]
    public class MediaBoxBlockData : BaseClasses.PublicBaseBlockData
    {
        
        [Display(GroupName = SystemTabNames.Content, Name = "Media Items")]
        [BackingType(typeof(MediaBoxItems))]
        public virtual string MediaItems { get; set; }

        public virtual List<MediaBoxItem> GetMediaItems()
        {
            var propertyValue = Property["MediaItems"] as MediaBoxItems;
            return propertyValue != null ? propertyValue.MediaBoxItemList : new List<MediaBoxItem>();
        }


    }

}
