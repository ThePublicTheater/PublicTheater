using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace PublicTheater.Custom.Episerver.Pages
{
    [ContentType(DisplayName = "[Subscription][Public] Package Detail", GUID = "762539cd-b0e7-4201-a9ea-d9e2d0905aae", Description = "Package detail page that can apply a source code.")]
    public class PackageDetailPageData : TheaterTemplate.Shared.EPiServerPageTypes.PackageDetailPageType
    {
        [Display(GroupName = SystemTabNames.Content, Name = "Source Code", Order = 0)]
        public virtual string SourceCode { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Performance Selection Heading", Order = 0)]
        public virtual XhtmlString PerformanceHeading { get; set; }
        
        [Display(GroupName = SystemTabNames.Content, Name = "Package Voucher Page")]
        public virtual PageReference PackageVoucherPage { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Package Voucher PaymentMethodId")]
        public virtual int PackageVoucherPaymentMethodId { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Package Voucher Unit Price")]
        public virtual double PackageVoucherUnitPrice { get; set; }

        [Display(GroupName = SystemTabNames.Content, Name = "Show Purchase Link")]
        public virtual XhtmlString ShowPurchaseLink { get; set; }

        [Display(GroupName = "UTR Symposium", Name = "Not Logged In Message")]
        public virtual XhtmlString notLoggedIn { get; set; }

        [Display(GroupName = "UTR Symposium", Name = "Not Invited Message")]
        public virtual XhtmlString notInvited { get; set; }

        [Display(GroupName = "UTR Symposium", Name = "Already Purchased Message")]
        public virtual XhtmlString alreadyPurchased { get; set; }

        [Display(GroupName = "UTR Symposium", Name = "Perf ID to Auto-Select")]
        public virtual int autoSelectId{ get; set; }

        [Display(GroupName = "UTR Symposium", Name = "Max Quantity")]
        public virtual int maxQty { get; set; }

        //[Display(GroupName = "UTR Symposium", Name = "Hide on Package List Page")]
        //public virtual bool hideOnPackageListPage { get; set; }



        public virtual bool PackageVoucherEnabled
        {
            get { return PackageVoucherPage != null; }
        }

        /// <summary>
        /// Get Pakcage Voucher link
        /// </summary>
        /// <returns></returns>
        public virtual string GetPackageVoucherLink()
        {
            if (PackageVoucherEnabled == false)
                return string.Empty;

            if (PackageVoucherUnitPrice <= 0)
            {
                throw new ApplicationException("Invalid unit price for this package");
            }

            if (PackageVoucherPaymentMethodId<= 0)
            {
                throw new ApplicationException("Invalid payment method id for this package");
            }

            return string.Format("{0}?UnitPrice={1}&PaymentMethodId={2}",
                Utility.Utility.GetFriendlyUrl(PackageVoucherPage), PackageVoucherUnitPrice,
                PackageVoucherPaymentMethodId);
        }
    }
}