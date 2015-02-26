using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.WebControls;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class TextBlockControl : Custom.Episerver.BaseClasses.PublicBaseResponsiveBlockControl<Custom.Episerver.Blocks.TextBlockData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}