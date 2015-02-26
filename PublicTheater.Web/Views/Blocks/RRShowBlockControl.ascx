<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RRShowBlockControl.ascx.cs" Inherits="PublicTheater.Web.Views.Blocks.RRShowBlockControl" %>
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
.clearfix:after {
    visibility: hidden;
    display: block;
    font-size: 0;
    content: " ";
    clear: both;
    height: 0;
}

.clearfix {display: inline-block;}
body { font-family: sans-serif; }
.packeryWrapper {
    margin-bottom: 5px;
}
.packery {
  background: #FDD;
  background: hsla(0, 100%, 100%, 0.2);
  max-width: 1000px;
  height:500px;
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

</style>
<script type="text/javascript">
    
    $(function () {
        _.templateSettings = {
            evaluate: /\[\[(.+?)\]\]/g,
            interpolate: /\{\{(.+?)\}\}/g
        };
        items = <%= json%>;
        youtubeTemplate = _.template("<div class='videoWrapper'><iframe src='//www.youtube.com/embed/{{data.id}}?showinfo=0&rel=0&modestbranding=1' frameborder='0' allowfullscreen></iframe></div>");

        vimeoTemplate = _.template("<div class='videoWrapper'> <iframe src='//player.vimeo.com/video/{{data.id}}?portrait=0&byline=0&badge=0&title=0' class='vimeo' frameborder='0' webkitallowfullcreen mozallowfullscreen allowfullscreen></iframe></div>");

        soundCloudTemplate = _.template("<iframe style='width:100%;' class='soundCloudiframe' scrolling='no' frameborder='no' src=\"//w.soundcloud.com/player/?url={{data.id}}&color=ef4135&amp;auto_play=false&amp;show_artwork=false\"></iframe>");

        audioTemplate = _.template("<div class='audioWrapper'><audio preload='metadata' src=\"{{data.url}}\"/></div>");


        pictureTemplate = _.template('<img src="{{data.url}}"></div>');

        template = "[[for (var index = 0; index < data.length; index++){]]" +
            '<div class="item_container" ><div class="item" data-show="showidhere"> <div onclick="" class="script" style="display:none;" ></div><div class="thumb"><img src="{{data[index].url}}"></div></div></div></div>' +
            "[[}]]";

        var itemPre = '<div class="item_container" ><div class="item" data-show="showidhere"> <div onclick="" class="script" style="display:none;" ></div><div class="thumb">';
        var itemPost = '</div></div></div></div>';
        var output = "";
        for (var index = 0; index < items.length; index++) {
            switch (items[index].type) {
                case "sound":
                    output = output + itemPre + audioTemplate({ data: items[index] }) + itemPost;
                    break;
                case "vimeo":
                    output = output + itemPre + vimeoTemplate( { data: items[index] }) + itemPost;
                    break;
                case "youtube":
                    output = output + itemPre + youtubeTemplate({ data: items[index] }) + itemPost;
                    break;
                case "image":
                    output = output + itemPre + pictureTemplate({ data: items[index] }) + itemPost;
                    break;
                case "picture":
                    //not actually picture, probably text, so treat it like it!
                    break;
                
            default:
            }   
        }
        
        $(".packery").html(output);
        $("audio").each(function() { window.audiojs.create(this); });
        $('.packery').imagesLoaded(function() {
            // set things to the appropriate width.
            $(".packery .thumb>img").each(function() {
                var t = $(this);
                t.parent().width(this.naturalWidth);
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
    })
</script>
<div class="clearfix">
        <EPiServer:Property ID="ShowText" runat="server" PropertyName="ShowText" CssClass="block">
        </EPiServer:Property>
</div>

<div class="packeryWrapper">

    <div class="packery">
    </div>
</div>

