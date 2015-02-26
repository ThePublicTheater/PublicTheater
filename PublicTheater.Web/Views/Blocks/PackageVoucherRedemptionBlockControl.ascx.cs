using System;
using System.Linq;
using System.Text;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using EPiServer.Web;
using PublicTheater.Custom.CoreFeatureSet.Common;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using PublicTheater.Custom.CoreFeatureSet.Memberships;
using TheaterTemplate.Shared.Common;
using TheaterTemplate.Shared.EpiServerConfig;
using TheaterTemplate.Shared.EpiServerProperties;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class PackageVoucherRedemptionBlockControl : Custom.Episerver.BaseClasses.PublicBaseResponsiveBlockControl<Custom.Episerver.Blocks.PackageVoucherRedemptionBlockData>
    {

        private PublicPackageListConfig _currentPackageListConfig;
        /// <summary>
        /// Package List Config object populated from EPiServer based on the current template page.
        /// </summary>
        protected virtual PublicPackageListConfig CurrentPackageListConfig
        {
            get
            {
                return _currentPackageListConfig ??
                       (_currentPackageListConfig = PublicPackageListConfig.GetPublicFlexPackageListConfig());
            }
        }

        protected string LinkedRedemptionCode
        {
            get { return Request.QueryString["RedemptionCode"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lbApplyRedemptionCode.Click += lbApplyRedemptionCode_Click;
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(CurrentData.RedeemButtonText) == false)
                {
                    lbApplyRedemptionCode.Text = CurrentData.RedeemButtonText;
                }

                if (string.IsNullOrEmpty(LinkedRedemptionCode) == false)
                {
                    //linked redemption code, auto apply it!
                    txtRedemptionCode.Text = LinkedRedemptionCode;
                    lbApplyRedemptionCode_Click(sender, e);
                }
            }
        }

        /// <summary>
        /// Apply redemotion code event trigger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void lbApplyRedemptionCode_Click(object sender, EventArgs e)
        {
            RedeemPackageVoucher(txtRedemptionCode.Text);
        }


        /// <summary>
        /// Redeem a package voucher code
        /// </summary>
        /// <param name="redemptionCode"></param>
        protected virtual void RedeemPackageVoucher(string redemptionCode)
        {
            redemptionCode = redemptionCode.Trim();

            try
            {
                var packageVoucher = PublicPackageVoucher.GetPackageVoucher(redemptionCode);

                //valid voucher but used with no balanace left
                if (packageVoucher.Balance <= 0)
                {
                    lblError.Text = CurrentData.PackageVoucherEmpty.ToHtmlString();
                    return;
                }

                var publicPackageDetailConfig = CurrentPackageListConfig.GetPackageDetailConfigByPaymentMethodId(packageVoucher.PaymentMethodId);

                if (publicPackageDetailConfig != null)
                {
                    //valid voucher save it into session and auto apply on payment page
                    packageVoucher.AttachToSession();
                    Response.Redirect(publicPackageDetailConfig.SubscribeNowUrl, false);
                }
                else
                {
                    //did not find the matching package for this voucher
                    lblError.Text = CurrentData.InvalidPackageVoucher.ToHtmlString();
                }
            }
            catch (Exception ex)
            {
                //invalid voucher
                lblError.Text = CurrentData.InvalidPackageVoucher.ToHtmlString();
                Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("Redeem Package Voucher Error- {0} - {1}", ex.Message, ex.StackTrace), null);
            }
        }
    }
}