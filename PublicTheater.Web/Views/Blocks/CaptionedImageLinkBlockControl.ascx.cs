using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class CaptionedImageLinkBlockControl : Custom.Episerver.BaseClasses.PublicBaseResponsiveBlockControl<Custom.Episerver.Blocks.CaptionedImageLinkBlockData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var captionedImageLink = CurrentBlock.CaptionedImageLinkTyped;

                if (captionedImageLink == null)
                    return;

                image.ImageUrl = captionedImageLink.ImageUrl;
                lnkMoreInfo.Text = captionedImageLink.CaptionTitle;
                lnkMoreInfo.NavigateUrl = captionedImageLink.LinkUrl;
            }
        }
    }
}