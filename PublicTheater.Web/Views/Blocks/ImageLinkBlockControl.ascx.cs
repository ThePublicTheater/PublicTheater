using System;
using EPiServer.Web;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class ImageLinkBlockControl : Custom.Episerver.BaseClasses.PublicBaseResponsiveBlockControl<Custom.Episerver.Blocks.ImageLinkBlockData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hlImage.ImageUrl = CurrentBlock.ImageUrl == null ? string.Empty : CurrentBlock.ImageUrl.ToString();
                hlImage.NavigateUrl = CurrentBlock.LinkUrl == null ? string.Empty : CurrentBlock.LinkUrl.ToString();
                hlImage.Text = hlImage.ToolTip = CurrentBlock.ImageTitle;
            }
        }
    }
}