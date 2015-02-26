<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MediaBoxBlockControl.ascx.cs"
    Inherits="PublicTheater.Web.Views.Blocks.MediaBoxBlockControl" %>
<%@ Import Namespace="System.Activities.Statements" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Adage.Theater.RelationshipManager.Helpers" %>
<%@ Import Namespace="Elmah.ContentSyndication" %>
<%@ Import Namespace="Microsoft.Ajax.Utilities" %>
<style type="text/css">
    /* Necessary CSS */
    .slider {
        overflow: hidden;
        position: relative;
        margin-bottom: inherit;
        height: 100px;
    }

        .slider ul {
            margin: 0;
            padding: 0;
        }

        .slider li {
            float: left;
            margin: 0 5px 0 0;
            list-style: none;
        }



    .lemmon-controls .lemmon-prev {
        float: left;
        margin-left: 0px;
    }



    .lemmon-controls .lemmon-next {
        float: right;
        width: 20px;
        margin-right: 0px;
    }

    .centered_arrow {
        position: absolute;
        margin: auto;
        bottom: 0;
        top: 0px;
        opacity: .5;
        width: auto !important; 
    }
    .centered_arrow:hover {
        opacity: 1;
    }
    /* IE6 issues */
    .slider ul {
        width: 100%;
    }

    .mediaBoxCaption {
        display: inline-block;
        width: 100%;
        text-align: right;
    }
    .sliderSelected img {
        opacity: .3;
    }
</style>
<%--<asp:ScriptManagerProxy runat="server" />--%>
<script src="/Static/backbone/lib/underscore.js"></script>
<script type="text/javascript">
    window.switchToMedia = function () { console.log("nothin"); };

    $(function () {

        _.templateSettings = {
            evaluate: /\[\[(.+?)\]\]/g,
            interpolate: /\{\{(.+?)\}\}/g
        };

        youtubeTemplate = _.template("<div class='videoWrapper'><iframe src='//www.youtube.com/embed/{{data.id}}?showinfo=0&rel=0&modestbranding=1' frameborder='0' allowfullscreen></iframe></div><span class='smallGrayText mediaBoxCaption'>{{data.caption}}</span>");

        vimeoTemplate = _.template("<div class='videoWrapper'> <iframe src='//player.vimeo.com/video/{{data.id}}?portrait=0&byline=0&badge=0&title=0' class='vimeo' frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe></div><span class='smallGrayText mediaBoxCaption'>{{data.caption}}</span>");

        soundCloudTemplate = _.template("<iframe style='width:100%;' class='soundCloudiframe' scrolling='no' frameborder='no' src=\"//w.soundcloud.com/player/?url={{data.id}}&color=ef4135&amp;auto_play=false&amp;show_artwork=false\"></iframe><span class='smallGrayText mediaBoxCaption'>{{data.caption}}</span>");

        audioTemplate = _.template("<audio preload='metadata' src=\"{{data.id}}\"/><span class='smallGrayText mediaBoxCaption'>{{data.caption}}</span>");

        pictureTemplate = _.template("<img syle='display:block;' width=\"100%\" src=\"{{data.id}}\" /><span class='smallGrayText mediaBoxCaption'>{{data.caption}}</span>");

        albumTemplate = _.template("" +
            "[[if(data.count>1){]]" +
            "<div style=\"position:relative;\">" +
            "<div style=\"cursor: pointer; float: right; margin-right:30px; height:1px; width:1px;\" onclick=\"{{data.next}}()\"><img src='/Static/img/media_right.png' class='centered_arrow' /></div>" +
            "<div style=\"cursor: pointer; float: left; margin-left:0px; height:1px; width:1px;\" onclick=\"{{data.prev}}()\"><img src='/Static/img/media_left.png' class='centered_arrow' /></div> [[}]]" +
            "<span class='smallGrayText mediaBoxCaption'>{{data.index}} of {{data.count}}</span>" +
            "<img syle='display:block;' width=\"100%\" src=\"{{data.url}}\" /></div>" +
            "<span class='smallGrayText mediaBoxCaption'>{{data.caption}}</span>");


        youtubeRegEx = /^(?:https?:\/\/)?(?:www\.)?(?:youtu\.be\/|youtube\.com\/(?:embed\/|v\/|watch\?v=|watch\?.+&v=))((\w|-){11})(?:\S+)?$/;

        vimeoRegEx = /^.*(vimeo\.com\/)((channels\/[A-z]+\/)|(groups\/[A-z]+\/videos\/))?([0-9]+)/;

        soundCloudRegEx = /((http:\/\/(soundcloud\.com\/.*|soundcloud\.com\/.*\/.*|soundcloud\.com\/.*\/sets\/.*|soundcloud\.com\/groups\/.*|snd\.sc\/.*))|(https:\/\/(soundcloud\.com\/.*|soundcloud\.com\/.*\/.*|soundcloud\.com\/.*\/sets\/.*|soundcloud\.com\/groups\/.*)))/;

        audioRegEx = /^.*\.(aiff|aac|flac|m4a|mp3|ogg|wav)$/;

        albumRegEx = /Album:(.*)/;

        var target_id = "<%=slideshowContent.ClientID%>";

        window[target_id + "switchToMedia"] = function (url, caption, target) {
            
            target.parent().parent().find("li").removeClass("sliderSelected");
            target.addClass("sliderSelected");
            function insertFinishedTemplate(html) {
                return $("#" + target_id).html(html).children()[0];
            }

            var data;
            var html;
            var regExResults;
            if (youtubeRegEx.test(url)) {
                regExResults = youtubeRegEx.exec(url);
                data = {
                    'id': regExResults[1],
                    'caption': caption
                };
                html = youtubeTemplate({ data: data });
                insertFinishedTemplate(html);
            } else if (vimeoRegEx.test(url)) {
                regExResults = vimeoRegEx.exec(url);
                data = {
                    'id': regExResults[5],
                    'caption': caption
                };
                html = vimeoTemplate({ data: data });
                insertFinishedTemplate(html);
            } else if (soundCloudRegEx.test(url)) {
                regExResults = soundCloudRegEx.exec(url);
                data = {
                    'id': regExResults[1],
                    'caption': caption
                };
                html = soundCloudTemplate({ data: data });
                insertFinishedTemplate(html);
            } else if (audioRegEx.test(url)) {
                data = {
                    'id': url,
                    'caption': caption
                };
                html = audioTemplate({ data: data });
                window.audiojs.create(insertFinishedTemplate(html));
            } else if (albumRegEx.test(url)) {
                regExResults = albumRegEx.exec(url);
                var t = regExResults[1];
                var stuff = _.map(t.split("|"), function (i) {
                    var result = i.split("~");
                    return { "url": result[0], "caption": result[1] };
                });
                _.each(stuff, function (i) {
                    i.count = stuff.length;
                    i.next = 'window.' + target_id + '_photoalbum_next';
                    i.prev = 'window.' + target_id + '_photoalbum_prev';
                });
                window[target_id + "_photoalbum_items"] = stuff;
                window[target_id + "_photoalbum_index"] = 0;

                window[target_id + "_photoalbum_next"] =
                    //_.bind(
                        function () {
                            var items = window[target_id + "_photoalbum_items"];
                            var newIndex = (window[target_id + "_photoalbum_index"] + 1) % items.length;
                            window[target_id + "_photoalbum_index"] = newIndex;
                            var data = items[newIndex];
                            data.index = newIndex + 1;
                            data.count = items.length;
                            html = albumTemplate({ data: data });
                            insertFinishedTemplate(html);
                        };
                //, this);
                window[target_id + "_photoalbum_prev"] =
                    _.bind(function () {
                        var items = window[target_id + "_photoalbum_items"];
                        var newIndex = window[target_id + "_photoalbum_index"] - 1;
                        if (newIndex < 0) {
                            newIndex = items.length - 1;
                        }
                        window[target_id + "_photoalbum_index"] = newIndex;
                        var data = items[newIndex];
                        data.index = newIndex + 1;
                        data.count = items.length;
                        html = albumTemplate({ data: data });
                        insertFinishedTemplate(html);
                    }, this);
                data = stuff[0];
                data.index = 1;
                data.count = stuff.length;
                html = albumTemplate({ data: data });
                insertFinishedTemplate(html);

            } else {
                data = {
                    'id': url,
                    'caption': caption
                };
                html = pictureTemplate({ data: data });
                insertFinishedTemplate(html);
            }




        }
    });
</script>
<script runat="server">
    public string handleImgDefaults(string url)
    {
        switch (url)
        {
            case "SoundCloudDefault":
                return "/Static/img/soundcloud-default.png";
            case "SoundDefault":
                return "/Static/img/sound-default.png";
            default:
                return url;
        }
    }

    public string icon(string url)
    {
        string ignore;
        string type = PublicTheater.Custom.Episerver.Utility.Utility.GetMediaType(url, out ignore);
        switch (type)
        {
            case "youtube":
                return @"<div style='
                            background: url(/Static/img/PUBLIC_media-icons_youtube.png) center center no-repeat;
                            width: 100%;
                            height: 100%;
                            position: absolute;
                        '></div>";
                break;
            case "vimeo":
                return @"<div style='
                            background: url(/Static/img/PUBLIC_media-icons_vimeo.png) center center no-repeat;
                            width: 100%;
                            height: 100%;
                            position: absolute;
                        '></div>";
                break;
            case "soundcloud":
                return @"<div style='
                            background: url(/Static/img/PUBLIC_media-icons_soundcloud.png) center center no-repeat;
                            width: 100%;
                            height: 100%;
                            position: absolute;
                        '></div>";
                break;
            case "album":
                return @"<div style='
                            background: url(/Static/img/PUBLIC_media-icons_slideshow.png) center center no-repeat;
                            width: 100%;
                            height: 100%;
                            position: absolute;
                        '></div>";
                break;
            case "sound":
                return @"<div style='
                            background: url(/Static/img/PUBLIC_media-icons_sound.png) center center no-repeat;
                            width: 100%;
                            height: 100%;
                            position: absolute;
                        '></div>";
                break;    
            default:
                return "";
                break;

        }


    }

</script>
<asp:Panel runat="server" ID="pnlMedia" CssClass="block noHeight mediaBlock">
    <div id="slideshowContent" style="
    position: relative;
" runat="server"></div>
    <div style="position: relative; <%= (rptRelatedMediaItems.Items.Count <= 1)? "display:none;": "" %>">
        <div class="mediaboxSlideshow slider" runat="server" id="slider">
            <asp:Repeater runat="server" ID="rptRelatedMediaItems">
                <HeaderTemplate>
                    <ul class="">
                </HeaderTemplate>
                <ItemTemplate>

                    <li style="position: relative;" onclick="window.<%=slideshowContent.ClientID%>switchToMedia(&quot;<%# Eval("LinkUrl") %>&quot;,&quot;<%# Eval("Caption") %>&quot;,$(event.currentTarget))">
                        <%# icon((string)Eval("LinkUrl")) %>


                        <img src="<%# handleImgDefaults(Eval("ImageUrl").ToString()) %>" />
                    </li>
                    <%# Container.ItemIndex == 0 ? @"<script>
    $(function() {
    var target = $('#" + slider.ClientID  + @"').find('li').first();
    window."+ slideshowContent.ClientID +"switchToMedia('"+ Eval("LinkUrl") +@"', '" + Eval("Caption") + @"',target)});</script>" 
                                : "" %>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </div>

        <div class="controls lemmon-controls">
            <a href="#" class="lemmon-prev prev-page">
                <img class="centered_arrow" src="/Static/img/media_left_slider.png" />
            </a>
            <a href="#" class="lemmon-next next-slide">
                <img class="centered_arrow" src="/Static/img/media_right_slider.png" />
            </a>
        </div>
    </div>


</asp:Panel>
