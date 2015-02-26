using System;
using EPiServer.UI;

namespace MakingWaves.FastUpload.FastUpload
{
    public partial class FastUploadDialog : SystemPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //Check that the user has edit access
            if (!(EPiServer.Security.PrincipalInfo.HasEditorAccess || EPiServer.Security.PrincipalInfo.HasAdminAccess))
            {
                Response.Status = "401 Unauthorized";
                Response.StatusCode = 401;
                Response.Write("Access Denied");
                Response.End();
            }
            
            this.MasterPageFile = ResolveUrlFromUI("MasterPages/EPiServerUI.master");
            this.SystemMessageContainer.Heading = "FastUpload";
        } 
    }
}