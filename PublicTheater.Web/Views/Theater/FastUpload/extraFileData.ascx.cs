using System;
using EPiServer.XForms;

namespace PublicTheater.Web.Views.Theater.FastUpload
{
    public partial class extraFileData : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            EPiServer.XForms.XForm form = XForm.CreateInstance();

            var epiConfig = System.Web.Configuration.WebConfigurationManager.GetSection("episerver") as EPiServer.Configuration.EPiServerSection;
            if (epiConfig != null)
            {
                string summaryPath = "~/FileSummary.config";
                if (string.IsNullOrWhiteSpace(summaryPath))
                    return;

                form.Document.Load(Server.MapPath(summaryPath));
            }

            extraFileDataControl.FormDefinition = form;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Request.Form["submitButton"] == "submitButton")
                updateFileInfo();
        }

        private void updateFileInfo()
        {
            extraFileDataControl.SubmitForm(ChannelOptions.None);

            var currentData = extraFileDataControl.Data.Data;
        }
    }
}