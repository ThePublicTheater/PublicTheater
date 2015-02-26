using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using EPiServer.Web.PropertyControls;
using EPiServer.Web.WebControls;
using System.Text.RegularExpressions;
using LinqToTwitter;
using Microsoft.Ajax.Utilities;
using Microsoft.ServiceModel.Samples;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PublicTheater.Custom.Episerver.Blocks;
using System.Web.Script.Serialization;
using PublicTheater.Custom.Episerver.Properties;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class RRBlockControl : Custom.Episerver.BaseClasses.PublicBaseBlockControl<Custom.Episerver.Blocks.RRBlockData>
    {
        protected class RRShow
        {
            public Url ShowImage;
            public List<ResourceRoomItem> Items;
            public PropertyData ShowText;
            public int ShowId;
        } 
        
        protected List<RRShow> Shows;
        protected string json;
        protected string backToImageStr = "";
        protected string opentext = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            backToImageStr = CurrentBlock.BackToShowImage != null ? CurrentBlock.BackToShowImage.ToString():"";
        
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            Shows = new List<RRShow>();
            foreach (var contentItem in CurrentBlock.RRShows.Contents)
            {
                var item = contentLoader.Get<RRShowBlockData>(contentItem.ContentLink);
                try
                {
                    item.GetItems();
                    var show = new RRShow { Items = item.GetItems(), ShowId = contentItem.ContentLink.ID, ShowImage = item.PosterImage, ShowText = item.Property["ShowText"] };
                    Shows.Add(show);

                }
                catch (Exception)
                {
                }

            }

            StringWriter sw = new StringWriter();
            if (CurrentBlock.OpenShowText != null)
            {
                PropertyLongStringControl ctrl = new PropertyLongStringControl();
                ctrl.PropertyData = CurrentBlock.Property["OpenShowText"];
                ctrl.SetupControl();
                StringWriter swa = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(swa);   
                ctrl.RenderControl(htw);
                sw.Write("<div class='openall'>" + swa.ToString() + "</div>");


            }

            foreach (var rrShow in Shows)
            {
                PropertyLongStringControl ctrl = new PropertyLongStringControl();
                ctrl.PropertyData = rrShow.ShowText;
                ctrl.SetupControl();
                StringWriter swa = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(swa);   
                ctrl.RenderControl(htw);
                sw.Write( "<div class='openshow' data-show='" + rrShow.ShowId + "'>" + swa.ToString() + "</div>");

            }
            opentext = sw.ToString();


            //Items = CurrentBlock.GetItems();
            string videoId = "";

            var blocks = Shows.SelectMany(show =>
            {
                var items = show.Items.Select((i => new
                {
                    url = !i.Image.IsNullOrWhiteSpace() ? i.Image : i.Media,
                    type = !i.Image.IsNullOrWhiteSpace() ? "image" : Custom.Episerver.Utility.Utility.GetMediaType(i.Media, out videoId),
                    id = videoId,
                    options = i.Options,
                    script = i.Script,
                    showId = show.ShowId

                })).ToList();
                
                    items.Add(new
                    {
                        url = show.ShowImage !=null? show.ShowImage.ToString() : "",
                        type = "showImage",
                        id = "",
                        options = "",
                        script = "",
                        showId = show.ShowId
                    });
                

                return items;
            });

            json = JsonConvert.SerializeObject(blocks);

        }
    }
}