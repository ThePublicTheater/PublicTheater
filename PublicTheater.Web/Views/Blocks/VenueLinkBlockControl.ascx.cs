using System;
using EPiServer.Web;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class VenueLinkBlockControl : Custom.Episerver.BaseClasses.PublicBaseResponsiveBlockControl<Custom.Episerver.Blocks.VenueLinkBlockData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptVenues.DataSource = CurrentData.GetVenueLinks();
                rptVenues.DataBind();
            }
        }
    }
}