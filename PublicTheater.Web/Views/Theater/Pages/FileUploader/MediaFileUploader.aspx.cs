using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adage.EPiMultiFileUpload.Code
{
    public partial class MediaFileUploader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["d"] == null)
                Session["fileUploaderDirectory"] = "~/Global/";
            else
                Session["fileUploaderDirectory"] = Request.QueryString["d"];

            if (Request.QueryString["returnto"] != null)
            {
                returnTo.NavigateUrl = Request.QueryString["returnto"];
                returnTo.Visible = true;
            }

            Page.ClientScript.RegisterClientScriptInclude("jquery", GetResourcePath("Adage.EPiMultiFileUpload.scripts.jquery.min.js"));
        }

        protected virtual string GetResourcePath(string path)
        {
            return this.Page.ClientScript.GetWebResourceUrl(typeof(FileTransferHandler), path);
        }
    }
}