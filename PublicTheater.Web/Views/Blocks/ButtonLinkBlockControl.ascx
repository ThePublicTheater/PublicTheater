<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ButtonLinkBlockControl.ascx.cs"
    Inherits="PublicTheater.Web.Views.Blocks.ButtonLinkBlockControl" %>
<div class="block linkBlock">
    <EPiServer:Property ID="Heading" PropertyName="Heading" runat="server" CustomTagName="h2" />
    <asp:repeater runat="server" id="rptLinks">
        <itemtemplate>
            <a href='<%# Eval("LinkUrl") %>' title='<%# Eval("LinkText")%>' class="btn"><%# Eval("LinkText")%></a> 
    </itemtemplate>
    </asp:repeater>
</div>
