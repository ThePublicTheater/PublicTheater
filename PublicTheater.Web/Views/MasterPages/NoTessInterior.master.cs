using System;
using System.Web.UI;
using EPiServer;
using EPiServer.Core;
using PublicTheater.Custom.Episerver.BaseClasses;

namespace PublicTheater.Web.Views.MasterPages
{
    public partial class NoTessInterior:MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Page is Pages.SectionLanding)
                {
                    pnlWrapper.CssClass = "homeWrapper";
                }
            }
        }

    }
}