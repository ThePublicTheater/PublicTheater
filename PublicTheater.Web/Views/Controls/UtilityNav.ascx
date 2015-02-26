<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UtilityNav.ascx.cs"
    Inherits="PublicTheater.Web.Views.Controls.UtilityNav" %>
<nav class="utilityNav">
                <ul>
                   <li>
                       <asp:HyperLink runat="server" ID="lnkLogin">
                           <i class="icon-lock"></i>
                           <asp:Label runat="server" ID="lbLogin">login</asp:Label>
                       </asp:HyperLink>
                   </li>
                    <li>
                        <asp:HyperLink runat="server" ID="lnkRegister" >
                            <i class="icon-user"></i>
                            <asp:Label runat="server" ID="lbRegister">Register Now</asp:Label>
                        </asp:HyperLink>
                   </li>
                    <li>
                        <asp:HyperLink runat="server" ID="lnkCart">
                            <i class="icon-shopping-cart"></i>
                            <asp:Label runat="server" ID="lbCart">Cart (0)</asp:Label>
                            <asp:Label runat="server" ID="cartExpire"  CssClass="cartExpireSpan"></asp:Label>
                        </asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink runat="server" ID="lnkEmailSignUp">
                            <i class="icon-envelope"></i>
                            <asp:Label runat="server" ID="lbEmailSignUp">Email Sign Up</asp:Label>
                        </asp:HyperLink>
                    </li> 
                </ul>
</nav>
