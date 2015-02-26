<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CartTotals.ascx.cs" Inherits="PublicTheater.Web.Controls.Cart.CartTotals" %>

<div class="row-fluid">

        <ul class="unstyled">
            <li>
                <asp:Label runat="server" CssClass="pull-left" ID="lblOrderSubTotal" Text="Order sub total:" AssociatedControlID="orderSubTotal" />
                <asp:Label runat="server" CssClass="pull-right" ID="orderSubTotal" />
            </li>    
            <asp:ListView runat="server" ID="lvOrderFees">
                <ItemTemplate>
                    <li>
                        <asp:Label runat="server" CssClass="pull-left" ID="lblOrderFees" AssociatedControlID="orderFees" />
                        <asp:Label runat="server" CssClass="pull-right" ID="orderFees" />
                    </li>
                </ItemTemplate>
            </asp:ListView>
            <asp:ListView runat="server" ID="lvOrderCredits">
                <ItemTemplate>
                    <li>
                        <asp:Label runat="server" CssClass="pull-left" ID="lblOrderCredit" AssociatedControlID="orderCredit" />
                        <asp:Label runat="server" CssClass="pull-right" ID="orderCredit"  />
                    </li>
                </ItemTemplate>
            </asp:ListView>
            <asp:PlaceHolder runat="server" ID="phAmountPaidToday" Visible="false">
                <li>
                    <asp:Label runat="server" CssClass="pull-left" ID="phAmount" Text="Amount paid Today:" AssociatedControlID="amountPaidToday" />
                    <asp:Label runat="server" CssClass="pull-right" ID="amountPaidToday" Text="" />
                </li>
                <asp:Repeater runat="server" ID="installmentsToPay">
                    <ItemTemplate>
                        <li>
                            <asp:Label CssClass="pull-left" runat="server" ID="InstallmentDate" /> - 
                            <asp:Label CssClass="pull-right" runat="server" ID="InstallmentAmount" />
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:PlaceHolder>
            <li class="total clearfix">
                <asp:Label runat="server" CssClass="pull-left" ID="lblOrderTotal" Text="TOTAL:" AssociatedControlID="orderTotal" />
                <asp:Label runat="server" ID="orderTotal" CssClass="currentOrderTotal pull-right" />
            </li>
        </ul>

    </div>
