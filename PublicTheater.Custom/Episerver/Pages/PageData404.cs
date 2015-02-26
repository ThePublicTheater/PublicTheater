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
    [ContentType(GUID = "063408fb-35c8-4679-8c17-1fa075fec6f2", DisplayName = "[Public Theater] 404 Page", GroupName=Constants.ContentGroupNames.ContentGroupName)]
    public class PageData404 : PublicBasePageData
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
        #endregion


        
    }
}
