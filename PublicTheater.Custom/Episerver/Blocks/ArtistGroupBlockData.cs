using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(GUID = "bf0db6bc-af0a-4213-84aa-bcd5bbdd5859", DisplayName = "Artist Group Block", GroupName = Constants.ContentGroupNames.UniversalBlocks)]
    public class ArtistGroupBlockData : BaseClasses.PublicBaseResponsiveBlockData
    {
        [Display(GroupName = SystemTabNames.Content, Name = "Artist List")]
        [BackingType(typeof(ArtistGroups))]
        public virtual string ArtistGroups { get; set; }

        public virtual List<ArtistGroup> GetArtistGroups()
        {
            var imageLinks = Property["ArtistGroups"] as ArtistGroups;
            return imageLinks != null ? imageLinks.ArtistGroupList : new List<ArtistGroup>();
        }
    }

}
