using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicTheater.Custom.Episerver
{
    public static class Constants
    {

        public static class TabNames
        {
            public const string SiteConfiguration = "Site Configuration";
            public const string Performances = "Performances";
            public const string SubTheme = "Sub Themes";
        }

        public static class RenderingTags
        {
            public const string PDP = "PDP";
            public const string Ticket = "Ticket";
            public const string Navigation = "Navigation";
        }

        

        public static class ContentGroupNames
        {
            public const string UniversalBlocks = "Universal Blocks";
            public const string HomePageBlocks = "HomePage Blocks";
            public const string MediaGroupName = "Media";
            public const string HistoryGroupName = "History";

            /*core feature set group names start */
            public const string SeasonGroupName = "Season";
            public const string SubscriptionGroupName = "Subscriptions";
            public const string CheckoutGroupName = "Checkout";
            public const string ContributeGroupName = "Contribute";
            public const string SeatingGroupName = "Seating";
            public const string ConfigurationGroupName = "Configuration";
            public const string AccountGroupName = "Account";
            public const string ContentGroupName = "Content";
            public const string EducationGroupName = "Education";
            /*core feature set group names end */

        }

        public static class CFSEmailCodeProperty
        {
            public const string CUSTOMERNUMBER = "CUSTOMERNUMBER";
            public const string ACCOUNTADDRESS = "ACCOUNTADDRESS";
        }

        public static class QueryStringParameters
        {
            public const string SiteTheme = "SiteTheme";
        }  

        public static class TessDefaultFields
        {
            public const string DefaultState = "Default State";
        }
    }
}
