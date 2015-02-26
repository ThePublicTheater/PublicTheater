using System.ComponentModel.DataAnnotations;
using Adage.Theater.RelationshipManager.PlugIn;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.UI.Admin;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "9d0b975a-ec61-4db8-963f-f3fa9d1d4ac2", DisplayName = "[Public Theater] No Tessitura Content Page", GroupName = Constants.ContentGroupNames.ContentGroupName)]
    public class NoTessContentPageData : PublicBasePageData
    {
        #region Properties
        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Hero Image")]
        public virtual Url HeroImage { get; set; }

        //[Display(GroupName = SystemTabNames.Content, Name = "Secondary Header")]
        //public virtual string SecondaryHeader { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Main Body")]
        public virtual XhtmlString MainBody { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Accordion Content", Order = 12)]
        public virtual Blocks.AccordionBlockData AccordionBlock { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Right Column Call Outs")]
        public virtual ContentArea CallOuts { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Under Main Content")]
        public virtual ContentArea Underbody { get; set; }

        #endregion



    }
}
