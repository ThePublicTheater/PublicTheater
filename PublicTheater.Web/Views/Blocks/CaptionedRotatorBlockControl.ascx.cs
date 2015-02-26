using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class CaptionedRotatorBlockControl : Custom.Episerver.BaseClasses.PublicBaseResponsiveBlockControl<Custom.Episerver.Blocks.CaptionedRotatorBlockData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindContent();
            }

            
        }

        private void BindContent()
        {
            ltrRotatorTitle.Text = CurrentBlock.RotatorTitle;

            var rotatorItems = CurrentBlock.GetSubHomeRotatorItems();

            rptRotatorItems.ItemDataBound += rptRotatorItems_ItemDataBound;
            rptRotatorItems.DataSource = rotatorItems;
            rptRotatorItems.DataBind();
        }

        void rptRotatorItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var captionImageLink = e.Item.DataItem as CaptionedImageLink;

            if (captionImageLink == null)
                return;
            
            var img = e.Item.FindControl("img") as Image;
            var captionTitle = e.Item.FindControl("ltrCaptionTitle") as Literal;
            var captionDescription = e.Item.FindControl("ltrCaptionDescription") as Literal;
            var lnkTickets = e.Item.FindControl("lnkTickets") as HyperLink;

            if (img != null)
            {
                img.ImageUrl = captionImageLink.ImageUrl;
            }

            if (captionDescription != null)
            {
                captionDescription.Text = captionImageLink.CaptionDescription;
            }
            if (lnkTickets != null)
            {
                lnkTickets.NavigateUrl = captionImageLink.LinkUrl;
                if(!string.IsNullOrEmpty(captionImageLink.CaptionTitle))
                {
                    lnkTickets.Text = captionImageLink.CaptionTitle;
                }
            }
        }
    }
}