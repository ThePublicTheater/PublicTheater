using System;
using EPiServer.Web;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class ButtonLinkBlockControl : Custom.Episerver.BaseClasses.PublicBaseResponsiveBlockControl<Custom.Episerver.Blocks.ButtonLinkBlockData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptLinks.DataSource = CurrentData.GetButtonLinks();
                rptLinks.DataBind();
            }
        }
    }
}