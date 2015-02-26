using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer;
using EPiServer.Core;
using EPiServer.DynamicContent;

namespace PublicTheater.Web.modules.ArbitraryHTML
{
    [DynamicContentPlugIn(DisplayName = "ArbitraryHTMLDynamicContent", ViewUrl = "~/modules/ArbitraryHTML/ArbitraryHTMLDynamicContent.ascx")]
    public partial class ArbitraryHTMLDynamicContent : UserControl
    {
        #region Editable Properties

        // Add your editable properties as normal .Net properties.
        // Supported property types are string, int, bool, 
        // EPiServer.Core.PageReference and any class
        // inheriting EPiServer.Core.PropertyData.
        public String arbitrary { get; set; }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }
        }
    }
}