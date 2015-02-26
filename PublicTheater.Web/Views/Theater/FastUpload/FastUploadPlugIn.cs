using System;
using System.Web;
using System.Web.UI;
using EPiServer;
using EPiServer.PlugIn;

namespace MakingWaves.FastUpload.FastUpload
{
    [PagePlugIn(DisplayName = "FastUpload PlugIn")]
    public class FastUploadPlugIn
    {
        public FastUploadPlugIn()
        {

        }

        public FastUploadPlugIn(PageBase pageBase)
        {
            this.PageBase = pageBase;
        }

        public PageBase PageBase { get; set; }
        public static void Initialize(int bitflags)
        {
            PageBase.PageSetup += new PageSetupEventHandler(PageBase_PageSetup);
        }

        private static void PageBase_PageSetup(PageBase pageBase, PageSetupEventArgs e)
        {
            var plugin = new FastUploadPlugIn(pageBase);
            pageBase.Load += new EventHandler(plugin.pageBase_Load);
        }

        private static Control FindControlRecursive(Control rootControl, string controlId)
        {
            if (rootControl.ID == controlId) return rootControl;

            foreach (Control controlToSearch in rootControl.Controls)
            {
                var controlToReturn =
                    FindControlRecursive(controlToSearch, controlId);
                if (controlToReturn != null) return controlToReturn;
            }
            return null;
        }


        public void pageBase_Load(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            if (context == null)
                return;

            if (!((context.Request.Url.PathAndQuery.ToLower().Contains("actionwindow") || context.Request.Url.PathAndQuery.ToLower().Contains("filemanagerbrowser"))))
                return;

            if (FindControlRecursive(PageBase, "virtualPathFileManager") != null)
            {
                PageBase.ClientScript.RegisterClientScriptBlock(typeof(FastUploadPlugIn),
                                                                "plupdiv",
                                                                @"
<style type=""text/css"">
    .epi-cmsButton input.epiuploadbutton, .epi-cmsButtondisabled input.epiuploadbutton{background-image: url(/Views/Theater/FastUpload/img/FastUpload.png);} 
</style>

<script>
var currentFolder;
$(document).ready(function(){
currentFolder = $('#" + FindControlRecursive(PageBase, "curDirectory").ClientID + @"').val();
var btn = $('#ctl00_FullRegion_ctl01_virtualPathFileManager_browsemode2_UploadFileButton');

if(!btn.parent().hasClass('epi-cmsButtondisabled'))
  btn.parent().after('<span class=""epi-cmsButton"" id=""MultipleNewFiles""><input type=""button"" onclick=""OpenFastUpload(currentFolder)"" class=""epiuploadbutton epi-cmsButton-tools"" onmouseout=""EPi.ToolButton.ResetMouseDownHandler(this)"" onmouseover=""EPi.ToolButton.MouseDownHandler(this)"" title=""Upload multiple files""></span>');
else
  btn.parent().after('<span class=""epi-cmsButtondisabled"" id=""MultipleNewFiles""><input type=""button"" class=""epiuploadbutton epi-cmsButton-tools"" title=""Upload multiple files""></span>');
})

function OpenFastUpload(toFolder)
{
var url = '/Views/Theater/FastUpload/FastUploadDialog.aspx?toFolder='+toFolder;
var dialogHeight = 420;
var dialogWidth = 580;
try
{
EPi.CreateDialog(url, PlupCompleted, '', '', {width: dialogWidth, height: dialogHeight});
}
catch (ex)
{
ShowPopupBlockedInfo();
}
return false;
} 

//Callback functions for Dialog
function PlupCompleted(returnValue, callbackArguments)
{
   __doPostBack("", "");
} 

</script>
",
 false);

            }
        }
    }
}