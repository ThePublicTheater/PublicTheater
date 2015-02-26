using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicTheater.Web.Views.Pages
{
    public partial class Library : Custom.Episerver.BaseClasses.PublicBasePage<Custom.Episerver.Pages.LibraryPageData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindMenuItems();
        }

        private void BindMenuItems()
        {
            menuItems.DataSource = CurrentPage.DinnerItems.GetAccordionItems();
            menuItems.DataBind();

            drinkItems.DataSource = CurrentPage.DrinkItems.GetAccordionItems();
            drinkItems.DataBind();
        }
    }
}