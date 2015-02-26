using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.WebControls;
using System.Text.RegularExpressions;

namespace PublicTheater.Web.Views.Blocks
{
    public partial class MediaItemBlockControl : Custom.Episerver.BaseClasses.PublicBaseResponsiveBlockControl<Custom.Episerver.Blocks.MediaItemBlockData>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (CurrentData.MediaType)
            {
                case Custom.Episerver.Enums.MediaType.vimeo:
                    if (null == CurrentData.MediaURL)
                        break;
                    var VimeoRegex = new Regex(@"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    Match vimeoMatch = VimeoRegex.Match(CurrentData.MediaURL.ToString());
                    if (vimeoMatch.Success)
                    {
                        VimeoIframe.Attributes.Add("src", "//player.vimeo.com/video/" + vimeoMatch.Groups[1].Value + "?portrait=0&byline=0&badge=0&title=0");

                        Vimeo.Visible = true;
                    }
                    break;
                case Custom.Episerver.Enums.MediaType.youtube:
                    if (null == CurrentData.MediaURL)
                        break;
                    var YoutubeRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    Match youtubeMatch = YoutubeRegex.Match(CurrentData.MediaURL.ToString());
                    if (youtubeMatch.Success)
                    {
                        YoutubeIframe.Attributes.Add("src", "//www.youtube.com/embed/" + youtubeMatch.Groups[1].Value + "?showinfo=0&rel=0&modestbranding=1&autohide=1&iv_load_policy=3");

                        Youtube.Visible = true;
                    }

                    break;
                case Custom.Episerver.Enums.MediaType.soundCloud:

                    if (null == CurrentData.MediaURL)
                        break;
                    SoundCloudIframe.Attributes.Add("src", String.Format(@"//w.soundcloud.com/player/?url={0}&color=ef4135&amp;auto_play=false&amp;show_artwork=false", CurrentData.MediaURL.ToString()));
                    SoundCloud.Visible = true;
                    break;
                case Custom.Episerver.Enums.MediaType.audio:
                    if (null == CurrentData.MediaURL)
                        break;
                    AudioElement.Attributes.Add("src", CurrentData.MediaURL.ToString());
                    AudioElementTitle.InnerHtml = CurrentData.MediaTitle;
                    AudioElementSubTitle.InnerHtml = CurrentData.MediaSubtitle;
                    Audio.Visible = true;
                    break;
            }
        }
    }
}