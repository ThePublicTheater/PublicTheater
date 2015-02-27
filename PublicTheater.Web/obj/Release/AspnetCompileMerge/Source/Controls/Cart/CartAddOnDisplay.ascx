<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CartAddOnDisplay.ascx.cs" Inherits="TheaterTemplate.Web.Controls.CartControls.CartAddOnDisplay" %>

<div class="row-fluid">
    <div class="addOnDescription">
        <asp:Literal runat="server" ID="ltrAddOnText" />
    </div>
    <div class="selectQty"> 
        <span> </span>
        <asp:DropDownList runat="server" ID="ddlAddOnQuantity" AutoPostBack="true" CssClass="parkingDropDown" />
        <asp:Label ID="lblNumberReservedText" runat="server" Visible="false" />
        <asp:Label runat="server" ID="lblQuantity" AssociatedControlID="ddlAddOnQuantity" />
        <div class="parkingSubTotal">
            <asp:LinkButton runat="server" ID="lbRemove" Visible="false" CommandName="Remove" Text="X" ToolTip="Remove from order" CssClass="remove" />
            <asp:Label runat="server" ID="lblPriceDescription" AssociatedControlID="lblPrice" />
            <asp:Label runat="server" ID="lblPrice" Text="$0.00" />
        </div>
        <asp:HiddenField runat="server" ID="performanceId" />
        <asp:HiddenField runat="server" ID="priceTypeId" />
        <asp:HiddenField runat="server" ID="sectionId" />
    </div>
</div>