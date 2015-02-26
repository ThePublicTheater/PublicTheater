using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPiServer;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(GUID = "07ebc66c-5603-4b90-826b-36fe7a733695", DisplayName = "Image Link Block", GroupName = Constants.ContentGroupNames.UniversalBlocks)]
    public class ImageLinkBlockData : BaseClasses.PublicBaseResponsiveBlockData
    {
        [Display(Name = "Title", GroupName = SystemTabNames.Content)]
        public virtual string ImageTitle { get; set; }

        [Display(Name = "Image Url", GroupName = SystemTabNames.Content)]
        [UIHint(UIHint.Image)]
        public virtual Url ImageUrl { get; set; }

        [Display(Name = "Link", GroupName = SystemTabNames.Content)]
        public virtual Url LinkUrl { get; set; }
    }

}
