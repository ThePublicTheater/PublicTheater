using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using TheaterTemplate.Shared.EPiServerPageTypes;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "dab1803b-556f-4198-8a9d-56b1a544ce79", DisplayName = "[Subscription][Public] Package List", Description = "Page that contains a list of packages", GroupName = TheaterPageTypeGroupNames.SubscriptionGroupName)]
    public class PackageListPageData : PackageListPageType 
    {
        [Display(GroupName = SystemTabNames.Content, Name = "Package Voucher Redemption Block", Order = 12)]
        public virtual Blocks.PackageVoucherRedemptionBlockData PackageVoucherRedemptionBlock { get; set; }
    }
    
}
