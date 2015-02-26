<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeroImageControl.ascx.cs" Inherits="PublicTheater.Web.Views.Controls.HeroImageControl" %>

<div class="interiorBackdrop">
    <img id="dontsave" src="data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==" style="width: 100%; height: 100%; position: absolute;">
    <asp:Image runat="server" ID="imgHero" />
</div>
