using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adage.Tessitura;
using Adage.Tessitura.Common;
using PublicTheater.Custom.CoreFeatureSet.UserObjects;

namespace PublicTheater.Web.Controls.Account
{
    public class RegisterControl : TheaterTemplate.Web.Controls.AccountControls.RegisterControl
    {
        protected global::EPiServer.Web.WebControls.Property propDuplicateAccountEmailError;
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["returnURL"]))
            {
                Adage.Tessitura.TessSession.GetSession().SetReturnURL(Request.QueryString["returnURL"]);
            }
            base.Page_Load(sender, e);
        }
        protected override void submitRegistration_Click(object sender, EventArgs e)
        {

            if (!Page.IsValid)
            {
                return;
            }
            HideErrorProperties();

            if (PublicUser.AllowEmailRegister(tbxEmailAddress.Text) == false)
            {
                propDuplicateAccountEmailError.Visible = true;
                return;
            }

            Session.Remove("reCaptchaValid");
            base.submitRegistration_Click(sender, e);

        }

        protected override void HideErrorProperties()
        {
            base.HideErrorProperties();
            propDuplicateAccountEmailError.Visible = false;
        }
    }
}