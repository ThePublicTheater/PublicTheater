using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adage.Tessitura;
using PublicTheater.Custom.CoreFeatureSet.Common;
using PublicTheater.Custom.Episerver;
using PublicTheater.Custom.Mail2REST;

namespace PublicTheater.Custom.CoreFeatureSet.Helper
{
    public static class Mail2Helper
    {
        /// <summary>
        /// minimum amount qualifies for partners benefits
        /// </summary>
        public static string Mail2RESTAPIKey
        {
            get { return Config.GetValue(PublicAppSettings.Mail2RESTAPIKey, "967043a055efdf0515b755e26fa0c769"); }
        }

        /// <summary>
        /// minimum amount qualifies for partners benefits
        /// </summary>
        public static string Mail2RESTAPIUrl
        {
            get { return Config.GetValue(PublicAppSettings.Mail2RESTAPIUrl, "api.emailcampaigns.net/2/REST/"); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="siteTheme"></param>
        /// <returns></returns>
        public static int GetMail2ListIdByTheme(Enums.SiteTheme siteTheme)
        {
            return Config.GetValue(string.Format("Mail2ListId_{0}", siteTheme), 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="siteTheme"></param>
        /// <returns></returns>
        public static int GetMail2ListIdByName(string name)
        {
            return Config.GetValue(string.Format("Mail2ListId_{0}", name), 0);
        }
        /// <summary>
        /// get new mail2 api client with config api key and url
        /// </summary>
        /// <returns></returns>
        public static mail2 GetMail2APIClient()
        {
            return new mail2(Mail2RESTAPIKey, false, Mail2RESTAPIUrl);
        }

        /// <summary>
        /// Get Active List 
        /// </summary>
        /// <returns></returns>
        public static string GetActiveList()
        {
            var optionalParameters = new Dictionary<string, string>();
            var result = GetMail2APIClient().List_Get_Active_Lists(optionalParameters);
            return result as string;
        }

        /// <summary>
        /// subscribe email to a list
        /// </summary>
        /// <param name="siteTheme"></param>
        /// <param name="email"></param>
        /// <param name="subscribe">subscribe or unsubscribe</param>
        /// <returns></returns>
        public static bool SubscribeEmailList(Enums.SiteTheme siteTheme, string email, bool subscribe)
        {
            var listId = GetMail2ListIdByTheme(siteTheme);
            if (listId > 0)
            {
                var optionalParameters = new Dictionary<string, string>();
                if(subscribe)
                {
                    return (bool)GetMail2APIClient().List_Subscribe(listId, email, optionalParameters);
                }
                else
                {
                    return (bool)GetMail2APIClient().List_Unsubscribe(listId, email, optionalParameters);
                }
            }

            return false;
        }
        /// <summary>
        /// subscribe email to a list
        /// </summary>
        /// <param name="siteTheme"></param>
        /// <param name="email"></param>
        /// <param name="subscribe">subscribe or unsubscribe</param>
        /// <returns></returns>
        public static bool SubscribeEmailList(int listId, string email, bool subscribe)
        {
            
            if (listId > 0)
            {
                var optionalParameters = new Dictionary<string, string>();
                if (subscribe)
                {
                    return (bool)GetMail2APIClient().List_Subscribe(listId, email, optionalParameters);
                }
                else
                {
                    return (bool)GetMail2APIClient().List_Unsubscribe(listId, email, optionalParameters);
                }
            }

            return false;
        }
        public static List<int> GetSubscriptionsByEmail(string email)
        {
            try
            {
                var result = GetMail2APIClient().Contact_Get_Subscriptions(email);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(result.ToString());
            }
            catch (Exception exception)
            {
                //if DeserializeObject failed, then account not exist
                return new List<int>();
                //throw;
            }
            
        }

        
    }
}
