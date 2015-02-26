<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master"
    AutoEventWireup="true" Inherits="TheaterTemplate.Web.Pages.Reserve.index" %>

<%@ Register TagPrefix="uc" TagName="ReserveControl" Src="~/Controls/reserve/ReserveControl.ascx" %>

<asp:content id="Content1" contentplaceholderid="HeadContent" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="PrimaryContent" runat="server">
    <div id="reserveSelectSeats">
        <div class="row-fluid">
            <uc:ReserveControl runat="server" ID="ReserveControl" ReserveJavaScriptFile="/js/reserve/reserveControl.js">
            </uc:ReserveControl>
        </div>
    </div>
</asp:content>
