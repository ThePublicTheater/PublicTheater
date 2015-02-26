<%@ Control Language="C#" AutoEventWireup="true" Inherits="PublicTheater.Web.Views.Theater.Controls.MediaManager" %>
<%@ Register TagPrefix="epi" Namespace="EPiServer.UI.WebControls" Assembly="EPiServer.UI" %>
<%@ Register TagPrefix="uc" TagName="MediaPropertyManagerArsht" Src="~/Views/Theater/Controls/MediaPropertyManagerArsht.ascx" %>
<script src="/Views/Theater/js/jquery.maskedinput.js" type="text/javascript"></script>
<script src="/Views/Theater/js/media_manager.js" type="text/javascript"></script>
<EPiServer:FileSystemDataSource runat="server" ID="FileDataSource" IncludeRoot="true"
    Root="/Global" />
<asp:TreeView ID="FileTree" runat="server" DataSourceID="FileDataSource" ExpandDepth="1"
    MaxDataBindDepth="6" OnTreeNodeDataBound="FileTree_TreeNodeDataBound" ShowExpandCollapse="true"
    EnableClientScript="true" PopulateNodesFromClient="true" CssClass="tree">
    <DataBindings>
        <asp:TreeNodeBinding TextField="Name" ToolTipField="Title" ImageUrlField="ImageUrl"
            PopulateOnDemand="true" />
    </DataBindings>
</asp:TreeView>
<div class="mediaManagerWrapper">
    <asp:Label ID="lblCurrentPath" runat="server" CssClass="currentPath" />
    <div class="updateAll mediaType">
        <div class="allWrapper">
            <h4>Bulk Update</h4>
            <p>Update all checked media below</p>
            <uc:MediaPropertyManagerArsht ID="HeaderPropertyManager" runat="server" />
            <div class="actions">
                <asp:Button ID="UpdateAll" runat="server" CssClass="updateAllBtn" Text="Update Checked" />
                <span>
                    <asp:Literal ID="SupportedMediaType" runat="server" /></span>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlPager" runat="server" DefaultButton="btnGoToPage">
        <div class="filePager">
            Page
            <asp:TextBox runat="server" ID="tbPageIndex" MaxLength="3" CssClass="filePagerInput"
                ClientIDMode="Static"></asp:TextBox>
            of
            <asp:Label runat="server" ID="lblPageCount"></asp:Label>
            <asp:HiddenField runat="server" ID="hfTotalPages" ClientIDMode="Static" />
            <asp:Label runat='server' ID="lblErrorMessage" CssClass="filePagerErrorMessage" />
            <asp:Button runat="server" ID="btnGoToPage" Text="Go" ClientIDMode="Static" OnClick="btnGoToPage_Click" />
        </div>
    </asp:Panel>
    <asp:ListView ID="lvFiles" runat="server">
        <ItemTemplate>
            
            
            <div id="mediaType" runat="server" class="mediaType">
                <div class="checkBox">
                    <asp:CheckBox runat="server" ID="cbUpdate" />
                </div>
                <div class="mediaImage">
                    <asp:HiddenField runat="server" ID="HfImageGuid" />
                    <asp:Image runat="server" ID="epiImage" ImageUrl="~/Views/Theater/img/ajax-loader.gif" />
                    <p>
                        <asp:Literal ID="litMediaName" runat="server" /></p>
                </div>

                <uc:MediaPropertyManagerArsht ID="ucPropertyManager" runat="server" />
                <div class="fields buttons">
                    <div class="AddButton">
                        <epi:ToolButton runat="server" ID="AddButton" Text="Save" CommandName="Add" DisablePageLeaveCheck="true"
                            CssClassInnerButton="addButton" />
                    </div>
                </div>
                
            </div>
        </ItemTemplate>
    </asp:ListView>
    <asp:PlaceHolder ID="phScript" runat="server" Visible="false">
        <asp:HiddenField ID="HfMediaManagerURL" runat="server" ClientIDMode="Static" />
        <script type="text/javascript">
            $(document).ready(function () {
                $('.mediaType:even').addClass('evenRow');

                InitializeMediaManagerControls();
                InitRelationshipEvents();
            });
        </script>
    </asp:PlaceHolder>
</div>
