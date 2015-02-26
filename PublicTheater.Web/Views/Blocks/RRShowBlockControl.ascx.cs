using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.WebControls;
using System.Text.RegularExpressions;
using LinqToTwitter;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PublicTheater.Custom.Episerver.Blocks;
using System.Web.Script.Serialization;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class RRShowBlockControl : Custom.Episerver.BaseClasses.PublicBaseBlockControl<Custom.Episerver.Blocks.RRShowBlockData>
    {
        protected List<ResourceRoomItem> Items;
        protected string json;
        protected void Page_Load(object sender, EventArgs e)
        {
            Items = CurrentBlock.GetItems();
            string videoId="";
            
            var t = from i in Items
                select new
                {
                    url = !i.Image.IsNullOrWhiteSpace() ? i.Image : i.Media,
                    type = !i.Image.IsNullOrWhiteSpace() ? "image": Custom.Episerver.Utility.Utility.GetMediaType(i.Media, out videoId),
                    id = videoId, 
                    options = i.Options,
                    script = i.Script
                };
            json = JsonConvert.SerializeObject(t);
            
        }
    }
}