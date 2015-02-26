using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using PublicTheater.Custom.Episerver.Properties;
using TheaterTemplate.Shared.EpiServerProperties;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(GUID = "f9fe00e2-db83-4cda-83d0-0188e79b570d", DisplayName = "Membership Purchase Block", GroupName = Constants.ContentGroupNames.UniversalBlocks)]
    public class MembershipPurchaseBlockData  : BaseClasses.PublicBaseResponsiveBlockData
    {
        [Display(Name = "Add to Cart Text", GroupName = SystemTabNames.Content)]
        public virtual string AddToCartText { get; set; }

        [Display(Name = "Description", GroupName = SystemTabNames.Content)]
        public virtual XhtmlString Description { get; set; }

        [Display(Name = "Default Fund", GroupName = SystemTabNames.Content, Description = "")]
        [BackingType(typeof(DonationFundItem))]
        public virtual string Funds { get; set; }

        public DonationFundItem DefaultDonationFund
        {
            get
            {
                return Property["Funds"] as DonationFundItem;
            }
        }
    }

}
