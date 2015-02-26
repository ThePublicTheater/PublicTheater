using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer;
using EPiServer.Core;
using EPiServer.DynamicContent;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Adage.Tessitura;

namespace PublicTheater.Web.modules.Change_Email_Control
{
    [DynamicContentPlugIn(DisplayName = "ChangeEmailControl", ViewUrl = "~/modules/ChangeEmailControl/ChangeEmailDynamicControl.ascx")]
    public partial class ChangeEmailDynamicControl : System.Web.UI.UserControl
    {
        Adage.Tessitura.User currentUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            currentUser = Adage.Tessitura.User.GetUser();
            if (currentUser.LoggedIn && !currentUser.IsAnonymousLogin)
            {
                currentUser.GetAccountInfo();
                lblCurrentEmail.Text = currentUser.Email;
            }
            else
                UpdatePanel1.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(tbNewEmail.Text.Contains("@") && tbNewEmail.Text.Contains(".")))
                {
                    lblResult.ForeColor = System.Drawing.Color.Red;
                    lblResult.Text = "Update Failed. Please enter a valid email address.";
                    return;
                }
                currentUser.Email = tbNewEmail.Text;
                currentUser.Update();
                lblCurrentEmail.Text = tbNewEmail.Text;
                lblResult.ForeColor = System.Drawing.Color.Black;
                lblResult.Text = "Success!";
            }
            catch(Exception ex)
            {
                lblResult.ForeColor = System.Drawing.Color.Red;
                if (ex.Message.ToUpper().Contains("DUPLICATE"))
                    lblResult.Text = "Update Failed. That email already belongs to another account.";
                else
                    lblResult.Text="Update Failed.";
            }
        }
    }
}