<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalendarHeader.ascx.cs" Inherits="PublicTheater.Web.Controls.Calendar.CalendarHeader" %>
<%@ Register Src="~/controls/account/PromoBox.ascx" TagPrefix="account" TagName="PromoBox" %>


<h1><asp:Literal runat="server" ID="ltrHeader" /></h1>

<div class="headerText">
 
        <asp:Panel runat="server" ID="pnlAdditionalInformation" CssClass="calendarAddlInfo">
            <asp:Literal runat="server" ID="ltrAdditionalInformation" />
        </asp:Panel>

   
        <div class="monthSelection">
            <asp:Label runat="server" ID="lblMonths" Text="View Month:" AssociatedControlID="ddlMonths" />
            <asp:DropDownList runat="server" ID="ddlMonths" AutoPostBack="true" />
        </div>
   
</div>

<div class="row-fluid">

    <div class="span5">
        <account:Promobox runat="server" id="promoBox" Visible="False" />
    </div>
    <div class="span7">
        <div class="legendArea">
            <ul>
                <asp:Repeater runat="server" ID="rptCalendarVenues">
                    <ItemTemplate>
                        <li id="legendItem" runat="server">
                            <asp:Label runat="server" ID="lblLegendText" />
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Repeater runat="server" ID="rptSpecialEventVenues">
                    <ItemTemplate>
                        <li id="legendItem" runat="server">
                            <asp:Label runat="server" ID="lblLegendText" />
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
                <li runat="server" id="promoLegendItem" visible="false">
                    <asp:Label runat="server" ID="lblPromoLegendText" Text="Promotion Items" />        
                </li>
            </ul>
        </div>
    </div>

</div>