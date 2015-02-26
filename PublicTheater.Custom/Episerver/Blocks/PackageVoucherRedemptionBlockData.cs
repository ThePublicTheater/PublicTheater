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
    [ContentType(GUID = "42c5bf08-7f98-4797-8515-163bf65d7549", DisplayName = "Package Voucher Redemption Block", GroupName = Constants.ContentGroupNames.UniversalBlocks)]
    public class PackageVoucherRedemptionBlockData : BaseClasses.PublicBaseResponsiveBlockData
    {
        [Display(Name = "RedeemButtonText ", GroupName = SystemTabNames.Content)]
        public virtual string RedeemButtonText { get; set; }

        [Display(Name = "Description", GroupName = SystemTabNames.Content)]
        public virtual XhtmlString Description { get; set; }

        [Display(Name = "WhatsThisContent", GroupName = SystemTabNames.Content)]
        public virtual XhtmlString WhatsThisContentProperty { get; set; }

        [Display(Name = "Error Package Voucher Empty", GroupName = SystemTabNames.Content)]
        public virtual XhtmlString PackageVoucherEmpty { get; set; }

        [Display(Name = "Error Invalid Package Voucher", GroupName = SystemTabNames.Content)]
        public virtual XhtmlString InvalidPackageVoucher { get; set; }
    }

}
