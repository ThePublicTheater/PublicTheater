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
    [ContentType(GUID = "07ebc16c-5603-4b90-827b-36fe7a733695", DisplayName = "Switch To Block", GroupName = Constants.ContentGroupNames.UniversalBlocks)]
    public class SwitchToBlockData : BaseClasses.PublicBaseResponsiveBlockData
    {


        [Display(Name = "Image Url", GroupName = SystemTabNames.Content)]
        [UIHint(UIHint.Image)]
        public virtual Url ImageUrl { get; set; }


        [Display(Name = "Link", GroupName = SystemTabNames.Content)]
        public virtual PageReference LinkUrl { get; set; }


    }

}
