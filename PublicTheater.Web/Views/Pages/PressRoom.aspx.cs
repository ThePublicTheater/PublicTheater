using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Filters;
using EPiServer.ServiceLocation;
using PublicTheater.Custom.Episerver.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PublicTheater.Custom.Episerver.Utility;

namespace PublicTheater.Web.Views.Pages
{
    public partial class PressRoom : Custom.Episerver.BaseClasses.PublicBasePage<Custom.Episerver.Pages.PressRoomPageData>
    {
        private IEnumerable<PressItemPageData> _allPressItems;
        public IEnumerable<PressItemPageData> AllPressItems
        {
            get
            {
                return _allPressItems ?? (_allPressItems = CurrentPageLink.SearchPageByType<PressItemPageData>().Where(p => p.VisibleInMenu));
            }
        }

        public IEnumerable<PressItemPageData> FilteredPressItems
        {
            get
            {
                var filteredPressItems = AllPressItems;

                if (ddlVenues.SelectedValue != string.Empty)
                {
                    filteredPressItems = filteredPressItems.Where(p => p.VenueName == ddlVenues.SelectedValue);
                }
                if (ddlYears.SelectedValue != string.Empty)
                {
                    filteredPressItems = filteredPressItems.Where(p => p.ReleaseDate.HasValue && p.ReleaseDate.Value.Year == int.Parse(ddlYears.SelectedValue));
                }
                return filteredPressItems.OrderByDescending(item=>item.ReleaseDate);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ddlVenues.SelectedIndexChanged += new EventHandler(DDLVenuesSelectedIndexChanged);
            ddlYears.SelectedIndexChanged += new EventHandler(DDLVenuesSelectedIndexChanged);

            if (!IsPostBack)
            {
                ddlVenues.DataSource = AllPressItems.Where(p => !string.IsNullOrEmpty(p.VenueName)).Select(p => p.VenueName).Distinct().OrderBy(n => n);
                ddlVenues.DataBind();
                ddlVenues.Items.Insert(0, new ListItem("All Venues", string.Empty));

                ddlYears.DataSource = AllPressItems.Where(p => p.ReleaseDate.HasValue).Select(p => p.ReleaseDate.Value.Year).Distinct().OrderByDescending(y => y);
                ddlYears.DataBind();
                ddlYears.Items.Insert(0, new ListItem("All Years", string.Empty));

                BindPressItems();
            }
        }

        void DDLVenuesSelectedIndexChanged(object sender, EventArgs e)
        {
            BindPressItems();
        }

        private void BindPressItems()
        {
            rptPressItemsDisplay.DataSource = FilteredPressItems;
            rptPressItemsDisplay.DataBind();
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            var inputPassword = Password.Text;
            if (CurrentPage.Password == null || inputPassword == CurrentPage.Password)
            {
                // go to press-photo
                Response.Redirect(Utility.GetFriendlyUrl(CurrentPage.PressPhotoPage));
            }
            else
            {
                propPasswordInvalidMessage.Visible = true;
            }
        }
    }
}