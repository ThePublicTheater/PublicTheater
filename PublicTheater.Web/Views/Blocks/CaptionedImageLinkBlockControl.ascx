<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaptionedImageLinkBlockControl.ascx.cs" Inherits="PublicTheater.Web.Views.Blocks.CaptionedImageLinkBlockControl" %>
<div class="block noHeight fullPosterBlock">
    <style>
         .captionImageLinkBtn p {
             line-height: initial;
             margin: 0;
         }
         .btn:after, input.btn:after, input[type="submit"]:after {
             top: 15px;
         }
    </style>
    <asp:Image ID="image" runat="server" />
    <%--<a href="#" class="btn">Click for more information</a>--%>
    <asp:HyperLink ID="lnkMoreInfo" runat="server" CssClass="btn captionImageLinkBtn" />
</div>
