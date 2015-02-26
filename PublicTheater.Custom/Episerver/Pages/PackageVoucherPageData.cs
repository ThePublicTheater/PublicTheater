using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(GUID = "3524fb29-e351-4099-b1c3-41d2c8464527", DisplayName = "[Subscription][Public] Package Voucher", GroupName = Constants.ContentGroupNames.SubscriptionGroupName)]
    public class PackageVoucherPageData : PublicBasePageData
    {
        #region Properties
        [UIHint(UIHint.Image)]
        [Display(GroupName = SystemTabNames.Content, Name = "Hero Image")]
        public virtual Url HeroImage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Main Body")]
        public virtual XhtmlString MainBody { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Purchase Voucher Error Message")]
        public virtual XhtmlString ErrorMessage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Max Quantity Per Transaction")]
        public virtual int MaxQty { get; set; }


        #endregion
    }
}
