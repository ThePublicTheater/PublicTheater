using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "79ce8727-30da-400b-881e-4e553d5dd452", DisplayName = "[Public Theater] Sponsors", GroupName=Constants.ContentGroupNames.ContentGroupName)]
    public class SponsorsPageData : PublicBasePageData
    {
        [CultureSpecific]
        [Editable(true)]
        [Display(GroupName = SystemTabNames.Content, Name = "Main Body", Order = 11)]
        public virtual XhtmlString MainBody { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Sponsor List")]
        public virtual ContentArea SponsorList { get; set; }
    }
}