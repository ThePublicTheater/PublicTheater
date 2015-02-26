using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.WebControls;
using PublicTheater.Custom.Episerver.Blocks;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class BlockContainerBlock : Custom.Episerver.BaseClasses.PublicBaseBlockControl<Custom.Episerver.Blocks.BlockContainerBlockData>
    
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}