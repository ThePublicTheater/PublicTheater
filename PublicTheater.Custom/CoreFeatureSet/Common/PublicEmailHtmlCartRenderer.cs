using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using EPiServer.Core;
using PublicTheater.Custom.CoreFeatureSet.CartObjects;
using PublicTheater.Custom.CoreFeatureSet.Memberships;
using TheaterTemplate.Shared.Common;

namespace PublicTheater.Custom.CoreFeatureSet.Common
{
    public class PublicEmailHtmlCartRenderer : TheaterSharedEmailHtmlCartRenderer
    {
        /// <summary>
        /// Creates the HTML emailer
        /// </summary>
        public PublicEmailHtmlCartRenderer(PageData CMSEmailPage)
            : base(CMSEmailPage)
        {
        }

        protected override void RenderPerformanceLineHeader(PerformanceLineItem performance, TheaterTemplate.Shared.EpiServerConfig.VenueConfig venueConfig)
        {
            if (performance.Performance.ProductionSeasonId != 26482)
            {
                base.RenderPerformanceLineHeader(performance, venueConfig);
                return;
            }
            sb.Append(GetCmsDecodedPropertyValue("SINGLE_TICKET_WRAPPER_START"));
            string venueName = ResolveVenueName(performance, venueConfig);

            sb.AppendFormat(GetCmsDecodedPropertyValue("SINGLE_TICKET_TITLE"), performance.Performance.Title);
            sb.AppendFormat(GetCmsDecodedPropertyValue("SINGLE_TICKET_DATE"), performance.Performance.PerformanceDate.ToString("D") );
            
            
            

            if (string.IsNullOrWhiteSpace(venueName) == false && Config.GetValue(TheaterSharedAppSettings.EMAIL_SHOW_VENUE, true))
                sb.AppendFormat(GetCmsDecodedPropertyValue("SINGLE_TICKET_VENUE"), venueName);


            

 	     
}
        
        protected override void RenderFees()
        {
            sb.AppendFormat(GetCmsDecodedPropertyValue("FEE_TABLE"));

            var fees = (Cart as PublicCart).GetGroupedFeeItems();
            foreach (var fee in fees)
            {
                string feeName = fee.Key;
                //Only include colon if it's not already in EpiServer   
                if (feeName.EndsWith(":") == false)
                    feeName = string.Concat(feeName, ":");

                sb.AppendFormat(GetCmsDecodedPropertyValue("FEE_ITEM"), feeName, fee.Value.ToString("C2"));
            }

            sb.AppendFormat(GetCmsDecodedPropertyValue("FEE_TABLE_CLOSE"));
        }

        protected override void RenderAdaLineItemInfo(Adage.Tessitura.CartLineItem lineItem)
        {
            //don;t append any ada item notes, as the logic in base class is not valid
            //base.RenderAdaLineItemInfo(lineItem);
        }

        protected override void RenderContributionLineItem(Adage.Tessitura.ContributionLineItem contribution)
        {
            var csi = PublicContributionCsiNote.GetFor(contribution);
            if (csi != null)
            {
                var details = string.Format("{0} - {1}", csi.ProgramName, csi.ProgramLevel);
                sb.AppendFormat(GetCmsDecodedPropertyValue("CONTRIBUTION_LINE"), details, contribution.Amount.ToString("C2"));
            }
            else
            {
                base.RenderContributionLineItem(contribution);
            }
        }

        /// <summary>
        /// override gift certificate renderer to render both vouchers and GCs
        /// </summary>
        protected override void RenderGiftCertificates()
        {
            var gcItems = Cart.CartLineItems.OfType<GiftCertificateLineItem>();

            var packageVouchers = new List<GiftCertificateLineItem>();
            var giftCertificates = new List<GiftCertificateLineItem>();
            foreach (var giftCertificateLineItem in gcItems)
            {
                if (PublicPackageVoucher.IsPackageVoucher(giftCertificateLineItem))
                {
                    packageVouchers.Add(giftCertificateLineItem);
                }
                else
                {
                    giftCertificates.Add(giftCertificateLineItem);
                }
            }

            if (packageVouchers.Any())
            {
                sb.Append(GetCmsDecodedPropertyValue("PACKAGE_VOUCHER_TABLE"));
                foreach (var packageVoucher in packageVouchers)
                {
                    RenderPackageVoucherLineItem(packageVoucher);
                }
                sb.Append(GetCmsDecodedPropertyValue("PACKAGE_VOUCHER_TABLE_CLOSE"));

            }

            if (giftCertificates.Any())
            {
                sb.Append(GetCmsDecodedPropertyValue("GIFT_CERTIFICATE_TABLE"));
                foreach (var giftCertificate in giftCertificates)
                {
                    RenderGiftCertificateLineItem(giftCertificate);
                } 
                sb.Append(GetCmsDecodedPropertyValue("GIFT_CERTIFICATE_TABLE_CLOSE"));    
            }
        }

        /// <summary>
        /// Renders the contribution line item to the string builder.
        /// </summary>
        protected virtual void RenderPackageVoucherLineItem(GiftCertificateLineItem giftCertificate)
        {
            sb.AppendFormat(GetCmsDecodedPropertyValue("PACKAGE_VOUCHER_LINE"), giftCertificate.Description,
                giftCertificate.GiftCertificateNumber, giftCertificate.Amount.ToString("C2"));
        }
    }
}
