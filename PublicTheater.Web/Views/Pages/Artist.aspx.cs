using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicTheater.Web.Views.Pages
{
    public partial class Artist : Custom.Episerver.BaseClasses.PublicBasePage<PublicTheater.Custom.Episerver.Pages.ArtistPageData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(CurrentPage.Picture!=null)
                {
                    imgHeadShot.Visible = true;
                    imgHeadShot.ImageUrl = CurrentPage.Picture.ToString();
                }
            }
        }
    }
}