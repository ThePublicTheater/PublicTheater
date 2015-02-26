using System;
using EPiServer.Web;
using PublicTheater.Custom.Episerver;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class RotatorBlockControl : Custom.Episerver.BaseClasses.PublicBaseResponsiveBlockControl<Custom.Episerver.Blocks.RotatorBlockData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptSlideShow.DataSource = CurrentData.GetImageLinks();
                rptSlideShow.DataBind();
            }
        }
    }
}