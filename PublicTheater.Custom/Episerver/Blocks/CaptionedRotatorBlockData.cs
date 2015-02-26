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
    [ContentType(GUID = "5e677ffa-fad4-4bdf-a554-8b0966404c2f", DisplayName = "Captioned Rotator Block", GroupName = Constants.ContentGroupNames.UniversalBlocks)]
    public class CaptionedRotatorBlockData : BaseClasses.PublicBaseResponsiveBlockData
    {
        [Display(GroupName = SystemTabNames.Content, Name = "Title")]
        public virtual string RotatorTitle { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Rotator Items")]
        [BackingType(typeof(CaptionedImageLinks))]
        public virtual string RotatorItems { get; set; }

        public virtual List<CaptionedImageLink> GetSubHomeRotatorItems()
        {
            var imageLinks = Property["RotatorItems"] as CaptionedImageLinks;

            if (imageLinks != null)
                return imageLinks.CaptionedImageLinkList;

            return new List<CaptionedImageLink>();
        }
    }
}
