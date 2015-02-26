using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer;
using PublicTheater.Web.WebUtility;

namespace PublicTheater.Web.Views.Controls
{
    public partial class SearchTextBox : System.Web.UI.UserControl
    {
        protected string SearchPageUrl
        {
            get
            {
                return Custom.Episerver.Utility.Utility.GetFriendlyUrl(EpiHelper.GetHomePage().SearchResultPage);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            hidSearchPageUrl.Value = SearchPageUrl;
            hidSearchPartnerID.Value = EpiHelper.GetHomePage().SearchEngineKey;
        }
    }
}