using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Adage.Tessitura.UserObjects;
using PublicTheater.Custom.CoreFeatureSet.UserObjects;

namespace PublicTheater.Web.Controls.Account
{
    public class AddressManagerControl : TheaterTemplate.Web.Controls.AccountControls.AddressManagerControl
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                //prompt to update shipping address if it is placeholder address
                if (PublicUser.IsPlaceHolderAddress(CurrentPublicUser.ShippingAddress))
                {
                    ShowEditForm(CurrentPublicUser.ShippingAddress);
                }
            }
        }

        protected virtual PublicUser CurrentPublicUser
        {
            get
            {
                return base.CurrentUser as PublicUser;
            }
        }

        protected override void BindExistingAddresses()
        {
            //base.BindExistingAddresses();
            Adage.Tessitura.Repository.Flush<Adage.Tessitura.User>();
            CurrentPublicUser.GetAccountInfo();
            CurrentPublicUser.GetConstituentInfo();
            CurrentPublicUser.GetAddresses();

            lblBillingAddress.Text = CurrentPublicUser.BillingAddress.ToString();

            var addresses = new List<UserAddress>() { CurrentPublicUser.ShippingAddress };
            lvExistingAddresses.DataSource = addresses;
            lvExistingAddresses.DataBind();
        }

        protected override void lvExistingAddresses_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
        {
            base.lvExistingAddresses_ItemDataBound(sender, e);
        }
        protected override void lvExistingAddresses_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            base.lvExistingAddresses_ItemCommand(sender, e);
        }

        protected override void btnUpdate_Click(object sender, EventArgs e)
        {
            base.btnUpdate_Click(sender, e);
        }

        protected override void lbBillingEditAddress_Click(object sender, EventArgs e)
        {
            //base.lbBillingEditAddress_Click(sender, e);
            CurrentUser.GetAccountInfo();
            CurrentUser.GetConstituentInfo();
            CurrentUser.GetAddresses();
            ShowEditForm(CurrentPublicUser.BillingAddress);
        }
    }
}