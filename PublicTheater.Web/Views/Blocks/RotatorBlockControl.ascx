<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RotatorBlockControl.ascx.cs"
    Inherits="PublicTheater.Web.Views.Blocks.RotatorBlockControl" %>
<asp:panel runat="server" id="pnlBlock" cssclass="block">
    <h2>
        <EPiServer:Property ID="RatatorHeaderTitle" PropertyName="RotatorTitle" runat="server"
            CustomTagName="span" />
    </h2>
    <div class="controls">
        <a href="#" class="prev"></a><a href="#" class="next"></a>
    </div>
    <asp:repeater runat="server" id="rptSlideShow">
        <headertemplate>
        <ul class="slideshow">    
    </headertemplate>
        <itemtemplate>
        <li>
            <a href='<%# Eval("LinkUrl") %>' title='<%# Eval("Title")%>'><img src='<%# Eval("ImageUrl") %>' style="width:100%;"/>
            </a> 
        </li>
    </itemtemplate>
        <footertemplate>
        </ul>
    </footertemplate>
    </asp:repeater>
</asp:panel>