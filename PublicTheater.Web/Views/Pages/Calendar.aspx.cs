using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicTheater.Web.Views.Pages
{
    public partial class Calendar : Custom.Episerver.BaseClasses.PublicBasePage<Custom.Episerver.Pages.CalendarPageData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                
            }
        }
    }
}