using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PublicTheater.Custom.Episerver.BaseClasses;
using EPiServer.Core;

namespace PublicTheater.Web.Views.Controls
{
    public partial class PageHeading : PublicBaseUserControl
    {
        private const string HEADING_PROP_NAME= "Heading";
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (CurrentPage.Property[HEADING_PROP_NAME] != null && !string.IsNullOrEmpty(CurrentPage.Property[HEADING_PROP_NAME].ToWebString()))
            {
                propHeading.PropertyName = HEADING_PROP_NAME;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}