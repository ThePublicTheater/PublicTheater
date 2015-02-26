using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using EPiServer.Core;
using EPiServer.Personalization.VisitorGroups;
using EPiServer.UI;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using PublicTheater.Custom.Episerver;
using PublicTheater.Custom.Episerver.Pages;
using PublicTheater.Custom.Episerver.Properties;
using PublicTheater.Custom.Episerver.Utility;
using TheaterTemplate.Shared.EpiServerConfig;
using TheaterTemplate.Shared.GiftCertificateObjects;
using TheaterTemplate.Web.Views.Controls;


namespace PublicTheater.Web.Views.Pages
{
    public partial class PackageVoucher : Custom.Episerver.BaseClasses.PublicBasePage<Custom.Episerver.Pages.PackageVoucherPageData>
    {
        
        protected virtual double UnitPrice
        {
            get
            {
                double unitPrice;
                double.TryParse(Request.QueryString["UnitPrice"], out unitPrice);
                return unitPrice;
            }
        }

        protected virtual int PaymentMethodId
        {
            get
            {
                int paymentMethodId;
                int.TryParse(Request.QueryString["PaymentMethodId"], out paymentMethodId);
                return paymentMethodId;
            }
        }

        protected virtual double TotalAmount
        {
            get
            {
                return UnitPrice * int.Parse(ddlQuantity.SelectedValue);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lbAddToCart.Click += lbAddToCart_Click;
            
            if (!IsInVisitorGroup("User Logged in") && notLoggedIn.InnerProperty.Value!=null && notLoggedIn.InnerProperty.Value.ToString().Length>0)
            {
                purchasePanel.Visible = false;
                notLoggedIn.Visible = true;
            }
            else if (!IsInVisitorGroup("UTR_Symposium_2014") && notInvited.InnerProperty.Value != null && notInvited.InnerProperty.Value.ToString().Length > 0)
            {
                purchasePanel.Visible = false;
                notInvited.Visible = true;
            }
            else if (IsInVisitorGroup("Purchased_UTR_Symposium_voucher_2014") && alreadyPurchased.InnerProperty.Value != null && alreadyPurchased.InnerProperty.Value.ToString().Length > 0)
            {
                purchasePanel.Visible = false;
                alreadyPurchased.Visible = true;
            }


            if (!IsPostBack)
            {
                lblUnitPrice.Text = UnitPrice.ToString("c");
                if(CurrentPage.MaxQty>0)
                    ddlQuantity.DataSource = Enumerable.Range(1, CurrentPage.MaxQty);
                else
                    ddlQuantity.DataSource = Enumerable.Range(1, 10);

                ddlQuantity.DataBind();
            }
        }

        protected virtual void lbAddToCart_Click(object sender, EventArgs e)
        {
            
            bool successful = false;

            bool voucherInCart =
                Cart.GetCart()
                    .CartLineItems.OfType<GiftCertificateLineItem>()
                    .Where(t => t.Description.ToUpper().Contains("VOUCHER"))
                    .Any();
            if (CurrentPage.MaxQty > 0)
            {
                if (Convert.ToInt32(ddlQuantity.SelectedValue) >
                    Convert.ToInt32(Session[Convert.ToString(CurrentPage.WorkPageID) + "qtyLeft"])&&voucherInCart)
                {
                    lblError.Text = "The maximum number of vouchers per customer is " +
                                    Convert.ToString(CurrentPage.MaxQty) + ". Please adjust your quantity.";
                    return;
                }
            }

            try
            {
                var redemptionCode = GiftCertificateLineItem.Reserve(TotalAmount, PaymentMethodId);
                SaveGiftCertificateEmailInformation(redemptionCode);
                successful = true;
             
            }
            catch (Exception ex)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("Add Package Voucher - {0} - {1}", ex.Message, ex.StackTrace), null);

                if (CurrentPage.ErrorMessage != null)
                {
                    lblError.Text = CurrentPage.ErrorMessage.ToHtmlString();
                }
            }

            if (successful)
            {
                if (CurrentPage.MaxQty > 0)
                {
                    Session[Convert.ToString(CurrentPage.WorkPageID) + "qtyLeft"] = CurrentPage.MaxQty -
                                                                                    Convert.ToInt32(ddlQuantity.SelectedValue);
                }
                Response.Redirect(TheaterSharedConfig.GetValue(TheaterTemplate.Shared.Common.TheaterSharedAppSettings.CART_URL, "/cart/index.aspx"));

            }
        }

        /// <summary>
        /// Save gift certificate information as an XML file so 
        /// it can be accessed later by the cart
        /// </summary>
        protected virtual void SaveGiftCertificateEmailInformation(string certificateCode)
        {
            var giftCertEmail = new GiftCertificateEmail
            {
                To = string.Empty,
                From = string.Empty,
                Email = string.Empty,
                Message = string.Empty,
                Amount = TotalAmount,
                DesignPath = CurrentPage.ExternalLinkUrl()
            };
            giftCertEmail.StoreAsXml(certificateCode);
        }
        public static bool IsInVisitorGroup(string roleName)
        {
            var visitorGroupHelp = new VisitorGroupHelper();
            return visitorGroupHelp.IsPrincipalInGroup(EPiServer.Security.PrincipalInfo.CurrentPrincipal, roleName);
        }

    }
}