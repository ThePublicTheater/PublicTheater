using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web;
using PublicTheater.Custom.Episerver;

namespace PublicTheater.Web.Views.Blocks
{
    [TemplateDescriptor(Tags = new[] { Constants.RenderingTags.PDP })]
    public partial class PDPTicketBlockControl : TicketBlockControl
    {
        
    }
}