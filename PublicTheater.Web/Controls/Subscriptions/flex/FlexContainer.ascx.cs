using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using EPiServer.Personalization.VisitorGroups;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using TheaterTemplate.Shared.EpiServerConfig;

namespace PublicTheater.Web.Controls.Subscriptions.flex
{
    public partial class FlexContainer : TheaterTemplate.Web.Controls.Subscriptions.FlexControls.FlexContainer
    {

       protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
          
            var config = CurrentPackageConfig as PublicPackageDetailConfig;
            if (config != null)
            {
                if (!string.IsNullOrEmpty(config.SourceCode))
                {
                var promoCodeDetails = PromoCodeDetails.Get(config.SourceCode);
                if (promoCodeDetails != null && Repository.Get<TessSession>().PromotionCode != promoCodeDetails.SourceCode)
                {
                    Repository.Get<TessSession>().UpdateSourceCode(promoCodeDetails.SourceCode.ToString());    
                }
            }

                if (!IsInVisitorGroup("User Logged in") && !string.IsNullOrEmpty(config.NotLoggedIn))
            {
                performanceSelectionContainer.Visible = false;
                notLoggedInLbl.Visible = true;
                notLoggedInLbl.Text = config.NotLoggedIn;
            }
                else if (!IsInVisitorGroup("UTR_Symposium_2014") && !string.IsNullOrEmpty(config.NotInvited))
            {
                performanceSelectionContainer.Visible = false;
                notInvitedLbl.Visible = true;
                notInvitedLbl.Text = config.NotInvited;
            }
            else if (IsInVisitorGroup("UTR_Symposium_Already_Purchased") && config.AlreadyPurchased != null && config.AlreadyPurchased.Length > 0)
            {
                performanceSelectionContainer.Visible = false;
                alreadyPurchasedLbl.Visible = true;
                alreadyPurchasedLbl.Text = config.AlreadyPurchased;
            }
        }
        }
        public static bool IsInVisitorGroup(string roleName)
        {
            var visitorGroupHelp = new VisitorGroupHelper();
            return visitorGroupHelp.IsPrincipalInGroup(EPiServer.Security.PrincipalInfo.CurrentPrincipal, roleName);
        }

    }
}