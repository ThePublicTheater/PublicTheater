<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master"
    AutoEventWireup="true" CodeBehind="PressRoom.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.PressRoom" %>

<asp:content id="Content1" contentplaceholderid="HeadContent" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="PrimaryContent" runat="server">
    <h3>
        News Releases</h3>
    <div class="large-8 list pressRoom">
        <asp:updatepanel runat="server" id="pnlPressReleases">
            <contenttemplate>
                <div class="filters">
                    <asp:DropDownList runat="server" ID="ddlVenues" AutoPostBack="True"></asp:DropDownList>
                    <asp:DropDownList runat="server" ID="ddlYears" AutoPostBack="True"></asp:DropDownList>
                </div>
                <ul>
                    <asp:repeater runat="server" id="rptPressItemsDisplay">
                        <itemtemplate>
                            <li>
                                <p><strong><%# Eval("FormattedReleaseDate")%></strong></p>
                                <p><a href='<%# Eval("ReleaseDocument")%>'><%# Eval("CalculatedHeading")%></a></p>
                            </li>
                        </itemtemplate>
                    </asp:repeater>
                </ul>        
            </contenttemplate>
        </asp:updatepanel>
    </div>
    <div class="large-4">
        <EPiServer:Property ID="Property1" runat="server" PropertyName="Contact" CssClass="textBlock">
        </EPiServer:Property>
        <div class="login press">
            <EPiServer:Property ID="Property2" runat="server" PropertyName="PressImagesTitle"
                CustomTagName="h4">
            </EPiServer:Property>
            <EPiServer:Property ID="Property3" runat="server" PropertyName="PressImagesDesc"
                CustomTagName="p">
            </EPiServer:Property>
            <ul>
                <li>
                    <asp:label id="PasswordLbl" associatedcontrolid="Password" runat="server">Password</asp:label>
                    <asp:textbox id="Password" runat="server">
                    </asp:textbox>
                    <asp:linkbutton id="Login" cssclass="btn" runat="server" onclick="Login_Click">Login</asp:linkbutton>
                </li>
            </ul>
            <EPiServer:Property runat="server" ID="propPasswordInvalidMessage" PropertyName="PasswordInvalidMessage" Visible="False" CssClass="error"></EPiServer:Property>
        </div>
        <EPiServer:Property ID="Property4" runat="server" PropertyName="MoreInfo" CustomTagName="div"
            CssClass="promo">
        </EPiServer:Property>
        <%--<div class="promo">
            <a href="#">
                <img src="http://placekitten.com/g/270/170" />
                <span>Click for more information</span> </a>
        </div>--%>
    </div>
</asp:content>
<asp:content id="Content5" contentplaceholderid="BeforeCloseBodyContent" runat="server">
</asp:content>