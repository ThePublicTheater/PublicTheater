<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YouMayAlsoLikeBlockControl.ascx.cs" Inherits="PublicTheater.Web.Views.Blocks.YouMayAlsoLikeBlockControl" %>
<div class="block noHeight mediaBlock">
    <h2>You may also like...</h2>
    <asp:Repeater ID="rptProductionSuggestions" runat="server">
        <ItemTemplate>
            <div class="large-6 medium-6 small-12">
                <asp:HyperLink ID="lnkPDP" runat="server" CssClass="mayLikeLink" NavigateUrl='<%# Eval("FriendlyUrl") %>'>
                    <asp:Image ID="productionImage" runat="server" ImageUrl='<%# Eval("Thumbnail150x75") %>' />
                    <asp:Literal ID="ltrProductionName" runat="server" Text='<%# Eval("Heading") %>'/>
                </asp:HyperLink>
            </div>
        </ItemTemplate>
    </asp:Repeater>

</div>
