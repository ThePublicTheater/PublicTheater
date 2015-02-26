<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BestAvailable.ascx.cs"
    Inherits="PublicTheater.Web.Controls.Reserve.BestAvailable" %>
<%@ Register Assembly="TheaterTemplate.Shared" TagPrefix="theaterTemplate" Namespace="TheaterTemplate.Shared.WebControls" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.51116.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<script type="text/javascript">
             function ThrottleReserve(){
                 $(".bestAvailableButton").addClass("disabled");
             };

</script>
<style>
    a.disabled {
        pointer-events: none;
    }
    a.disabled:after {
        content: "\f110";
        display: inline-block;
        -webkit-animation: spin1 1s infinite steps(8);
        -animation: spin1 1s infinite steps(8);
    }
    @-webkit-keyframes spin1 {
0% { -webkit-transform: rotate(0deg);}
100% { -webkit-transform: rotate(360deg);}
}
@-moz-keyframes spin1 {
0% { -moz-transform: rotate(0deg); }
100% { -moz-transform: rotate(360deg);}
}
@-o-keyframes spin1 {
0% { -o-transform: rotate(0deg);}
100% { -o-transform: rotate(360deg);}
}
@-ms-keyframes spin1 {
0% { -ms-transform: rotate(0deg);}
100% { -ms-transform: rotate(360deg);}
}

     
</style>
        
<div id="bestAvailableTable" class="table table-condensed table-bordered">

    <ul class="large-12 medium-12 small-12 tableHead">
        <li>Section</li>               
        <asp:ListView runat="server" ID="lvPriceTypeNames">
            <ItemTemplate>
                <li>
                    <asp:Label runat="server" ID="lblPriceTypeName"></asp:Label>
                </li>
            </ItemTemplate>
        </asp:ListView>
    </ul>

    <asp:ListView runat="server" ID="lvSectionSelection">
        <ItemTemplate>
            <ul class="large-12 medium-12 small-12 tableBody">
                <li><theaterTemplate:GroupRadioButton runat="server" ID="rbSection" GroupName="SectionRadios" /></li>
                <asp:ListView runat="server" ID="lvSectionPriceTypes">
                    <ItemTemplate>
                        <li>
                            <asp:Label runat="server" ID="lblSectionPrice"></asp:Label>
                        </li>
                    </ItemTemplate>
                </asp:ListView>
            </ul>
        </ItemTemplate>
    </asp:ListView>

    <ul class="large-12 medium-12 small-12 tableFoot">
        <li>Quantity</li>
        <asp:ListView runat="server" ID="lvPriceTypeQuantitySelection">
                <ItemTemplate>
                    <li>
                        <asp:DropDownList runat="server" ID="ddlPriceTypeQuantity">
                        </asp:DropDownList>
                    </li>
                </ItemTemplate>
        </asp:ListView>
    </ul>

</div>

<div class="large-12 medium-12 small-12">
    <asp:LinkButton runat="server" ID="btnSubmit" Text="Reserve" class="btn bestAvailableButton" OnClientClick="ThrottleReserve()" />
    <asp:HiddenField runat="server" ID="submitClicked" ClientIDMode="Static" Value="false" />
    <episerver:Property runat="server" id="BestAvailableMainBody" PropertyName="CustomProperty3" />

    <asp:Panel runat="server" ID="pnlError" CssClass="errorBox" Visible="false">
        <asp:Label runat="server" ID="lblErrors" />
        <script type="text/javascript">
            console.log("Error message appeared");
        </script>

    </asp:Panel>
    <asp:Image runat="server" id="VenueImage" />
</div>


<asp:Panel runat="server" ID="pnlMultiZoneDialog" CssClass="selectNewSectionContainer" Style="display: none;">
    <asp:ModalPopupExtender ID="mdlMultiZoneConfirm" runat="server" TargetControlID="btnShowModal"
        PopupControlID="pnlMultiZoneDialog" DropShadow="false" RepositionMode="none" BackgroundCssClass="bgOverlay"
        CancelControlID="lbClose" />
    <asp:Button runat="server" ID="btnShowModal" Text="Show Modal" Style="display: none;" />
    <div class="subsModalInner">
        <div class="packageDescription">
            <h2>Note: Your order will not all be seated together.</h2>
        </div>
        <div class="subsModalContent">
            You have selected both priority and standard seats, which are located in different parts of the theater. Do you want to continue?
        </div>
        <div class="continueBtn updateButton">
            <asp:LinkButton runat="server" ID="lbOK" CssClass="btn btnStandOut" CausesValidation="false">Yes - Continue</asp:LinkButton>
            <asp:LinkButton runat="server" ID="lbClose" CssClass="btn btnStandOut" CausesValidation="false">No - Cancel</asp:LinkButton>
        </div>
    </div>
</asp:Panel>