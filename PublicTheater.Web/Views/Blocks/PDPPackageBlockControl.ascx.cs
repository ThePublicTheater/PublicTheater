using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web;
using PublicTheater.Custom.Episerver;
using PublicTheater.Custom.Episerver.Utility;

namespace PublicTheater.Web.Views.Blocks
{
    [TemplateDescriptor(Tags = new[] { Constants.RenderingTags.PDP })]
    public partial class PDPPackageBlockControl : TicketBlockControl
    {
        public override void GetPerformanceAvailabilityFromPDP()
        {
            if (CurrentData.PackageLinkURL != null && CurrentData.PackageLinkURL.Trim().Length > 0)
            {
                tixLink.HRef = CurrentData.PackageLinkURL;
                if (CurrentData.PackageLinkText != null && CurrentData.PackageLinkText.Trim().Length > 0)
                    tixLink.InnerText = CurrentData.PackageLinkText;
            }
            else
            {
                tixLink.Visible = false;
            }
            
        }
    }
}