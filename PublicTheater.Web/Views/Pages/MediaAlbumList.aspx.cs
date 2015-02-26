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
    public partial class MediaAlbumList : Custom.Episerver.BaseClasses.PublicBasePage<MediaAlbumListPageData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateAlbumList();
                PopulateChildAlbumList();
            }
        }

        

        private void PopulateAlbumList()
        {
            if (CurrentPage.CurrentAlbumList.Any() == false)
            {
                rptAlbumItems.Visible = false;
                return;
            }
            rptAlbumItems.DataSource = CurrentPage.CurrentAlbumList;
            rptAlbumItems.DataBind();
        }
        private void PopulateChildAlbumList()
        {
            if (CurrentPage.ChildAlbumList.Any() == false)
            {
                rptChildAlbumItems.Visible = false;
                return;
            }
            rptChildAlbumItems.DataSource = CurrentPage.ChildAlbumList;
            rptChildAlbumItems.DataBind();
        }
    }
}