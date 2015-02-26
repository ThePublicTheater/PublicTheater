using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web;
using EPiServer.Web.WebControls;
using PublicTheater.Custom.Episerver.BaseClasses;
using PublicTheater.Custom.Episerver.Pages;

namespace PublicTheater.Web.Views.Pages
{
    [TemplateDescriptor(Path = "~/Views/Pages/BlankUtilPage.aspx")]
    public partial class BlankUtilPage : Custom.Episerver.BaseClasses.PublicBasePage<Custom.Episerver.Pages.BlankUtilPageData>
    {
    }
}