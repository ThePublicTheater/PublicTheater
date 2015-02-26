using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(GUID = "1991a471-9d5c-4f29-97eb-ebc2fe918e22", DisplayName = "Captioned Image Link Block", GroupName = Constants.ContentGroupNames.UniversalBlocks)]
    public class CaptionedImageLinkBlockData : BaseClasses.PublicBaseResponsiveBlockData
    {
        [Display(GroupName = SystemTabNames.Content, Name = "Captioned Image Link Block")]
        [BackingType(typeof(CaptionedImageLink))]
        public virtual string CaptionedImageLink { get; set; }


        public virtual CaptionedImageLink CaptionedImageLinkTyped
        {
            get { return this.Property["CaptionedImageLink"] as CaptionedImageLink; }
        }

    }
}
