using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(DisplayName = "InstagramBlockData", GUID = "cabb9fa2-3c30-4452-a404-7faeb76e27e3", Description = "", GroupName = Constants.ContentGroupNames.MediaGroupName)]
    public class InstagramBlockData : BaseClasses.PublicBaseResponsiveBlockData
    {
        [Display(Name = "UserName", GroupName = SystemTabNames.Content)]
        public virtual string UserName { get; set; }
    }
}