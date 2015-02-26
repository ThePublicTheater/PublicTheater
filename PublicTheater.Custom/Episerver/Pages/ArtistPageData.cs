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
    [ContentType(GUID = "a8b125e5-7970-4b4b-bbd2-30f602218835", DisplayName = "[Public Theater] Artist", GroupName=Constants.ContentGroupNames.MediaGroupName)]
    public class ArtistPageData : PublicBasePageData
    {
        #region Properties

        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Thumbnail 54x54")]
        public virtual Url Thumbnail { get; set; }

        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Headshot 97x97")]
        public virtual Url Headshot { get; set; }

        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Picture 300x300")]
        public virtual Url Picture { get; set; }



        [Display(GroupName = SystemTabNames.Content, Name = "Artist Bio")]
        public virtual XhtmlString Bio { get; set; }


        [BackingType(typeof(GenericRelationshipProperty))]
        [Editable(true)]
        [Display(Name = "Performances", Description = "", GroupName = Constants.TabNames.Performances, Order = 10)]
        public virtual string Performances { get; set; }

        #endregion


        #region Methods

        private GenericRelationshipProperty _relatedPerfromances;
        public virtual GenericRelationshipProperty GetRelatedPerfromances()
        {
            return _relatedPerfromances ?? (_relatedPerfromances = this.Property["Performances"] as GenericRelationshipProperty);
        }
        #endregion
    }
}