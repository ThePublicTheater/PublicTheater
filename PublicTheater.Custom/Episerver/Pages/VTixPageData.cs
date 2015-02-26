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
    [ContentType(GUID = "3157d8e5-599a-44ca-bc2e-2daf4555aea9", DisplayName = "[Public Theater] VTix Page", GroupName = Constants.ContentGroupNames.ContentGroupName)]
    public class VTixPageData : PublicBasePageData
    {
        #region Properties
        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Hero Image")]
        public virtual Url HeroImage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "State 01: No line Today", Order = 3)]
        public virtual XhtmlString State01 { get; set; }
        [Display(GroupName = SystemTabNames.Content, Name = "State 02: Not Eligible", Order = 4)]
        public virtual XhtmlString State02 { get; set; }
        [Display(GroupName = SystemTabNames.Content, Name = "State 03: Already Entered", Order = 5)]
        public virtual XhtmlString State03 { get; set; }
        [Display(GroupName = SystemTabNames.Content, Name = "State 04: Entry Step 1", Order = 6)]
        public virtual XhtmlString Entry1 { get; set; }
        [Display(GroupName = SystemTabNames.Content, Name = "State 04: Entry Step 2", Order = 7)]
        public virtual XhtmlString Entry2 { get; set; }
        [Display(GroupName = SystemTabNames.Content, Name = "State 04: Entry Step 3", Order = 8)]
        public virtual XhtmlString Entry3 { get; set; }
        [Display(GroupName = SystemTabNames.Content, Name = "State 05: Drawing In Progress", Order = 9)]
        public virtual XhtmlString State05 { get; set; }
        [Display(GroupName = SystemTabNames.Content, Name = "State 06: Winner PreShow", Order = 10)]
        public virtual XhtmlString State06 { get; set; }
        [Display(GroupName = SystemTabNames.Content, Name = "State 07: Lose Preshow", Order = 11)]
        public virtual XhtmlString State07 { get; set; }
        [Display(GroupName = SystemTabNames.Content, Name = "State 08: No Valid Entry Today", Order = 12)]
        public virtual XhtmlString State08 { get; set; }
        [Display(GroupName = SystemTabNames.Content, Name = "State 09: Show Started", Order = 13)]
        public virtual XhtmlString State09 { get; set; }




        [Display(GroupName = SystemTabNames.Content, Name = "Right Column Call Outs")]
        public virtual ContentArea CallOuts { get; set; }
        #endregion



    }
}
