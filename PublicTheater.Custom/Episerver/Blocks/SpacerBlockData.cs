using System;
using System.ComponentModel.DataAnnotations;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(GUID = "81ad6e25-44c9-43a8-aaa6-cb39f7629a9a",DisplayName = "Spacer Block", GroupName = Constants.ContentGroupNames.UniversalBlocks)]
    public class SpacerBlockData : BaseClasses.PublicBaseResponsiveBlockData
    {
        [Display(Name = "Show Large", GroupName = SystemTabNames.Content)]
        public virtual Boolean ShowLarge { get; set; }

        [Display(Name = "Show Medium", GroupName = SystemTabNames.Content)]
        public virtual Boolean ShowMedium { get; set; }

        [Display(Name = "Show Small", GroupName = SystemTabNames.Content)]
        public virtual Boolean ShowSmall { get; set; }

        [Display(Name = "Image Url", GroupName = SystemTabNames.Content)]
        [UIHint(UIHint.Image)]
        public virtual Url ImageUrl { get; set; }
    }
}