using System.ComponentModel.DataAnnotations;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "fc315bdb-5118-4b93-bbcd-1fc3573b49d6", DisplayName = "[Public Theater] Library")]
    public class LibraryPageData : PublicBasePageData
    {
        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Hero Image")]
        public virtual Url HeroImage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "About Location")]
        public virtual XhtmlString AboutLocation { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "About Hours")]
        public virtual XhtmlString AboutHours { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "About Contact")]
        public virtual XhtmlString AboutContact { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "About Connect")]
        public virtual XhtmlString AboutConnect { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "About Information")]
        public virtual XhtmlString AboutInformation { get; set; }

        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "About Image")]
        public virtual XhtmlString AboutImage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Event Information")]
        public virtual XhtmlString EventInformation { get; set; }

        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Event Image")]
        public virtual XhtmlString EventImage { get; set; }


        [Display(GroupName = SystemTabNames.Content, Name = "Menu Items")]
        public virtual Blocks.AccordionBlockData DinnerItems { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Drink Items")]
        public virtual Blocks.AccordionBlockData DrinkItems { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Press Content")]
        public virtual XhtmlString PressContent { get; set; }
    }
}