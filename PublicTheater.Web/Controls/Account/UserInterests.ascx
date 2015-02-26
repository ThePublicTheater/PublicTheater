<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserInterests.ascx.cs" Inherits="TheaterTemplate.Web.Controls.AccountControls.UserInterests" %>
<div class="checkList" style="display: none">
    <h3>I am interested in receiving more information on the following:</h3>
    <asp:CheckBoxList ID="CheckBoxListUserInterests" RepeatLayout="UnorderedList" runat="server" CssClass="unstyled">
    </asp:CheckBoxList>
</div>