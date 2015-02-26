using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using TheaterTemplate.Shared.Common;

namespace PublicTheater.Custom.CoreFeatureSet.Common
{
    [Serializable]
    public class PublicPackageVoucher : TheaterSharedGiftCertificateInfo
    {
        private const string SessionVariableKey = "CurrentPackageVoucher";

        /// <summary>
        /// Payment Method Id
        /// </summary>
        public virtual int PaymentMethodId { get; protected set; }

        /// <summary>
        /// Fill gift certificate fields and payment method id
        /// </summary>
        protected override void Fill()
        {
            base.Fill();

            //fill payment method id by calling the stored procedure
            var sqlParameters = new Dictionary<string, object>();
            sqlParameters.Add("@gc_no", this.Number);
            try
            {
                DataSet dataSet = TessSession.GetSession().ExecuteLocalProcedure(Common.PublicAppSettings.PackageVoucherPaymentMethodSP, sqlParameters);
                if (dataSet != null && (dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0))
                {
                    var giftCertInfo = dataSet.Tables[0].Rows[0];
                    PaymentMethodId = giftCertInfo.GetColumnValue("pmt_method", 0);
                }
            }
            catch (Exception exception)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(exception, "LoadPublicPackageVoucher PaymentMethodId", null);
            }
        }

        /// <summary>
        /// Store data in Session
        /// </summary>
        public virtual void AttachToSession()
        {
            TessSession.GetSession().Variables.SetVariable(SessionVariableKey,this.Number);
        }

        /// <summary>
        /// Clear current package voucher in session
        /// </summary>
        public static void ClearSession()
        {
            TessSession.GetSession().Variables.DeleteVariable(SessionVariableKey);
        }

        /// <summary>
        /// Load current package voucher from session
        /// </summary>
        /// <returns></returns>
        public static PublicPackageVoucher LoadFromSession()
        {
            try
            {
                var redemptionCode = TessSession.GetSession().Variables.GetVariable(SessionVariableKey);
                return PublicPackageVoucher.GetPackageVoucher(redemptionCode);
            }
            catch (Exception ex)
            {
                //does not exist in session, return null
                return null;
            }
        }


        /// <summary>
        /// Get package voucher by redemption code
        /// </summary>
        /// <param name="redemptionCode"></param>
        /// <returns></returns>
        public static PublicPackageVoucher GetPackageVoucher(string redemptionCode)
        {
            var packageVoucher = Repository.Get<PublicPackageVoucher>(redemptionCode);
            if (packageVoucher.isFilled == false)
            {
                packageVoucher.Number = redemptionCode;
                packageVoucher.Fill();
            }

            return packageVoucher;
        }

        /// <summary>
        /// Determind if a gift certificate line item is a package voucher by checking the payment method description
        /// </summary>
        /// <param name="giftCertificateLineItem"></param>
        /// <returns></returns>
        public static bool IsPackageVoucher(GiftCertificateLineItem giftCertificateLineItem)
        {
            return giftCertificateLineItem != null && IsPackageVoucher(giftCertificateLineItem.Description);
        }

        /// <summary>
        /// Is package voucher by gift certificate description
        /// </summary>
        /// <param name="giftCertificateDescription"></param>
        /// <returns></returns>
        public static bool IsPackageVoucher(string giftCertificateDescription)
        {
            var packageVoucherDesc = Config.GetValue("PackageVoucherDescription", "Package Voucher");
            return giftCertificateDescription.ToLower().Contains(packageVoucherDesc.ToLower());
        }

        /// <summary>
        /// get the redemption url for a package voucher
        /// </summary>
        /// <param name="redemptionCode"></param>
        /// <returns></returns>
        public static string GetPackageVoucherRedemptionUrl(string redemptionCode)
        {
            if (string.IsNullOrEmpty(redemptionCode))
                return string.Empty;

            var packageLandingPage = PublicPackageListConfig.GetPublicFlexPackageListConfig();
            if (packageLandingPage == null)
                return string.Empty;
            var baseUrl = HttpContext.Current == null
                ? string.Empty
                : HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            return string.Format("{0}{1}?RedemptionCode={2}", 
                baseUrl,
                packageLandingPage.GetPackageListPageUrl(),
                redemptionCode);
        }
    }
}
