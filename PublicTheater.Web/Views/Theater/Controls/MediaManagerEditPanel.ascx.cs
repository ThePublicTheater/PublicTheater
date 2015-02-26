using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Adage.Theater.RelationshipManager;
using EPiServer;
using EPiServer.UI.WebControls;

namespace PublicTheater.Web.Views.Theater.Controls
{
    public partial class MediaManagerEditPanel : UserControlBase
    {
        private List<MediaRelationship> currentMedia;
        List<MediaRelationship> RelatedMedia
        {
            get
            {
                if (currentMedia == null)
                {
                    currentMedia = MediaRelationship.GetMediaRelationshipByPageGuid(CurrentPage.PageGuid).OrderBy(m => m.SortIndex).ToList();
                    MediaRelationship.NormalizeSortIndex(currentMedia);
                }
                return currentMedia;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //BindMediaFiles();
            BindMassMediaUploader();
            BindMediaManager();

            btnSubmitAfterReorder.Click += new EventHandler(btnSubmitAfterReorder_Click);
            btnSaveNow.Click += new EventHandler(btnSubmitAfterReorder_Click);
            lvMediaManager.ItemCommand += new EventHandler<ListViewCommandEventArgs>(lvMediaManager_ItemCommand);
        }


        private string CurrentMediaFolder
        {
            get
            {
                var filteredPageName = CurrentPage.PageName.Replace("'", string.Empty).Replace("/", string.Empty);
                if (CurrentPage.PageTypeID == PageType.PerformanceID)
                {
                    return "/Global/Plays/" + filteredPageName;
                }
                if (CurrentPage.PageTypeID == PageType.EventsID)
                {
                    return "/Global/Events/" + filteredPageName;
                }
                if (CurrentPage.PageTypeID == PageType.SubscriptionID)
                {
                    return "/Global/Subscriptions/" + filteredPageName;
                }
                if (CurrentPage.PageTypeID == PageType.ArtistID)
                {
                    return "/Global/Artists/Actors/" + filteredPageName;
                }
                return "/Global/Plays/";
            }
        }


        private void BindMediaManager()
        {
            if (CurrentPage.PageTypeID != PageType.PerformanceID
                && CurrentPage.PageTypeID != PageType.ArtistID
                && CurrentPage.PageTypeID != PageType.EventsID
                && CurrentPage.PageTypeID != PageType.SubscriptionID)
            {
                btnToMediaManager.Visible = false;
                return;
            }

            string url = string.Format("/Views/Theater/Pages/MediaManager.aspx?toFolder={0}", CurrentMediaFolder);
            btnToMediaManager.Attributes.Add("onClick", "javascript:window.open('" + url + "'); return false;");

        }

        private void BindMassMediaUploader()
        {
            if (CurrentPage.PageTypeID == PageType.PerformanceID || CurrentPage.PageTypeID == PageType.EventsID || CurrentPage.PageTypeID == PageType.SubscriptionID)
            {
                btnMassUploader.Attributes["onclick"] = "javascript:OpenFastUpload('" + CurrentMediaFolder + "')";
            }
            else
            {
                btnMassUploader.Visible = false;
            }
        }
        private void BindMediaFiles()
        {
            lvMediaManager.ItemDataBound += new EventHandler<ListViewItemEventArgs>(lvMediaManager_ItemDataBound);
            lvMediaManager.DataSource = RelatedMedia;
            lvMediaManager.DataBind();
        }

        void lvMediaManager_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType != ListViewItemType.DataItem) return;

            MediaRelationship mediaRelationship = e.Item.DataItem as MediaRelationship;
            if (mediaRelationship == null) return;

            Image epiImage = e.Item.FindControl("epiImage") as Image;
            if (epiImage != null)
                epiImage.Attributes["onload"] = String.Format("javascript:LoadImage(this, '{0}')",

                                                     ResolveClientUrl(mediaRelationship.EPiMedia.EPiMediaThumbPath));
            Literal litMediaName = e.Item.FindControl("litMediaName") as Literal;
            if (litMediaName != null) litMediaName.Text = mediaRelationship.EPiMediaName;

            Literal litMediaType = e.Item.FindControl("litMediaType") as Literal;

            if (litMediaType != null) litMediaType.Text = mediaRelationship.EPiMedia.MediaType.MediaTypeDescription;

            var btnUp = e.Item.FindControl("BtnUp") as ToolButton;
            if (btnUp != null)
            {
                if (mediaRelationship.MediaRelationshipID == RelatedMedia.First().MediaRelationshipID) btnUp.Visible = false;
                AddEvent(btnUp, "BtnUp", e.Item.DataItemIndex, mediaRelationship.MediaRelationshipID.ToString(), new EventHandler(BtnUp_Click));
            }
            var btnDown = e.Item.FindControl("BtnDown") as ToolButton;
            if (btnDown != null)
            {
                if (mediaRelationship.MediaRelationshipID == RelatedMedia.Last().MediaRelationshipID) btnDown.Visible = false;
                AddEvent(btnDown, "BtnDown", e.Item.DataItemIndex, mediaRelationship.MediaRelationshipID.ToString(),
                         new EventHandler(BtnDown_Click));
            }

            HiddenField HfImageGuid = e.Item.FindControl("HfImageGuid") as HiddenField;
            if (HfImageGuid != null) HfImageGuid.Value = mediaRelationship.EPiMediaGuid.ToString();

            LinkButton btnSendToIndex = e.Item.FindControl("btnSendToIndex") as LinkButton;
            if (btnSendToIndex != null)
            {
                TextBox txtSendToIndex = e.Item.FindControl("txtSendToIndex") as TextBox;
                btnSendToIndex.CommandName = "SENDTOINDEX";

                //if (txtSendToIndex != null && !string.IsNullOrEmpty(txtSendToIndex.Text))
                //{
                //    btnSendToIndex.CommandArgument = string.Format("{0},{1}", mediaRelationship.EPiMediaGuid, txtSendToIndex.Text);
                //    btnSendToIndex.Command += new CommandEventHandler(btnSendToIndex_Command);
                //}
            }

            Label SortIndex = e.Item.FindControl("SortIndex") as Label;
            if (SortIndex != null) SortIndex.Text = mediaRelationship.SortIndex.ToString();

            HiddenField hfMediaRelationshipId = e.Item.FindControl("hfMediaRelationshipId") as HiddenField;
            if (hfMediaRelationshipId != null) hfMediaRelationshipId.Value = mediaRelationship.MediaRelationshipID.ToString();

            Label lblPosition = e.Item.FindControl("lblPosition") as Label;
            if (lblPosition != null) lblPosition.Text = string.Format("{0}.", (mediaRelationship.SortIndex + 1).ToString());
        }

        //protected void btnSendToIndex_Command(object sender, CommandEventArgs e)
        //{
        //    string[] args = e.CommandArgument.ToString().Split(',');

        //    if (args.Length == 2)
        //        MediaRelationship.SendMediaToIndex(args[0], args[1], RelatedMedia);
        //}

        protected void lvMediaManager_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "SENDTOINDEX":
                    var sortIndex = e.Item.FindControl("txtSendToIndex") as TextBox;
                    var mediaGuid = e.Item.FindControl("HfImageGuid") as HiddenField;
                    //var Relationship = e.Item.DataItem as MediaRelationship;
                    if (mediaGuid == null || sortIndex == null || string.IsNullOrEmpty(sortIndex.Text))
                        break;

                    MediaRelationship.SendMediaToIndex(mediaGuid.Value, sortIndex.Text, RelatedMedia);

                    break;

            }
        }


        void AddEvent(ToolButton toolButton, string toolButtonId, int itemIndex, string commandArgument, EventHandler toolButtonEventHandler)
        {
            toolButton.CommandArgument = commandArgument;
            toolButton.ID = String.Concat(toolButtonId, "_", itemIndex);
            toolButton.Click += toolButtonEventHandler;
        }

        void BtnUp_Click(object sender, EventArgs e)
        {
            SwapItem((ToolButton)sender, -1);
        }

        void BtnDown_Click(object sender, EventArgs e)
        {
            SwapItem((ToolButton)sender, 1);
        }

        private void SwapItem(ToolButton toolButton, int swapItemIncrement)
        {
            int itemId = 0;

            if (int.TryParse(toolButton.CommandArgument.ToString(), out itemId))
            {
                SwapItem(itemId, swapItemIncrement);
            }

            RebindMedia();
        }

        private void RebindMedia()
        {
            currentMedia = null;

            BindMediaFiles();
        }

        private void SwapItem(int itemId, int swapItemIncrement)
        {
            var currentItem = RelatedMedia.Single(r => r.MediaRelationshipID == itemId);
            int currentItemSortIndex = currentItem.SortIndex;
            var swapItem =
                RelatedMedia.Single(r => r.SortIndex == currentItemSortIndex + swapItemIncrement);


            currentItem.SwapSortOrder(swapItem);

            AdageTheaterRelationshipManagerEntities.Current.SaveChanges();
        }
        protected void MediaManagerDataPager_OnPreRender(object sender, EventArgs e)
        {
            BindMediaFiles();
        }

        protected void btnSubmitAfterReorder_Click(object sender, EventArgs e)
        {

            foreach (ListViewItem item in lvMediaManager.Items)
            {

                int itemId = 0;
                HiddenField hfMediaRelationshipId = item.FindControl("hfMediaRelationshipId") as HiddenField;
                if (hfMediaRelationshipId == null || !int.TryParse(hfMediaRelationshipId.Value, out itemId))
                    continue;

                var mediaRelationship = RelatedMedia.Single(r => r.MediaRelationshipID == itemId);
                if (mediaRelationship == null)
                    continue;

                int sortIndex = 0;
                HiddenField hfMediaSortIndex = item.FindControl("hfMediaSortIndex") as HiddenField;
                if (hfMediaSortIndex != null && int.TryParse(hfMediaSortIndex.Value, out sortIndex))
                {
                    mediaRelationship.SortIndex = sortIndex;
                }

            }

            AdageTheaterRelationshipManagerEntities.Current.SaveChanges();
            RebindMedia();
        }
    }
}
