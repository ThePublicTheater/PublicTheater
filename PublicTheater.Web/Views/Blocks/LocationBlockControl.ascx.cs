using System;
using EPiServer.Web;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class LocationBlockControl : Custom.Episerver.BaseClasses.PublicBaseResponsiveBlockControl<Custom.Episerver.Blocks.LocationBlockData>
    {
        bool pageDataLoaded = false;

        PublicTheater.Custom.Episerver.Pages.LocationPageData _currentLocationPage;
        protected PublicTheater.Custom.Episerver.Pages.LocationPageData CurrentLocationPage
        {
            get
            {
                if (pageDataLoaded == false)
                {
                    pageDataLoaded = true;
                    _currentLocationPage = CurrentBlock.GetLocationPageData();
                }
                return _currentLocationPage;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                if (CurrentLocationPage == null)
                {
                   this.Visible = false;
                }

                if (CurrentBlock.LargeView == Custom.Episerver.Enums.BlockLargeView.Full)
                {
                    this.pnlLargeView.Visible = true;
                }
                else {
                    // show half view as default
                    this.pnlHalfView.Visible = true;
                } 
            }
        }
    }
}