<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageAlbumBlockControl.ascx.cs" Inherits="PublicTheater.Web.Views.Blocks.ImageAlbumBlockControl" %>
<asp:panel runat="server" id="pnlBlock" cssclass="block">
    <h2 style="margin-right: 30px;">
        <EPiServer:Property ID="RotatorHeaderTitle" PropertyName="MediaTitle" runat="server"
            CustomTagName="span" />
    </h2>
    <div class="controls">
        <a href="#" class="prev"></a><a href="#" class="next"></a>
    </div>
    <asp:repeater runat="server" id="rptSlideShow">
        <headertemplate>
        <ul class="slideshow adaptiveSlideshow  <% if (!CurrentBlock.AutoRotate){ %>manualSlideshow<% } %><% if (CurrentBlock.HideDots){ %>noPagerSlideshow<% } %>">    
    </headertemplate>
        <itemtemplate>
        <li>
            
            
            <div><img src='<%# Eval("ImageUrl") %>' title='<%# Eval("Title")%>'/>
                <% if (CurrentBlock.ShowCaptions)
                   { %>
                <div class="imageAlbumBlockCaption" style="font-family: 'Knockout 31 A', 'Knockout 31 B';	font-size: 20px; ">
                    <%# Eval("Description") %>    
                </div>
                <% } %>
            </div>
             
        </li>
    </itemtemplate>
        <footertemplate>
        </ul>
    </footertemplate>
    </asp:repeater>
</asp:panel>