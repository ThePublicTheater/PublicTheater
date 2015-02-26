using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adage.Tessitura;
using EPiServer.Core;
using PublicTheater.Custom.CoreFeatureSet.Common;
using PublicTheater.Custom.Episerver.Utility;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Custom.CoreFeatureSet.EpiServerConfig
{
    public class PublicPackageListConfig : PackageListConfig
    {
        /// <summary>
        /// Get page url for the current package list page id
        /// </summary>
        /// <returns></returns>
        public string GetPackageListPageUrl()
        {
            return Utility.GetFriendlyUrl(new PageReference(PageId));
        }

        /// <summary>
        /// Get enabled package detail config by voucher payment method id
        /// </summary>
        /// <param name="paymentMethodId"></param>
        /// <returns></returns>
        public PublicPackageDetailConfig GetPackageDetailConfigByPaymentMethodId(int paymentMethodId)
        {
            return this.PackageDetailPages.OfType<PublicPackageDetailConfig>()
                .FirstOrDefault(config => 
                    config != null 
                    && config.PackageVoucherPaymentMethodId == paymentMethodId);
        }

        /// <summary>
        /// Get Flex Package List config from config page id
        /// </summary>
        /// <returns></returns>
        public static PublicPackageListConfig GetPublicFlexPackageListConfig()
        {
            var pageId = Config.GetValue("PublicFlexPackageListPageId", 2199);
            return GetPackageList(pageId) as PublicPackageListConfig;
        }
    }
}
