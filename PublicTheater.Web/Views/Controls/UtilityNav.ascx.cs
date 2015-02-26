using System;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using Adage.Theater.RelationshipManager.Helpers;
using PublicTheater.Custom.Episerver;

namespace PublicTheater.Web.Views.Controls
{
    public partial class UtilityNav : Custom.Episerver.BaseClasses.PublicBaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var linkUrl = Config.GetValue(string.Format("EmailSignUp_{0}", RequestedTheme), string.Empty);
                if (string.IsNullOrEmpty(linkUrl))
                {
                    lnkEmailSignUp.Visible = false;
                }
                else
                {
                    lnkEmailSignUp.NavigateUrl = linkUrl;
                }


                lnkCart.NavigateUrl = Config.GetValue(TheaterTemplate.Shared.Common.TheaterSharedAppSettings.CART_URL, "~/cart/index.aspx");

                try
                {
                    lbCart.Text = string.Format("Cart ({0})", Adage.Tessitura.Cart.GetCart().CartLineItems.Count);
                }
                catch (Exception)
                {
                    //cart expired or mos conflict?
                    lbCart.Text = "Cart (0)";
                }

                var hfNoTess = this.Parent.Parent.Parent.FindControl("hfNoTess") as HiddenField;
                if (hfNoTess == null)
                {
                    var currentUser = Adage.Tessitura.User.GetUser();
                    if (currentUser.IsAnonymousLogin == false && currentUser.LoggedIn)
                    {
                        lnkLogin.NavigateUrl =
                            Config.GetValue(Custom.CoreFeatureSet.Common.PublicAppSettings.LOGIN_PAGE_URL,
                                "/account/login");
                        lbLogin.Text = "Log Out";
                    }
                    else
                    {
                        if (Custom.Episerver.Utility.Utility.GetRequestedTheme(CurrentPage) ==
                            Enums.SiteTheme.HereLiesLove)
                        {

                            lnkLogin.NavigateUrl =
                                Config.GetValue("LOGIN_PAGE_URL_HLL",
                                    "/account/login/?SiteTheme=HereLiesLove&returnURL=/Tickets/Calendar/PlayDetailsCollection/public/hll/Tickets/");
                        }
                        else
                        {
                            lnkLogin.NavigateUrl =
                                Config.GetValue(Custom.CoreFeatureSet.Common.PublicAppSettings.LOGIN_PAGE_URL,
                                    "/account/login");
                        }
                        lbLogin.Text = "Login";
                    }
                }

            }

            if (Custom.Episerver.Utility.Utility.GetRequestedTheme(CurrentPage) == Enums.SiteTheme.HereLiesLove)
            {

                //always display my account link
                lnkRegister.NavigateUrl =
                    Config.GetValue(Custom.CoreFeatureSet.Common.PublicAppSettings.Ticket_History_URL,
                        "/account/Ticket-History")+"?SiteTheme=HereLiesLove";
            }
            else
            {
                   //always display my account link
                lnkRegister.NavigateUrl =
                    Config.GetValue(Custom.CoreFeatureSet.Common.PublicAppSettings.Ticket_History_URL,
                        "/account/Ticket-History");
            }
            lbRegister.Text = "My Account";

            
        }

    }
}