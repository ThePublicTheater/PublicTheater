using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using PublicTheater.Custom.CoreFeatureSet.Common;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using PublicTheater.Custom.CoreFeatureSet.Helper;

namespace PublicTheater.Web.Controls.Subscriptions.flex
{
    public partial class FlexPackageDisplay : TheaterTemplate.Web.Controls.Subscriptions.FlexControls.FlexPackageDisplay
    {
        protected virtual PublicPackageListConfig CurrentPackageListConfig
        {
            get
            {
                return PublicPackageListConfig.GetPublicFlexPackageListConfig();
            }
        }

        protected override void BindPackageInformation()
        {
            base.BindPackageInformation();
            ltrMinimum.Text = PerformanceHelper.GetFlexPackageRequirementDescription(CurrentFlexPackage);
        }

        protected override void rptPriceTypes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            base.rptPriceTypes_ItemDataBound(sender, e);

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckVoucherQuantity(e);
                var x = CurrentPackageConfig as PublicPackageDetailConfig;
                var ddlQuantity = e.Item.FindControl("ddlQuantity") as DropDownList;
                if (ddlQuantity == null) return;
                if (x.MaxQty > 0)
                {
                    if (x.MaxQty == 1)
                    {
                        ddlQuantity.SelectedValue = "1";
                        ddlQuantity.Attributes.Add("style", "display:none");
                    }
                    else
                        ddlQuantity.DataSource = Enumerable.Range(1, x.MaxQty);
                }
                else
                {
                     ddlQuantity.DataSource = Enumerable.Range(1, 10);
                }
                  ddlQuantity.DataBind();
            }
            else if (e.Item.ItemType == ListItemType.Header)
            {
                var lbl = e.Item.FindControl("qtyLbl") as Label;
                if (lbl == null) return;
                var x = CurrentPackageConfig as PublicPackageDetailConfig;
                if (x.MaxQty == 1)
                    lbl.Visible = false;
            }
        }

        protected virtual void CheckVoucherQuantity(RepeaterItemEventArgs e)
        {
            var ddlQuantity = e.Item.FindControl("ddlQuantity") as DropDownList;
            if (ddlQuantity == null)
            {
                return;
            }
            var packageVoucher = PublicPackageVoucher.LoadFromSession();
            if (packageVoucher == null)
            {
                //no voucher found in session
                return;
            }

            var packageConfig = CurrentPackageListConfig.GetPackageDetailConfigByPaymentMethodId(packageVoucher.PaymentMethodId);
            if (packageConfig == null || packageConfig.FlexPackageId != CurrentFlexPackage.PackageId)
            {
                //voucher in session not assigned to any package
                return;
            }

            var quantity = packageVoucher.OriginalAmount / (Decimal)packageConfig.PackageVoucherUnitPrice;

            var item = ddlQuantity.Items.FindByValue(quantity.ToString("0"));
            if (item != null)
            {
                ddlQuantity.Enabled = false;
                ddlQuantity.SelectedValue = item.Value;
            }



        }

        protected override void BindPriceTypes()
        {
            rptPriceTypes.ItemDataBound += new RepeaterItemEventHandler(rptPriceTypes_ItemDataBound);

            if (CurrentFlexPackage.PackagePricing.PriceTypes.Count > 0 && CurrentPackageConfig.AllowedPriceTypes.Count > 0)
            {
                rptPriceTypes.DataSource = CurrentFlexPackage.PackagePricing.PriceTypes.Where(pt => CurrentPackageConfig.AllowedPriceTypes.Contains(pt.PriceTypeId));
                rptPriceTypes.DataBind();
            }
            else if (CurrentFlexPackage.PackagePricing.PriceTypes.Count > 0)
            {
                rptPriceTypes.DataSource = CurrentFlexPackage.PackagePricing.PriceTypes;
                rptPriceTypes.DataBind();
            }
            else
            {
                rptPriceTypes.Visible = false;
                btnContinueToSeating.Visible = false;
                Adage.Common.ElmahCustomError.CustomError.LogError(null, string.Format("Flex Package Has No Price Types {0}", CurrentFlexPackage.PackageId), null);
            }
        }

    }
}