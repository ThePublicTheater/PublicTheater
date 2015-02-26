using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "1e412d93-41e6-4213-ab46-fd6f085efdb0", DisplayName = "[Public Theater] Visit Us Page", GroupName=Constants.ContentGroupNames.ContentGroupName)]
    public class VisitPageData : PublicBasePageData
    {
        #region Content Area

        [Display(GroupName = SystemTabNames.Content, Name = "Location List")]
        public virtual ContentArea LocationList { get; set; }

        #endregion

        #region properties
        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Hero Image")]
        public virtual Url HeroImage { get; set; }

        [Display(Name = "Secondary Header", GroupName = SystemTabNames.Content, Order = 10)]
        public virtual string Heading { get; set; }

        [CultureSpecific]
        [Editable(true)]
        [Display(GroupName = SystemTabNames.Content, Name = "Main Body", Order = 11)]
        public virtual XhtmlString MainBody { get; set; }

       
        #endregion
    }
}