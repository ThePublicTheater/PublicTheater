using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiServer.Core;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Custom.CoreFeatureSet.EpiServerConfig
{
    public class PublicCartContentConfig : CartContentConfig
    {
        /// <summary>
        /// Text displayed above the Package Voucher area
        /// </summary>
        public virtual string PackageVoucherHeader
        {
            get;
            protected set;
        }

        /// <summary>
        /// Text displayed above the Package Voucher total area
        /// </summary>
        public virtual string PackageVoucherTotalHeader
        {
            get;
            protected set;
        }

        /// <summary>
        /// Text displayed below the Package Voucher text in the cart
        /// </summary>
        public virtual string PackageVoucherText
        {
            get;
            protected set;
        }

        protected override void FillFromLock(PageData cartContentPageData)
        {
            base.FillFromLock(cartContentPageData);
            PackageVoucherHeader = GetStringValueFromVisitorGroup(cartContentPageData, "PackageVoucherHeader", true);
            PackageVoucherTotalHeader = GetStringValueFromVisitorGroup(cartContentPageData, "PackageVoucherTotalHeader", true);
            PackageVoucherText = GetStringValueFromVisitorGroup(cartContentPageData, "PackageVoucherText", true);
        }
    }
}
