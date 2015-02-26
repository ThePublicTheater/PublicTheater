<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InstagramBlockControl.ascx.cs" Inherits="PublicTheater.Web.Views.Blocks.InstagramBlockControl" %>
<%@ Import Namespace="PublicTheater.Web.Services" %>
<script runat="server" type="text/C#">
/*
    public string InstagramifyCaption(string raw_caption)
    {

        return raw_caption;
    }
 */
</script>
<style type="text/css">
    .prev_arrow {
        float: left;
        margin-left: 0px;
    }
    .next_arrow {
        float: right;
        width: 30px;
        margin-right: 0px;
    }
     .centered_arrow {
         position: absolute;
         margin: auto;
         bottom: 0;
         top: 0px;
         opacity: .4;
         width: initial;
     }
    .centered_arrow:hover {
        opacity: .9;
    }
    .instagramBlock .bx-controls-direction .bx-next {
        display: none;
    }
    .instagramBlock .bx-controls-direction .bx-prev {
        display: none;
    }
    .bx-controls .bx-pager-item a.active {
        background: rgb(239, 65, 53);
    }
</style>
<asp:panel runat="server" id="pnlBlock" cssclass="block instagramBlock">
    <div class="controls">
        <a href="#" class="prev" style="display: none;"></a><a href="#" class="next" style="display: none;"></a>
    </div>
    <asp:repeater runat="server" id="rptSlideShow">
        <headertemplate>
        <ul class="slideshow adaptiveSlideshow manualSlideshow">    
    </headertemplate>
        <ItemTemplate>
            <li>
                <div style="position: relative;">
                    <span style="font-size: 20px;"><a href="http://instagram.com/<%# CurrentBlock.UserName %>"><img style="height: 22px;
display: inline;
width: auto;
vertical-align: sub;" src="/Static/img/invert instagram.png" />@<%# CurrentBlock.UserName %></a></span>
                    <img src='<%# ((Instagram.instagramPhotoItem)Container.DataItem).img %>' />
                   <%-- <div onclick="$(this).parents('.block').find('.bx-prev').click();" class="prev_arrow">
                        <img style="width: auto;" class="centered_arrow" src="/Static/img/media_left.png" />
                    </div>
                    <div onclick="$(this).parents('.block').find('.bx-next').click();" class="next_arrow">
                        <img style="width: auto;" class="centered_arrow" src="/Static/img/media_right.png" />
                    </div>--%>
                </div>
                <span><%# ((Instagram.instagramPhotoItem)Container.DataItem).caption %></span>
        </li>
    </itemtemplate>
        <footertemplate>
        </ul>
    </footertemplate>
    </asp:repeater>
</asp:panel>