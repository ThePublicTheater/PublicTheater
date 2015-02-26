using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(GUID = "f7986ba2-aaa1-41a0-ba31-4a3d8379bb81", DisplayName = "Social Media Block", GroupName = Constants.ContentGroupNames.UniversalBlocks)]
    public class SocialMediaBlockData : BaseClasses.PublicBaseResponsiveBlockData
    {
        [Display(Name = "TwitterUserNames", GroupName = SystemTabNames.Content)]
        public virtual string TwitterUserNames { get; set; }

        [Display(Name = "SocialLinks", GroupName = SystemTabNames.Content)]
        public virtual XhtmlString SocialLinks { get; set; }
    }

}
