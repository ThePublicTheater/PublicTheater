<%@ Control Language="C#" AutoEventWireup="true" 
    Inherits="Adage.Theater.RelationshipManager.PlugIn.GenericRelationship"
    EnableViewState="true" %>
<%@ Register TagPrefix="epi" Assembly="EpiServer" Namespace="EPiServer.Web.WebControls" %>
<%@ Register TagPrefix="epi" Namespace="EPiServer.UI.WebControls" Assembly="EPiServer.UI" %>
<link href="/Views/Theater/CSS/media_manager.css" rel="Stylesheet" media="all" />
<!--[if IE]> 
        <link rel="stylesheet" type="text/css" media="screen" href="/App_Themes/main/ie8lte.css" />
    <![endif]-->
<div class="mediamanagerContainer">
    <div class="relationName">
        <asp:Label ID="lblError" runat="server" ForeColor="Red" />
        <asp:Label ID="lblRelatedPage" runat="server" AssociatedControlID="SelectedPageReference">Related Page</asp:Label>
        <epi:InputPageReference runat="server" ID="SelectedPageReference" CssClass="pagereference_class" />
        <div class="relationship">
            <asp:Label ID="LblRelationshipName" runat="server">Name</asp:Label>
            <asp:TextBox runat="server" ID="TbxRelationshipName"></asp:TextBox>
        </div>
        <div class="saveCancel">
            <epi:ToolButton ID="SaveButton" SkinID="Save" Text="<%$ Resources: EPiServer, button.save %>"
                ToolTip="<%$ Resources: EPiServer, button.save %>" runat="server" DisablePageLeaveCheck="true" />
            <epi:ToolButton ID="CancelButton" SkinID="Cancel" Text="<%$ Resources: EPiServer, button.cancel %>"
                ToolTip="<%$ Resources: EPiServer, button.cancel %>" runat="server" DisablePageLeaveCheck="true" />
        </div>
        <input type="hidden" id="DataItemIndexId" runat="server" class="hiddenItemIndexValue" />
        <input type="hidden" id="DataStoreId" runat="server" class="hiddenItemValue" />
    </div>
    <div class="relatedPages">
        <%--<asp:UpdatePanel ID="upRelatedPages" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>--%>
                <asp:Repeater runat="server" ID="rptRelatedPages">
                    <ItemTemplate>
                        <div class="related">
                            <div class="info">
                                <div class="arrow">
                                    <epi:ToolButton runat="server" ID="BtnUp" Text=" " DisablePageLeaveCheck="true" SkinID="Up" />
                                </div>
                                <div class="arrow">
                                    <epi:ToolButton runat="server" ID="BtnDown" Text=" " DisablePageLeaveCheck="true"
                                        SkinID="Down" />
                                </div>
                                <asp:HiddenField runat="server" ID="pageId" />
                                <asp:Label ID="RelatedPageName" runat="server" CssClass="pageName" />
                                <p class="description">
                                    <%# Eval("Description") %></p>
                            </div>
                            <div class="activateEdit">
                                <epi:ToolButton runat="server" ID="ActiveButton" Text="Activate" DisablePageLeaveCheck="true" />
                                <epi:ToolButton runat="server" ID="BtnDelete" Text="Delete" DisablePageLeaveCheck="true" />
                                <epi:ToolButton runat="server" ID="BtnUpdate" Text="Edit" title="Edit" DisablePageLeaveCheck="true"
                                    CssClassInnerButton="editRelationshipButton" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</div>
<asp:PlaceHolder ID="phScript" runat="server" Visible="false">
    <script type="text/javascript">
        $(document).ready(function () {
            $("input.editRelationshipButton").live('click', function (e) {
                var itemId = $(this).parent().attr("itemId");
                var itemIndexId = $(this).parent().attr("itemIndexId");
                $(this).parents(".mediamanagerContainer").find(".hiddenItemValue").val(itemId);
                $(this).parents(".mediamanagerContainer").find(".hiddenItemIndexValue").val(itemIndexId);
                //Update InputPageReference Display Value :: There is a bug with updating display value, it's 1-off so the last value is displayed instead
                var relatedItem = $(this).parents(".related");
                var pageName = relatedItem.find(".pageName").html();
                var pageId = relatedItem.find("input[type='hidden']").val();
                var pageRef = pageName + ' [' + pageId + ']';
                $(".pagereference_class").find("input[type='text']").val(pageRef);
                var pageDescription = relatedItem.find(".description").html();
                $(".relationship").find("input[type='text']").val(pageDescription.trim());

                e.preventDefault();
            });
        });
    </script>
</asp:PlaceHolder>
