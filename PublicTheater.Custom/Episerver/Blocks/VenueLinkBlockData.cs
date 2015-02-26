using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(GUID = "1a4e932a-fc27-4a78-b5f4-d021c542c667", DisplayName = "Venue Links Block", GroupName = Constants.ContentGroupNames.HomePageBlocks)]
    public class VenueLinkBlockData : BaseClasses.PublicBaseResponsiveBlockData
    {
        [Display(GroupName = SystemTabNames.Content, Name = "Heading")]
        public virtual string Heading { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Venue Links")]
        [BackingType(typeof(VenueLinks))]
        public virtual string VenueLinks { get; set; }

        public virtual List<VenueLink> GetVenueLinks()
        {
            var imageLinks = Property["VenueLinks"] as VenueLinks;
            return imageLinks != null ? imageLinks.VenueLinkList : new List<VenueLink>();
        }
    }

}
