using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Filters;
using EPiServer.ServiceLocation;
using PublicTheater.Custom.Episerver;
using PublicTheater.Custom.Episerver.BaseClasses;
using PublicTheater.Custom.Episerver.Pages;
using PublicTheater.Custom.Episerver.Utility;
using PublicTheater.Custom.Models;
using PublicTheater.Custom.WatchAndListen;

namespace PublicTheater.Web.Views.Pages
{
    public partial class WatchListen : PublicBasePage<WatchListenPageData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptCategories.DataSource = CurrentPage.Category.Select(Category.Find).OrderBy((Category arg)=>{return arg.SortOrder;});
                rptCategories.DataBind();
                
                
            }
            mediaCenterData.Value = CurrentPage.GetPlayerData();
        }
    }
}