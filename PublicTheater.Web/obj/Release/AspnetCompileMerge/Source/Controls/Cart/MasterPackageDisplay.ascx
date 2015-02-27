<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MasterPackageDisplay.ascx.cs" Inherits="TheaterTemplate.Web.Controls.CartControls.MasterPackageDisplay" %>

<%@ Register Src="~/controls/cart/FullPackageDisplay.ascx" TagPrefix="cart" TagName="FullPackageDisplay" %>

<div class="packageArea row-fluid">
    <div class="flexGuidelines">
        <h3>
            <asp:LinkButton runat="server" ID="lbRemove" Text="X" CommandName="Remove" CssClass="remove" ToolTip="Remove this from your cart" />
            <asp:Literal runat="server" ID="ltrPackageDescription" Text="Master Package" />  
        </h3>
    </div>
    <div class="miniCart">
        <asp:Repeater runat="server" ID="rptPackages">
            <HeaderTemplate>
                <table cellspacing="0" border="0" class="packageCart"> 
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <cart:FullPackageDisplay runat="server" ID="cartFullPackage" HideHeader="true" ShowRemoveButton="false" />                
            </ItemTemplate>
            <FooterTemplate>
                    </tbody>
                </table>
            </FooterTemplate>        
        </asp:Repeater>      
        <asp:Panel runat="server" ID="pnlEdits" CssClass="packageEdits" Visible="false">
            <asp:Literal runat="server" ID="PackageEdits" />
        </asp:Panel>
    </div>
</div>