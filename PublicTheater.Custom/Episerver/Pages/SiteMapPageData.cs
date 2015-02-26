using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "225b0b69-2f08-4ef3-8e7f-f8b5ba83e6b3", DisplayName = "[Public Theater] SiteMap", GroupName=Constants.ContentGroupNames.ContentGroupName)]
    public class SiteMapPageData : PublicBasePageData
    {
        [CultureSpecific]
        [Editable(true)]
        [Display(GroupName = SystemTabNames.Content, Name = "Root Page", Order = 11)]
        public virtual PageReference RootPage { get; set; }
    }
}