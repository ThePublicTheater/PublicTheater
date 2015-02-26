<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MembershipPurchaseBlockControl.ascx.cs"
    Inherits="PublicTheater.Web.Views.Blocks.MembershipPurchaseBlockControl " %>
<div class="block MembershipPurchase">
    <EPiServer:Property ID="propDescription" PropertyName="Description" runat="server" CustomTagName="div" />
    
    <div>
    Quantity:<asp:DropDownList runat="server" ID="ddlQuantity"></asp:DropDownList>    
    </div>
    <div>
    <asp:LinkButton runat="server" ID="btnAdd" CssClass="btn">
        Add to Cart
    </asp:LinkButton>    
    </div>
    
</div>
