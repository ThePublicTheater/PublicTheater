using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adage.Tessitura;
using TheaterTemplate.Shared.Common;

namespace PublicTheater.Web.Controls.Account
{
    public class LoginControl : TheaterTemplate.Web.Controls.AccountControls.LoginControl
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["returnURL"]))
            {
                Adage.Tessitura.TessSession.GetSession().SetReturnURL(Request.QueryString["returnURL"]);
            }
            base.Page_Load(sender, e);
        }
        protected override bool ShowPromo
        {
            get
            {
                //hide promo on login page
                return false;
            }
        }
        protected override void CheckIfLoggedIn()
        {
            //if logged in, logout before redirect
            var currentUser = User.GetUser();
            if (currentUser.IsAnonymousLogin == false && currentUser.LoggedIn)
            {
                Adage.Tessitura.User.GetUser().Logout();
                Adage.Tessitura.Repository.Flush<Adage.Tessitura.User>();
                Adage.Tessitura.User.GetUser().LoginAnonymous();
                Response.Redirect(Adage.Tessitura.TessSession.GetSession().GetReturnURL());
            }

            //base.CheckIfLoggedIn();
        }
        /// <summary>
        /// Event handler for retrieve password
        /// Sends a Temporary password
        /// CFS default behavior is a huge security hole
        /// </summary>
        protected override void retrievePassword_Click(object sender, EventArgs e)
        {
            try
            {
                invalidLogin.Visible = false;
                invalidPromo.Visible = false;
                loginError.Visible = false;
                resetSuccessful.Visible = false;
                resetError.Visible = false;
                pnlError.Visible = false;

                User currUser = Adage.Tessitura.User.GetUser();
                currUser.Email = tbxEmailRetrievePassword.Text.Trim();
                var templateID = Config.GetValue(Adage.Tessitura.Common.ConfigKeys.Send_Credentials_Template, 1);
                int loginType = Config.DefaultLoginType;
                
                try
                {
                    // Try with default login type
                    
                    Repository.Get<TessituraAPI>().SendCredentials(Repository.Get<TessSession>().SessionKey, currUser.Email, loginType, true, false, true, templateID);
                }
                catch
                {
                    // Retry with temporary login type
                    loginType = -1;
                    Repository.Get<TessituraAPI>().SendCredentials(Repository.Get<TessSession>().SessionKey, currUser.Email, loginType, true, false, true, templateID);
                }

                resetSuccessful.Visible = true;
                pnlError.Visible = true;
            }
            catch (Exception ex)
            {
                resetError.Visible = true;
                pnlError.Visible = true;
                if (ex.Message.ToLower().Contains(Config.GetValue(TheaterSharedAppSettings.FORGOT_PASSWORD_NO_LOGIN_EXCEPTION, "no login found")) == false)
                    Adage.Common.ElmahCustomError.CustomError.LogError(ex, "Retrieve Password", null);
            }
        }
    }
}