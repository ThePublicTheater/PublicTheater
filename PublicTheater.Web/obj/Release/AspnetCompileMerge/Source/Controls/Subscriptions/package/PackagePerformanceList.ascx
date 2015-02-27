<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PackagePerformanceList.ascx.cs" Inherits="TheaterTemplate.Web.Controls.Subscriptions.PackageControls.PackagePerformanceList" %>

<asp:Repeater runat="server" ID="rptPerformances">
    <HeaderTemplate>
        <h3>The Plays:</h3> 
        <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li class="span3 packagePlayItem">
                <asp:HyperLink ID="lnkPDP" runat="server" Target="_blank">
                    <span class="title"><asp:Literal runat="server" ID="ltrTitle" /></span>
                    <span class="date"><asp:Literal runat="server" ID="ltrDate" /></span>
                    <img src="http://placehold.it/250x150" />
                    <asp:Image runat="server" ID="imgThumb" AlternateText="" />
                </asp:HyperLink>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>

<script type="text/javascript">
    function openInNewWindow(url) {
        var newwindow = window.open(url, 'name', 'height=600,width=450');
        if (window.focus) { newwindow.focus(); }
        return false;

    }
</script>