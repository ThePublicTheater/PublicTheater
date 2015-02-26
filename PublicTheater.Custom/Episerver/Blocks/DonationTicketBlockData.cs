using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using TheaterTemplate.Shared.EpiServerProperties;

namespace PublicTheater.Custom.Episerver.Blocks
{
    [ContentType(GUID = "2a85c402-4463-4fd9-97da-e8d566dcf34d", DisplayName = "Donation Ticket Block", GroupName = Constants.ContentGroupNames.UniversalBlocks)]
    public class DonationTicketBlockData : BaseClasses.PublicBaseBlockData
    {
        [Display(Name = "Default Fund", GroupName = SystemTabNames.Content, Description = "")]
        [BackingType(typeof(DonationFundItem))]
        public virtual string Funds { get; set; }

        [Display(Name = "Header", GroupName = SystemTabNames.Content, Description = "")]
        public virtual string Header { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Main Body", Order = 0)]
        public virtual XhtmlString MainBody { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Max Quantity", Order = 0)]
        public virtual int MaxQty { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Sold Out Body", Order = 0)]
        public virtual XhtmlString SoldOutHtml { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Price", Order = 0)]
        public virtual int Price { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Sold Out", Order = 0)]
        public virtual bool SoldOut { get; set; }


        public DonationFundItem DefaultDonationFund
        {
            get
            {
                return Property["Funds"] as DonationFundItem;
            }
        }


    }
}
