using System;
using System.Collections.Generic;
using System.Web.Services;
using Adage.Tessitura;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using PublicTheater.Custom.Episerver;


namespace PublicTheater.Web.Services
{
    /// <summary>
    /// Summary description for 
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EmailSubscribeService : System.Web.Services.WebService
    {

        [WebMethod]
        public static bool MatchSubscriptions(bool publicList, bool joesPubList, bool shakespeareList, bool forumList, bool thisWeekList, bool utrList)
        {
            try
            {
                User currentUser = Adage.Tessitura.User.GetUser();
                currentUser.GetAccountInfo();
                List<int> currentMail2Subscriptions = Mail2Helper.GetSubscriptionsByEmail(currentUser.Email);

                if (publicList != currentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByTheme(Enums.SiteTheme.Default)))
                    Mail2Helper.SubscribeEmailList(Enums.SiteTheme.Default, currentUser.Email, publicList);

                if (joesPubList != currentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByTheme(Enums.SiteTheme.JoesPub)))
                    Mail2Helper.SubscribeEmailList(Enums.SiteTheme.JoesPub, currentUser.Email, joesPubList);

                if (shakespeareList != currentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByTheme(Enums.SiteTheme.Shakespeare)))
                    Mail2Helper.SubscribeEmailList(Enums.SiteTheme.Shakespeare, currentUser.Email, shakespeareList);

                if (forumList != currentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByName("forum")))
                    Mail2Helper.SubscribeEmailList(Mail2Helper.GetMail2ListIdByName("forum"), currentUser.Email, forumList);

                if (thisWeekList != currentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByName("weekly")))
                    Mail2Helper.SubscribeEmailList(Mail2Helper.GetMail2ListIdByName("weekly"), currentUser.Email, thisWeekList);

                if (utrList != currentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByName("utr")))
                    Mail2Helper.SubscribeEmailList(Mail2Helper.GetMail2ListIdByName("utr"), currentUser.Email, utrList);
                
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        [WebMethod]
        public static bool Subscribe(bool publicList, bool joesPubList, bool shakespeareList, bool forumList, bool thisWeekList, bool utrList)
        {
            try
            {
                User currentUser = Adage.Tessitura.User.GetUser();
                currentUser.GetAccountInfo();
                List<int> currentMail2Subscriptions = Mail2Helper.GetSubscriptionsByEmail(currentUser.Email);


                if (publicList && !currentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByTheme(Enums.SiteTheme.Default)))
                    Mail2Helper.SubscribeEmailList(Enums.SiteTheme.Default, currentUser.Email, true);

                if (joesPubList && !currentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByTheme(Enums.SiteTheme.JoesPub)))
                    Mail2Helper.SubscribeEmailList(Enums.SiteTheme.JoesPub, currentUser.Email, true);

                if (shakespeareList && !currentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByTheme(Enums.SiteTheme.Shakespeare)))
                    Mail2Helper.SubscribeEmailList(Enums.SiteTheme.Shakespeare, currentUser.Email, true);

                if (forumList && !currentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByName("forum")))
                    Mail2Helper.SubscribeEmailList(Mail2Helper.GetMail2ListIdByName("forum"), currentUser.Email, true);

                if (thisWeekList && !currentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByName("weekly")))
                    Mail2Helper.SubscribeEmailList(Mail2Helper.GetMail2ListIdByName("weekly"), currentUser.Email, true);

                if (utrList && !currentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByName("utr")))
                    Mail2Helper.SubscribeEmailList(Mail2Helper.GetMail2ListIdByName("utr"), currentUser.Email, true);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
    }
}
