<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master"
    AutoEventWireup="true" CodeBehind="xForm.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.xForm" %>

<asp:content id="Content1" contentplaceholderid="HeadContent" runat="server">
</asp:content>

<asp:content id="Content2" contentplaceholderid="PrimaryContent" runat="server">
    <EPiServer:Property ID="Property2" runat="server" PropertyName="MainBody">
    </EPiServer:Property>
</asp:content>

<asp:content id="Content3" contentplaceholderid="PrimaryContentBottomSection" runat="server">
   <div class="interiorPage">
      <div class="formWrapper">
            <EPiServer:Property ID="Property3" runat="server" PropertyName="FormTitle" CustomTagName="h3">
            </EPiServer:Property>
            <EPiServer:Property ID="Property1" runat="server" PropertyName="ContactForm">
            </EPiServer:Property>
          </div>
    </div>
</asp:content>
