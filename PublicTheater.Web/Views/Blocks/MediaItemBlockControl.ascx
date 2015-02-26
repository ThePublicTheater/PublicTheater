<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MediaItemBlockControl.ascx.cs" Inherits="PublicTheater.Web.Views.Blocks.MediaItemBlockControl" %>
<div class="block media">
    <asp:PlaceHolder ID="Vimeo" runat="server" Visible="false">
        <div class="videoWrapper">
            <iframe id="VimeoIframe" class="vimeo" runat="server" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="Youtube" runat="server" Visible="false">
        <div class="videoWrapper">
            <iframe id="YoutubeIframe" runat="server" frameborder="0" allowfullscreen></iframe>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="livestream" runat="server" Visible="false">
        <div class="videoWrapper">
            <iframe width="560" height="340" src="http://cdn.livestream.com/embed/newplay?layout=4&clip=pla_c0a09532-e385-4f03-851c-efca0fdedd22&color=0xe7e7e7&autoPlay=false&mute=false&iconColorOver=0x888888&iconColor=0x777777&allowchat=true&height=340&width=560" style="border: 0; outline: 0" frameborder="0" scrolling="no"></iframe>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="SoundCloud" runat="server" Visible="false">
        <iframe id="SoundCloudIframe" class="soundCloudiframe" scrolling="no" frameborder="no" runat="server"></iframe>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="Audio" runat="server" Visible="false">
        <script src="/Static/backbone/lib/require.js" data-main="/Static/backbone/main"></script>
        <script src="/backbone/lib/audiojs/audio.min.js"></script>
        
        <script type="text/javascript">
            
                $(function () {
                    window.audiojs.create(document.getElementById('<%= AudioElement.ClientID%>')); 
                });
            
        </script>
        <audio id="AudioElement" preload="none" runat="server" />
        <div class="bottomSection">
            <span id="AudioElementTitle" runat="server" class="mediaTitle">{{title}}</span>
            <span id="AudioElementSubTitle" runat="server" class="mediaSubTitle">{{subtitle}}</span>
        </div>
    </asp:PlaceHolder>
</div>
