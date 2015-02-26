using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Adage.Theater.RelationshipManager.PlugIn;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Core;
using EPiServer.UI.Admin;
using EPiServer.Web;
using EPiServer;
using PublicTheater.Custom.Episerver.BaseClasses;
using PublicTheater.Custom.Episerver.Blocks;
using PublicTheater.Custom.Episerver.Properties;
using TheaterTemplate.Shared.EpiServerProperties;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "af1f66bf-3671-4069-8932-3c8faa16926d", DisplayName = "[Public Theater] Gala Transaction")]
    public class GalaTransactionPageData : PublicBasePageData
    {
        #region Properties

        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Banner Image")]
        public virtual Url BannerImage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Banner Text")]
        public virtual XhtmlString BannerText { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Side Heading")]
        public virtual XhtmlString SideHeading { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Gala Levels")]
        [BackingType(typeof(Properties.GalaLevelGroups))]
        public virtual string GalaLevels { get; set; }

        [Display(Name = "Additional Donation Fund", GroupName = SystemTabNames.Content, Description = "")]
        [BackingType(typeof(DonationFundItem))]
        public virtual string AdditionalDonationFund { get; set; }


        [Display(GroupName = SystemTabNames.Content, Name = "Invalid Level Error")]
        public virtual XhtmlString InvalidLevelError { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Acknowledgement Notes")]
        public virtual string AcknowledgementNotes{ get; set; }

        #endregion


        public DonationFundItem AdditionalGalaDonationFund
        {
            get
            {
                return Property["AdditionalDonationFund"] as DonationFundItem;
            }
        }

        public List<Properties.GalaLevelGroup> GetGalaLevelGroups()
        {
            var prop = this.Property["GalaLevels"] as Properties.GalaLevelGroups;

            return prop == null ? new List<GalaLevelGroup>() : prop.GalaLevelGroupList;
        }

        
    }
}