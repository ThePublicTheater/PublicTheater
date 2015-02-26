<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FilterControl.ascx.cs" Inherits="TheaterTemplate.Web.Controls.Utility.FilterControl" %>
<%@ Register Assembly="TheaterTemplate.Shared" TagPrefix="theaterTemplate" Namespace="TheaterTemplate.Shared.WebControls" %>

<div id="filterSelection">
    <asp:Repeater runat="server" ID="rptFilterContainers">
        <ItemTemplate>
            <div class="filter">
                <h3>
                    <asp:Literal runat="server" ID="ltrFilterTitle" />
                </h3>
                <asp:Repeater runat="server" ID="rptFilters">
                    <HeaderTemplate>
                        <ul class="unstyled">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <label class="checkbox">
                                <asp:CheckBox runat="server" ID="chkFilter" />
                                <theaterTemplate:GroupRadioButton runat="server" ID="rdoFilter" Visible="false" />
                                <asp:Label runat="server" AssociatedControlID="chkFilter" ID="lblChkFilter" />
                            </label>
                            <span class="hiddenArea" style="display:none;">
                                <asp:HiddenField runat="server" ID="filterValue" />                                
                            </span>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>