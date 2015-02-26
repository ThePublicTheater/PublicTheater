using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer;

namespace PublicTheater.Web.Views.Pages
{
    public partial class Contribute : TheaterTemplate.Web.Views.Pages.Contribute
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindSmallBannerImage();
            }
        }

        private void BindSmallBannerImage()
        {
            var prop = CurrentPage.Property["SmallBanner"];
            if (prop == null)
            {
                imgDonationSmallBanner.Visible = false;
            }
            else
            {
                imgDonationSmallBanner.ImageUrl = prop.Value.ToString();
            }
        }
    }
}