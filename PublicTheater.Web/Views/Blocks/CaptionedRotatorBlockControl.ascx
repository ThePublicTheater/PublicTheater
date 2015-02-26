<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaptionedRotatorBlockControl.ascx.cs" Inherits="PublicTheater.Web.Views.Blocks.CaptionedRotatorBlockControl" %>
<style type="text/css">

</style>

<div class="whatsOnSub noHeight block" >
    <div class="loading">
  <img class="loading-image" src="/views/Theater/img/ajax-loader.gif" alt="Loading..." />
</div>
    
        
        <div class="hideTheWrapper" style="display:none" >
            <h2>
            <asp:Literal ID="ltrRotatorTitle" runat="server" /></h2>
            <asp:Repeater ID="rptRotatorItems" runat="server">
                <HeaderTemplate>
                    <ul class="slideshowHalf">
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <asp:Image ID="img" runat="server" />
                        <div>
                            <table style="width:100%">
                                <tbody>
                                    <tr>
                                        <td style="width: 55%;">
                                            
                                                <asp:Literal ID="ltrCaptionDescription" runat="server" />
                                        </td>
                                        <td style="vertical-align: bottom; width: 45%;">

                                            <asp:HyperLink ID="lnkTickets" runat="server" Text="View Details" CssClass="btn" CssSyle="max-width: 100%;" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                        </div>
                        
                       <%-- <div>
                            <div class="text">
                               <asp:Literal ID="ltrCaptionDescription" runat="server" />
                            </div>
                            <asp:HyperLink ID="lnkTickets" runat="server" Text="View Details" CssClass="btn" />
                        </div>--%>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </div>
</div>
