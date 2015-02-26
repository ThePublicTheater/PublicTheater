using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adage.Tessitura;
using PublicTheater.Custom.Episerver;
using PublicTheater.Web.Services;

namespace PublicTheater.Web.Controls.Account
{
    public class MyProfileControl : TheaterTemplate.Web.Controls.AccountControls.MyProfileControl
    {

        protected global::System.Web.UI.WebControls.HyperLink lnkEmailPreference;
        protected global::System.Web.UI.WebControls.HiddenField Mail2Public;
        protected global::System.Web.UI.WebControls.HiddenField Mail2JoesPub;
        protected global::System.Web.UI.WebControls.HiddenField Mail2Shakespeare;
        protected global::System.Web.UI.WebControls.HiddenField Mail2Forum;
        protected global::System.Web.UI.WebControls.HiddenField Mail2Utr;
        protected global::System.Web.UI.WebControls.HiddenField Mail2Weekly;
        protected override void LoadUserFields()
        {
            base.LoadUserFields();

            if(lnkEmailPreference!=null)
            {
                 var linkUrl = Config.GetValue(string.Format("EmailSignUp_{0}", Enums.SiteTheme.Default), string.Empty);
                if (string.IsNullOrEmpty(linkUrl))
                {
                    lnkEmailPreference.Visible = false;
                }
                else
                {
                    lnkEmailPreference.NavigateUrl = linkUrl;
                }
            }
        }

        protected override void UpdateUserObject()
        {
            base.UpdateUserObject();
            var email = CurrentUser.UserEmailAddresses.GetPrimaryEmailAddress();
            if(email.Email != tbxEmailAddress.Text)
            {
                email.Email = tbxEmailAddress.Text;
                email.Update();
            }
            EmailSubscribeService.MatchSubscriptions(Mail2Public.Value == "true", Mail2JoesPub.Value == "true", Mail2Shakespeare.Value == "true", Mail2Forum.Value == "true", Mail2Weekly.Value == "true", Mail2Utr.Value == "true");

        }
        
    }
}