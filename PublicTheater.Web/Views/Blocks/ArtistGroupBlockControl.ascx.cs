using System;
using System.Web.UI.WebControls;
using EPiServer.Web;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class ArtistGroupBlockControl : Custom.Episerver.BaseClasses.PublicBaseResponsiveBlockControl<Custom.Episerver.Blocks.ArtistGroupBlockData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rptArtistGroups.ItemDataBound += RptArtistGroupsItemDataBound;
            if (!IsPostBack)
            {
                rptArtistGroups.DataSource = CurrentData.GetArtistGroups();
                rptArtistGroups.DataBind();

            }
        }

        void RptArtistGroupsItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var artistGroup = e.Item.DataItem as ArtistGroup;
                var rptArtists = e.Item.FindControl("rptArtists") as Repeater;

                if (artistGroup != null && rptArtists != null)
                {
                    rptArtists.DataSource = artistGroup.GetArtistList();
                    rptArtists.DataBind();
                }
            }

        }

    }
}