using System;
using System.IO;
using System.Web.UI;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.PropertyControls;
using PublicTheater.Custom.Episerver.Blocks;
using PublicTheater.Custom.Episerver.Pages;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class SwitchToBlockControl : Custom.Episerver.BaseClasses.PublicBaseResponsiveBlockControl<Custom.Episerver.Blocks.SwitchToBlockData>
    {
        protected string hlImage= "";
        protected string newbody = "";
        protected string newtitle = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            hlImage = CurrentBlock.ImageUrl == null ? string.Empty : CurrentBlock.ImageUrl.ToString();
            try
            {
                
                PropertyLongStringControl ctrl = new PropertyLongStringControl();
                var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
                var item = contentLoader.Get<IContent>(CurrentBlock.LinkUrl);
                newtitle = item.Property["PageName"].ToWebString();
                ctrl.PropertyData = item.Property["MainBody"];
                ctrl.SetupControl();
                StringWriter swa = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(swa);
                ctrl.RenderControl(htw);
                newbody = swa.ToString();
            }
            catch (Exception)
            {
            }
            


        }
    }
}