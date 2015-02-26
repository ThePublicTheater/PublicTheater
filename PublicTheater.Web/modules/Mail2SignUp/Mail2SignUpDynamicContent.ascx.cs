using System;
using System.Web.UI;
using Adage.Common.ExtensionMethods.Generic;
using Adage.Tessitura;
using EPiServer.DynamicContent;
using System.Collections.Generic;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using PublicTheater.Custom.Episerver;


namespace PublicTheater.Web.modules.Mail2SignUp
{
    [DynamicContentPlugIn(DisplayName = "Mail2SignUpDynamicContent", ViewUrl = "~/modules/Mail2SignUp/Mail2SignUpDynamicContent.ascx")]
    public partial class Mail2SignUp : UserControl
    {
        #region Editable Properties

        // Add your editable properties as normal .Net properties.
        // Supported property types are string, int, bool, 
        // EPiServer.Core.PageReference and any class
        //[DynamicContentItemAttribute(PropertyType = typeof(PropertyXhtmlString))]

        
        public bool SubmitButton { get; set; }
        public string SubmitText { get; set; }
        public bool MatchSubscriptions { get; set; }
        public int FirstChoice { get; set; }
        public string PublicText { get; set; }
        public bool PublicDefaultChecked { get; set; }
        public string JoesPubText { get; set; }
        public bool JoesPubDefaultChecked { get; set; }
        public string ShakespeareText { get; set; }
        public bool ShakespeareDefaultChecked { get; set; }
        public string ForumText { get; set; }
        public bool ForumDefaultChecked { get; set; }
        public string UtrText { get; set; }
        public bool UtrDefaultChecked { get; set; }
        public string WeeklyText { get; set; }
        public bool WeeklyDefaultChecked { get; set; }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            submitEntry.Text = SubmitText;



            if (!IsPostBack)
            {
                if (MatchSubscriptions)
                {
                    User currentUser = Adage.Tessitura.User.GetUser();
                    currentUser.GetAccountInfo();
                    List<int> currentMail2Subscriptions = Mail2Helper.GetSubscriptionsByEmail(currentUser.Email);
                    if (
                        currentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByTheme(Enums.SiteTheme.Default)))
                    {
                        PublicDefaultChecked = true;
                    }
                    if (
                        currentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByTheme(Enums.SiteTheme.JoesPub)))
                    {
                        JoesPubDefaultChecked = true;
                    }
                    if (currentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByTheme(Enums.SiteTheme.Shakespeare)))
                    {
                        ShakespeareDefaultChecked = true;
                    }
                    if (currentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByName("forum")))
                    {
                        ForumDefaultChecked = true;
                    }
                    if (currentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByName("utr")))
                    {
                        UtrDefaultChecked = true;
                    }
                    if (currentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByName("weekly")))
                    {
                        WeeklyDefaultChecked = true;
                    }
                }
                DataBind();

            }

           

        }

        
    }
}