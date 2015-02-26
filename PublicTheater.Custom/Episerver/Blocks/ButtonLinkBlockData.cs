using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(GUID = "13bd1dc6-e923-46ad-b408-21c6c4501fa1", DisplayName = "Button Links Block", GroupName = Constants.ContentGroupNames.UniversalBlocks)]
    public class ButtonLinkBlockData : BaseClasses.PublicBaseResponsiveBlockData
    {
        [Display(GroupName = SystemTabNames.Content, Name = "Heading")]
        public virtual string Heading { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Button Links")]
        [BackingType(typeof(ButtonLinks))]
        public virtual string ButtonLinkItems { get; set; }

        public virtual List<ButtonLink> GetButtonLinks()
        {
            var buttonLinks = Property["ButtonLinkItems"] as ButtonLinks;
            return buttonLinks != null ? buttonLinks.ButtonLinkList : new List<ButtonLink>();
        }
    }

}
