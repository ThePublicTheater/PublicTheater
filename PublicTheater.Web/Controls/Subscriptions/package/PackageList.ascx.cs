using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;

namespace PublicTheater.Web.Controls.Subscriptions.package
{
    public class PackageList : TheaterTemplate.Web.Controls.Subscriptions.PackageControls.PackageList
    {
        protected override void BindPackages()
        {
            var packages = CurrentPackageListConfig.PackageDetailPages.Where(p => p.ShowSubscribeLink);

            rptPackages.DataSource = packages;
            rptPackages.DataBind();
            rptPackageAnchors.DataSource = CurrentPackageListConfig.PackageDetailPages;
            rptPackageAnchors.DataBind();
        }


        protected override void rptPackages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
            base.rptPackages_ItemDataBound(sender, e);
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var packageDetail = e.Item.DataItem as PublicPackageDetailConfig;

                string showPurchaseLink = packageDetail.ShowPurchaseLink;
                var lnkPurchaseVoucher = e.Item.FindControl("lnkPurchaseVoucher") as HyperLink;
                var lnkPurchasePackage = e.Item.FindControl("lnkSubscribeNow") as HyperLink;

                if (packageDetail != null && packageDetail.PackageVoucherEnabled && lnkPurchaseVoucher != null)
                {
                    lnkPurchaseVoucher.Visible = true;
                    lnkPurchaseVoucher.NavigateUrl = packageDetail.PackageVoucherLink;
                }
                if (showPurchaseLink != null && showPurchaseLink.ToUpper().Contains("FALSE") && lnkPurchasePackage != null)
                {
                    lnkPurchasePackage.Visible = false;
                }
            }
        }
    }
}