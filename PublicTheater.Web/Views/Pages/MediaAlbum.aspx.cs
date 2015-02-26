using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Filters;
using EPiServer.ServiceLocation;
using PublicTheater.Custom.Episerver.Pages;
using PublicTheater.Custom.WatchAndListen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicTheater.Web.Views.Pages
{
    public partial class MediaAlbum : Custom.Episerver.BaseClasses.PublicBasePage<MediaAlbumPageData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulatePhotoList();
            }
        }

        private void PopulatePhotoList()
        {
            if (CurrentPage.AlbumImages.Any() == false)
            {
                rptPhotoItems.Visible = false;
                return;
            }
            rptPhotoItems.ItemDataBound += rptPhotoItems_ItemDataBound;
            rptPhotoItems.DataSource = CurrentPage.AlbumImages;
            rptPhotoItems.DataBind();
        }



        void rptPhotoItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var photo = e.Item.DataItem as MediaItemPageData;

                var mediaLink = e.Item.FindControl("mediaItemLink") as HyperLink;
                if (mediaLink != null && photo != null)
                {
                    mediaLink.NavigateUrl = photo.FriendlyURL;
                }

                var mediaImage = e.Item.FindControl("mediaItemImage") as Image;
                if (mediaImage != null && photo != null)
                {
                    mediaImage.ImageUrl = photo.CoverImage;
                }

                var mediaItemTitle = e.Item.FindControl("mediaItemTitle") as Literal;
                if (mediaItemTitle != null && photo != null)
                {
                    mediaItemTitle.Text = photo.MediaTitle;
                }
            }
        }


    }
}