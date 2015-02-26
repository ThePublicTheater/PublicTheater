using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiServer.Core;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Custom.CoreFeatureSet.EpiServerConfig
{
    public class PublicPackageDetailConfig : PackageDetailConfig
    {
        /// <summary>
        /// The SourceCode to use for the current package detail
        /// </summary>
        public virtual string SourceCode { get; protected set; }

        /// <summary>
        /// The PerformanceHeading to use for the performance selection page
        /// </summary>
        public virtual string PerformanceHeading { get; protected set; }

        /// <summary>
        /// Package Voucher Payment Method Id
        /// </summary>
        public virtual int PackageVoucherPaymentMethodId { get; protected set; }

        /// <summary>
        /// Package Voucher Unit Price 
        /// </summary>
        public virtual double PackageVoucherUnitPrice { get; protected set; }

        /// <summary>
        /// Package Voucher Enabled
        /// </summary>
        public virtual bool PackageVoucherEnabled { get; protected set; }


        /// <summary>
        /// Package Voucher Link 
        /// </summary>
        public virtual string PackageVoucherLink { get; protected set; }

        public virtual string NotLoggedIn { get; protected set; }
        public virtual string NotInvited { get; protected set; }
        public virtual string AlreadyPurchased { get; protected set; }
        public virtual int AutoSelectPerf { get; protected set; }

        /// <summary>
        /// Whether or not to show the purchase link
        /// </summary>
        public virtual string ShowPurchaseLink { get; protected set; }

        public virtual int MaxQty { get; protected set; }

        //public virtual bool hideOnPackageListPage { get; protected set; }

        /// <summary>
        /// Fill config from package details page
        /// </summary>
        /// <param name="packageDetailPage"></param>
        protected override void FillFromLock(PageData packageDetailPage)
        {
            base.FillFromLock(packageDetailPage);
            var pageData = packageDetailPage as Episerver.Pages.PackageDetailPageData;
            if (pageData != null)
            {
                SourceCode = pageData.SourceCode;
                try
                {
                PerformanceHeading = pageData.PerformanceHeading.ToHtmlString();
                }
                catch (Exception e)
                {
                }
                PackageVoucherEnabled = pageData.PackageVoucherEnabled;
                PackageVoucherLink = pageData.GetPackageVoucherLink();
                PackageVoucherPaymentMethodId = pageData.PackageVoucherPaymentMethodId;
                PackageVoucherUnitPrice = pageData.PackageVoucherUnitPrice;
                
                if (pageData.autoSelectId > 0)
                    AutoSelectPerf = pageData.autoSelectId;
                //if (pageData.maxQty > 0)
                    MaxQty = pageData.maxQty;

                   // hideOnPackageListPage = pageData.hideOnPackageListPage;
            }
            ShowPurchaseLink = GetStringValueFromVisitorGroup(packageDetailPage, "ShowPurchaseLink", false); 
            NotLoggedIn = GetStringValueFromVisitorGroup(packageDetailPage, "notLoggedIn", false);
            NotInvited = GetStringValueFromVisitorGroup(packageDetailPage, "notInvited", false);
            AlreadyPurchased = GetStringValueFromVisitorGroup(packageDetailPage, "alreadyPurchased", false);

        }
    }
}
