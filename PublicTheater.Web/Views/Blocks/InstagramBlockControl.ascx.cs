using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.WebControls;
using PublicTheater.Custom.Episerver.Blocks;
using PublicTheater.Web.Services;

namespace PublicTheater.Web.Views.Blocks
{

    public partial class InstagramBlockControl : Custom.Episerver.BaseClasses.PublicBaseResponsiveBlockControl<InstagramBlockData> 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var items = Instagram.GetPhotosByUserNameArray(CurrentBlock.UserName);
            rptSlideShow.DataSource = items;
            rptSlideShow.DataBind();
        }
    }
}