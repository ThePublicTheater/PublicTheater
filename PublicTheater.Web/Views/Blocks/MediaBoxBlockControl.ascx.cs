using System;
using Elmah.ContentSyndication;
using EPiServer.Search;
using EPiServer.Web;
using System.Linq;
using EPiServer.XForms.WebControls;
using LinqToTwitter;
using Microsoft.Ajax.Utilities;
using PublicTheater.Custom.Episerver;
using System.Web.UI;
using System.Web;
using PublicTheater.Custom.Episerver.Properties;
using PublicTheater.Custom.Models;
using PublicTheater.Web.gift;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class MediaBoxBlockControl : Custom.Episerver.BaseClasses.PublicBaseBlockControl<Custom.Episerver.Blocks.MediaBoxBlockData>
    {
        protected EPiServer.Core.IContent selected = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (CurrentBlock.MediaItems == null)
                {
                    pnlMedia.Visible = false;
                    return;
                }
                var mediaItems = CurrentBlock.GetMediaItems();
                //var albums = mediaItems.Where(item => !item.Album.IsNullOrWhiteSpace()).DistinctBy(item => item.Album).ToDictionary(item=>item.Album);
                
                var complete = mediaItems.DistinctBy(
                    item =>
                        item.Album.IsNullOrWhiteSpace()
                            ? "not album here123412341234" + mediaItems.IndexOf(item)
                            : item.Album)
                            .Select(
                                item =>
                                {
                                    if (item.Album.IsNullOrWhiteSpace())
                                    {
                                        return new 
                                        {
                                            Caption=item.Caption,
                                            LinkUrl=item.LinkUrl,
                                            ImageUrl = item.ImageUrl

                                        };
                                    }
                                    var albumItems = "Album:" + String.Join("|", mediaItems.Where(i => i.Album == item.Album)
                                                         .Select(i => i.LinkUrl + "~" + i.Caption));
                                    var result = new
                                    {
                                        Caption = item.Album,
                                        LinkUrl = albumItems,
                                        ImageUrl = item.ImageUrl

                                    };
                                    return result;
                                });
                


                
                rptRelatedMediaItems.DataSource = complete;

                rptRelatedMediaItems.DataBind();

            }
        }
       
    }
}