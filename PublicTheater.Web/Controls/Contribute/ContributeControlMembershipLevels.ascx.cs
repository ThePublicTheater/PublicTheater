using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheaterTemplate.Shared.EpiServerProperties;

namespace PublicTheater.Web.Controls.ContributeControls
{
    /// <summary>
    /// Contribute Control Membership Levels - Binds membership levels from the MembershipLevelsList
    /// </summary>
    public partial class ContributeControlMembershipLevels : TheaterTemplate.Web.Controls.ContributeControls.ContributeControlMembershipLevels
    {
        /// <summary>
        /// Page Load Function - Binds membership levels
        /// </summary>
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }

        /// <summary>
        /// Handles the binding of membership levels to a given category
        /// </summary>
        protected override void rptMembershipLevelCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                var categoryLevelList = e.Item.DataItem as MembershipLevelItemList;
                if (categoryLevelList != null)
                {
                    var categoryTitle = (Literal)e.Item.FindControl("CategoryTitle");
                    categoryTitle.Text = (string)categoryLevelList.PropertyCollection[0].Value ?? string.Empty;

                    var rptMembershipLevels = (Repeater)e.Item.FindControl("rptMembershipLevels");
                    rptMembershipLevels.ItemDataBound += new RepeaterItemEventHandler(rptMembershipLevels_ItemDataBoundCustom);
                    rptMembershipLevels.DataSource = categoryLevelList.PropertyCollection.Where(item => item is MembershipLevelItem);
                    rptMembershipLevels.DataBind();
                }
            }
        }

        /// <summary>
        /// Handles each membership level bound to the funds repeater by populating fields
        /// </summary>
        protected void rptMembershipLevels_ItemDataBoundCustom(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var levelItem = e.Item.DataItem as MembershipLevelItem;
                if (levelItem != null)
                {
                    var hdnStartingDonationLevel = (HiddenField)e.Item.FindControl("hdnStartingDonationLevel");
                    var ltLevelTitle = (Literal)e.Item.FindControl("ltLevelTitle");
                    var ltLevelDescription = (Literal)e.Item.FindControl("ltLevelDescription");
                    var ltAmountRange = (Literal)e.Item.FindControl("ltAmountRange");

                    // For some reason, CFS did not render these correctly. levelItem.LevelDescription is empty string.
                    ltLevelDescription.Text = levelItem.PropertyCollection["Membership Description"].Value.ToString();
                    ltLevelTitle.Text = levelItem.Title;
                    ltAmountRange.Text = levelItem.AmountRange;
                    hdnStartingDonationLevel.Value = levelItem.StartingLevel.ToString();
                }
            }
        }
    }
}