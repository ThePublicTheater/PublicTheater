<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RRBlockControl.ascx.cs" Inherits="PublicTheater.Web.Views.Blocks.RRBlockControl" %>
<%@ Import Namespace="RestSharp.Extensions" %>
<script src="/Static/js/lib/jquery.js"></script>
<script src="/Static/backbone/lib/underscore.js"></script>
<script src="/Static/js/lib/draggabilly.pkgd.js"></script>
<script src="/Static/js/lib/packery.pkgd.min.js"></script>
<script src="/Static/js/lib/imagesloaded.pkgd.min.js"></script>
<script src="/Static/backbone/lib/require.js" data-main="/Static/backbone/main"></script>
<script src="/backbone/lib/audiojs/audio.min.js"></script>
<style>
figure.embed.reveal-smooth.revealEvent figcaption,
figure.embed-top.reveal-smooth.revealEvent figcaption,
figure.overlay.reveal-smooth.revealEvent figcaption,
figure.embed-over.reveal-smooth.revealEvent figcaption,
figure.reveal-smooth.revealEvent figcaption {
  opacity: 1;
  -webkit-transition: opacity .5s;
  -moz-transition: opacity .5s;
  -o-transition: opacity .5s;
  transition: opacity .5s;
}

* {
  -webkit-box-sizing: border-box;
     -moz-box-sizing: border-box;
          box-sizing: border-box;
}
.hidden {
      
}
.infoWrapper {
    padding-left: 0px;
    padding-right: 0px;
}
.clearfix:after {
    visibility: hidden;
    display: block;
    font-size: 0;
    content: " ";
    clear: both;
    height: 0;
}

.clearfix {display: inline-block; height: 1px; width: 1px;}

.packeryWrapper {
    margin-bottom: 5px;
}
.packery {
  
  
  min-height:200px;
}

/* clearfix */
.packery:after {
  content: ' ';
  display: block;
  clear: both;
}

.item_container {
  width: auto;
  float: left;
  font-size: 20px;
  color: black;
}

.item_container:hover {
  border-color: white;
  cursor: move;
}
.videoWrapper {
    width: 500px;
}
.audioWrapper {
    width: 250px;
    padding-bottom: 10px;
}
.item_container.is-dragging>.item,
.item_container.is-positioning-post-drag>.item {
  border-color: white;
  background: #09F;
  z-index: 2;
}
.item {
  background: #FFFFFF;
  margin: 4px;
  border-color: hsla(0, 0%, 0%, 0.3);
  padding: 10px;
}
.openshow{ display: none;}

</style>
<script type="text/javascript">
    function getBackground(select) {
        return;
        var newBackground = $(".backgrounds>[data-name="+select + "]");
        if(newBackground.length == 0){return;}
        $(".backgrounds>img").removeClass("opaque");
        newBackground.addClass("opaque");
    }

    function switchToShow(showid){
        getBackground(showid);
        $('html, body').animate({
            scrollTop: $(".infoWrapper").offset().top
        }, 400);
        if(showid=="shows"){
            $(".showslink").hide();
            $(".ClosedShowText").show();
            $(".OpenShowText").hide();
            $(".openshow").hide();
        } else {
            $(".showslink").show(400);
            $(".ClosedShowText").hide();
            $(".OpenShowText").show();
            $(".openshow[data-show=" + showid + "]").show();
        }
        $(".item[data-show][data-show!="+showid+"]").hide();
        
        $(".item[data-show="+showid+"]").show(500,function(){pckry.layout();});
        
    }

    $(function() {
        _.templateSettings = {
            evaluate: /\[\[(.+?)\]\]/g,
            interpolate: /\{\{(.+?)\}\}/g
        };
        items = <%= json%>;
        youtubeTemplate = _.template("<div class='videoWrapper'><iframe src='//www.youtube.com/embed/{{data.id}}?showinfo=0&rel=0&modestbranding=1' frameborder='0' allowfullscreen></iframe></div>");

        vimeoTemplate = _.template("<div class='videoWrapper'> <iframe src='//player.vimeo.com/video/{{data.id}}?portrait=0&byline=0&badge=0&title=0' class='vimeo' frameborder='0' webkitallowfullcreen mozallowfullscreen allowfullscreen></iframe></div>");

        soundCloudTemplate = _.template("<iframe style='width:100%;' class='soundCloudiframe' scrolling='no' frameborder='no' src=\"//w.soundcloud.com/player/?url={{data.id}}&color=ef4135&amp;auto_play=false&amp;show_artwork=false\"></iframe>");

        audioTemplate = _.template("<div class='audioWrapper'><audio preload='metadata' src=\"{{data.url}}\"/></div>");

        showImageTemplate = _.template('<div class="item_container" ><div class="item showImage" data-show="shows"> <div onclick="switchToShow({{data.showId}})" class="script" style="display:none;" ></div><div class="thumb"><img src="{{data.url}}"></div></div></div></div></div>');

        pictureTemplate = _.template('<img src="{{data.url}}"></div>');

        template = "[[for (var index = 0; index < data.length; index++){]]" +
            '<div class="item_container" ><div class="item" data-show="showidhere"> <div onclick="" class="script" style="display:none;" ></div><div class="thumb"><img src="{{data[index].url}}"></div></div></div></div>' +
            "[[}]]";

        var itemPre = _.template('<div class="item_container" ><div class="item" data-type="{{data.type}}" data-show="{{data.showId}}" style="display:none;"> <div onclick="" class="script" style="display:none;" ></div><div class="thumb">');
        var itemPost = '</div></div></div></div>';
        var output = '<div class="item_container">' +
            '<div class="item showslink" style="display:none;">' +
                '<div onclick="switchToShow(' + "'shows'" + ')" class="script" style="display:none;" ></div>' +
                '<div class="thumb">' +
                '<img src="'+ "<%=backToImageStr%>" +'" /></div>' +
            '</div></div>';

        for (var index = 0; index < items.length; index++) {
            switch (items[index].type) {
            case "sound":
                output = output + itemPre({ data: items[index] }) + audioTemplate({ data: items[index] }) + itemPost;
                break;
            case "vimeo":
                output = output + itemPre({ data: items[index] }) + vimeoTemplate({ data: items[index] }) + itemPost;
                break;
            case "youtube":
                output = output + itemPre({ data: items[index] }) + youtubeTemplate({ data: items[index] }) + itemPost;
                break;
            case "image":
                output = output + itemPre({ data: items[index] }) + pictureTemplate({ data: items[index] }) + itemPost;
                break;
            case "picture":
                //not actually picture, probably text, so treat it like it!
                break;
            case "showImage":
                output = output + showImageTemplate({ data: items[index] });
                break;

            default:
            }
        }

        $(".packery").html(output);

        $("audio").each(function() { window.audiojs.create(this); });
        $('.packery').imagesLoaded(function() {
            var multiplier = 1;
            if ($(window).width() < 900) {
                multiplier = .65;
            }
            if ($(window).width() < 500) {
                multiplier = .45;
            }
            // set things to the appropriate width.
            $(".packery .thumb>img").each(function() {
                var t = $(this);

                t.parent().width(this.naturalWidth * multiplier);
            });
            var container = document.querySelector('.packery');
            pckry = new Packery(container, {
                "columnWidth": 20,
                "rowHeight": 20
            });
            var itemElems = pckry.getItemElements();
            // for each item element
            for (var i = 0, len = itemElems.length; i < len; i++) {
                var elem = itemElems[i];
                // make element draggable with Draggabilly
                var draggie = new Draggabilly(elem);
                // bind Draggabilly events to Packery
                pckry.bindDraggabillyEvents(draggie);
                (function(elem) {

                    var onDragEnd = function(dragger) {
                        // compare movement
                        var drag = dragger.dragPoint;
                        if (drag.x !== 0 || drag.y !== 0) {
                            return;
                        }
                        // dragger didn't move
                        $(elem).children(".item").children(".script").click();
                        $(elem).toggleClass('gigante');
                        var isGigante = $(elem).hasClass('gigante');
                        pckry.layout();
                    };
                    draggie.on('dragEnd', onDragEnd);
                })(elem);
            }
        });
    });
</script>
<div class="large-12 medium-12 small-12 infoWrapper">
<div class="block noHeight ClosedShowText">
        <EPiServer:Property ID="ClosedText" runat="server" PropertyName="ClosedShowText" >
        </EPiServer:Property>
</div>
<div class="block noHeight OpenShowText" style="display: none">
    <%= opentext %>
</div>
    </div>
<div class="clearfix" >
    </div>
<div class="packeryWrapper">

    <div class="packery">
    </div>
</div>

