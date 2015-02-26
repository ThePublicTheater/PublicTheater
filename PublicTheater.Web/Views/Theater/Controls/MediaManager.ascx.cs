using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Adage.Theater.RelationshipManager;
using EPiServer;
using EPiServer.PlugIn;
using EPiServer.UI.WebControls;
using EPiServer.Web.Hosting;
using EPiServer.Web.WebControls;
using Adage.Theater.RelationshipManager.Utilities;

namespace PublicTheater.Web.Views.Theater.Controls
{
    [GuiPlugIn(DisplayName = "MediaManager", Url = "~/Views/Theater/Properties/MediaManager.ascx")]
    public partial class MediaManager : UserControlBase
    {
        private string ParentFolder
        {
            get
            {
                if (ViewState["ParentFolder"] == null)
                    return String.Empty;

                return (string)ViewState["ParentFolder"];
            }
            set { ViewState["ParentFolder"] = value; }
        }

        private string CurrentPath
        {
            get
            {
                if (ViewState["CurrentPath"] == null)
                    return String.Empty;

                return (string)ViewState["CurrentPath"];
            }
            set { ViewState["CurrentPath"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            FileTree.TreeNodeExpanded += FileTree_TreeNodeExpanded;
            FileTree.TreeNodeCollapsed += FileTree_TreeNodeCollapsed;

            lvFiles.ItemDataBound += lvFiles_ItemDataBound;
            lvFiles.ItemCommand += lvFiles_ItemCommand;

            UpdateAll.Click += UpdateAll_Click;

            if (HttpContext.Current.Items["MediaManagerScript"] == null)
            {
                HttpContext.Current.Items["MediaManagerScript"] = true;
                phScript.Visible = true;
            }

            HfMediaManagerURL.Value = String.Concat(Request.Url.AbsoluteUri.Split('?')[0], "/AddMedia");

            if (!Page.IsPostBack)
            {
                SupportedMediaType.Text = String.Format("Supported Media Types: {0}", String.Join(", ", MediaExtension.GetAllSupportedExtensions().ToArray()));

                if (Request.QueryString["toFolder"] != null)
                {
                    BindStartPath();
                }
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindFiles();
            }

        }


        protected void BindStartPath()
        {
            CurrentPath = Request.QueryString["toFolder"];
            FileTree.DataBind();

            var newPath = new StringBuilder();
            newPath.Append("/Global");
            var currentNode = FileTree.Nodes[0];
            foreach (string eachChildName in CurrentPath.Split('/'))
            {
                foreach (TreeNode eachNode in currentNode.ChildNodes)
                {

                    if (eachNode.Text.ToLower() == eachChildName.ToLower())
                    {
                        //Find closest Folder to the start path
                        newPath.Append(string.Format("/{0}", eachChildName));

                        eachNode.Expand();
                        currentNode = eachNode;
                        break;
                    }
                }
                if (eachChildName == string.Empty)
                    continue;
                // if we didn't find the next node down then break out
                if (currentNode.Text != eachChildName)
                    break;
            }
            CurrentPath = newPath.ToString();
            BindFiles();
        }

        void UpdateAll_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            UpdateAllMedia();

            BindFiles();
        }

        List<int> checkMediaIndexes = new List<int>();

        private void UpdateAllMedia()
        {
            foreach (ListViewItem item in lvFiles.Items)
            {
                var cbUpdate = item.FindControl("cbUpdate") as CheckBox;

                if (cbUpdate == null || !cbUpdate.Checked)
                    continue;

                var HfMediaGuid = item.FindControl("HfImageGuid") as HiddenField;
                if (HfMediaGuid == null)
                    continue;

                checkMediaIndexes.Add(item.DataItemIndex);
                UpdateCurrentItem(HfMediaGuid);
            }

            AdageTheaterRelationshipManagerEntities.Current.SaveChanges();
        }

        private void UpdateCurrentItem(HiddenField HfMediaGuid)
        {


            var PropertyManager = (MediaPropertyManager)FindControl("HeaderPropertyManager");

            PropertyManager.UpdateProperties(HfMediaGuid);

        }

        void FileTree_TreeNodeCollapsed(object sender, TreeNodeEventArgs e)
        {
            BindFiles(e);
        }

        void FileTree_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            if (Page.IsPostBack)
                BindFiles(e);
        }

        private void BindFiles(TreeNodeEventArgs e)
        {
            tbPageIndex.Text = "1";
            CurrentPath = e.Node.ValuePath;
            BindFiles();
        }

        private void BindFiles()
        {
            if (string.IsNullOrEmpty(CurrentPath))
                CurrentPath = "Global";
            var path = String.Concat("/", CurrentPath, "/");

            var directory = HostingEnvironment.VirtualPathProvider.GetDirectory(path) as VirtualDirectory;

            if (directory == null) return;

            int pageIndex = 1;
            int.TryParse(tbPageIndex.Text, out pageIndex);

            int fileCount = directory.Files.Cast<VersioningFile>().Count();
            int pageSize = Utility.Media_Manager_PageSize;
            var totalPages = Math.Ceiling((double)fileCount / pageSize);
            hfTotalPages.Value = totalPages.ToString();

            tbPageIndex.Text = pageIndex.ToString();
            lblPageCount.Text = totalPages.ToString();
            pnlPager.Visible = (totalPages > 1);

            lblCurrentPath.Text = String.Concat("<strong>Current Path: </strong>", path);

            lvFiles.DataSource = directory.Files.Cast<VersioningFile>().Skip((pageIndex - 1) * pageSize).Take(pageSize);
            lvFiles.DataBind();
        }

        void lvFiles_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.Item.ItemType != ListViewItemType.DataItem)
                return;

            if (e.CommandName.ToUpper() != "ADD") return;

            var HfImageGuid = e.Item.FindControl("HfImageGuid") as HiddenField;
            if (HfImageGuid == null)
                return;

            var ucPropertyManager = e.Item.FindControl("ucPropertyManager") as MediaPropertyManager;
            if (ucPropertyManager == null)
                return;

            ucPropertyManager.UpdateProperties(HfImageGuid);
            //lvFiles_ItemDataBound(sender,new ListViewItemEventArgs(e.Item));
            BindFiles();
        }

        void lvFiles_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType != ListViewItemType.DataItem)
                return;

            VersioningFile file = e.Item.DataItem as VersioningFile;

            if (file == null) return;

            var mediaType = e.Item.FindControl("mediaType") as HtmlGenericControl;

            if (!Adage.Theater.RelationshipManager.Helpers.Utility.IsValidMedia(file.Name))
            {
                mediaType.Visible = false;
                return;
            }

            bool isImage = Adage.Theater.RelationshipManager.Helpers.Utility.IsImage(file.Name);

            if (!isImage)
            {
                var divThumbnail = e.Item.FindControl("divThumbnail") as HtmlGenericControl;
                if (divThumbnail != null)
                {
                    divThumbnail.Visible = true;

                    var thumbprop = e.Item.FindControl("thumbprop") as Property;
                    var propertyURL = new EPiServer.SpecializedProperties.PropertyUrl();
                    thumbprop = new Property(propertyURL);
                    thumbprop.EditMode = true;
                    thumbprop.PropertyName = "url";
                }
            }

            var litMediaName = e.Item.FindControl("litMediaName") as Literal;
            if (litMediaName != null) litMediaName.Text = file.Name;

            var imageGuid = e.Item.FindControl("HfImageGuid") as HiddenField;
            if (imageGuid != null) imageGuid.Value = file.Guid.ToString();

            var media = EPiMedia.GetEPiMediaByMediaGuid(file.Guid);

            var epiImage = e.Item.FindControl("epiImage") as Image;
            if (epiImage != null)
            {
                var imgPath = (isImage || media == null) ? file.VirtualPath : media.EPiMediaThumbPath;
                epiImage.Attributes["onload"] = String.Format("javascript:LoadImage(this, '{0}')", ResolveClientUrl(imgPath));

                epiImage.Visible = !string.IsNullOrEmpty(imgPath);
            }



            //if (epiImage != null) epiImage.ImageUrl = file.VirtualPath;

            var cbUpdate = e.Item.FindControl("cbUpdate") as CheckBox;



            var parentFolderPath = file.Parent.VirtualPath.Split("/".ToCharArray(),
                                                                     StringSplitOptions.RemoveEmptyEntries);

            var PropertyManager = e.Item.FindControl("ucPropertyManager") as MediaPropertyManager;
            if (PropertyManager == null)
                throw new ApplicationException("Could not find ucMediaPropertyManager");
            PropertyManager.BindProperties(media, parentFolderPath);
            if (media == null)
            {
                var AddOrSaveButton = e.Item.FindControl("AddButton") as ToolButton;
                AddOrSaveButton.Text = "Add";
            }

            if (cbUpdate != null) cbUpdate.Checked = true;

            if (checkMediaIndexes.Contains(e.Item.DataItemIndex))
            {
                if (cbUpdate != null)
                    cbUpdate.Checked = true;
            }

            var AddButton = e.Item.FindControl("AddButton") as EPiServer.UI.WebControls.ToolButton;
            AddButton.Attributes["itemIndex"] = e.Item.DataItemIndex.ToString();
        }

        protected void FileTree_TreeNodeDataBound(object sender, System.Web.UI.WebControls.TreeNodeEventArgs e)
        {
            VirtualFileBase virtualFile = e.Node.DataItem as VirtualFileBase;
            if (virtualFile.IsDirectory)
            {
                e.Node.SelectAction = TreeNodeSelectAction.Expand;
            }
            else
            {
                //e.Node.NavigateUrl = e.Node.DataPath;
            }
            e.Node.Text = Server.HtmlEncode(virtualFile.Name);
        }

        protected void btnGoToPage_Click(object sender, EventArgs e)
        {
            BindFiles();
        }
    }
}
