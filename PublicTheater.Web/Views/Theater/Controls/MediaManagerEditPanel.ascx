<%@ Control Language="C#" AutoEventWireup="true" Inherits="PublicTheater.Web.Views.Theater.Controls.MediaManagerEditPanel" %>

<script src="/Views/Theater/js/media_manager.js" type="text/javascript"></script>
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
<script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.10.2/jquery-ui.min.js"></script>
<link href="/Views/Theater/CSS/media_manager.css" rel="Stylesheet" media="all" />
<style type="text/css">
    .epi-cmsButton input.epiuploadbutton, .epi-cmsButtondisabled input.epiuploadbutton
    {
        background-image: url(/Views/Theater/FastUpload/img/FastUpload.png);
    }
</style>
<span id="MultipleNewFiles">
    <asp:HyperLink type="button" runat="server" Text="Mass Media Uploader" title="Upload multiple files"
        ID="btnMassUploader" CssClass="epiBtn" />
    <asp:HyperLink runat="server" type="button" Text="Media Manager" title="Media Manager"
        ID="btnToMediaManager" CssClass="epiBtn" />
</span>
<div id="plupdiv">
</div>
<script type="text/javascript">

    var currentFolder;
    var curPage;
    var pageSize = 10;

    function OpenFastUpload(toFolder) {
        var url = '/Views/Theater/FastUpload/FastUploadDialog.aspx?toFolder=' + toFolder;
        var dialogHeight = 580;
        var dialogWidth = 700;
        try {
            EPi.CreateDialog(url, PlupCompleted, '', '', { width: dialogWidth, height: dialogHeight });
        }
        catch (ex) {
            ShowPopupBlockedInfo();
        }
        return false;
    }

    //Callback functions for Dialog
    function PlupCompleted(returnValue, callbackArguments) {
        __doPostBack("", "");
    }

    
</script>
<div id="sortableContainer">
    <asp:ListView ID="lvMediaManager" runat="server">
        <ItemTemplate>
            <div class="mediaManagerFullWidth">
                <div id="mediaType" runat="server" class="mediaType">
                    
                    <div class="reorderHandle">
                        <asp:Image ID="imgReorderHandle" runat="server" ImageUrl="~/Views/Theater/img/reorder_handle.png"
                            AlternateText="reorder handle" />
                    </div>
                    <div class="mediaImage reorderable">
                        <%--<div class="arrow">
                            <epi:ToolButton runat="server" ID="BtnUp" Text=" " DisablePageLeaveCheck="true" SkinID="Up" />
                        </div>
                        <div class="arrow">
                            <epi:ToolButton runat="server" ID="BtnDown" Text=" " DisablePageLeaveCheck="true"
                                SkinID="Down" />
                        </div>--%>
                        <asp:Label runat="server" CssClass="PositionLabel" ID="lblPosition"></asp:Label>
                        <asp:HiddenField runat="server" ID="HfImageGuid" />
                        <asp:Image runat="server" ID="epiImage" ImageUrl="~/Views/Theater/img/ajax-loader.gif" />
                        <p>
                            <asp:Literal ID="litMediaName" runat="server" />
                            <br />
                            <asp:Literal ID="litMediaType" runat="server" />
                        </p>
                        <span class="hfMediaSortIndex">
                            <asp:HiddenField runat="server" ID="hfMediaSortIndex" />
                        </span><span class="hfMediaRelationshipId">
                            <asp:HiddenField runat="server" ID="hfMediaRelationshipId" />
                        </span>

                    </div>
                    <div class="orderIndex">
                        <asp:Label runat="server" ID="lblSendToIndex" Text="Send media to index:"/>
                        <asp:TextBox ID="txtSendToIndex" runat="server"/>
                        <asp:LinkButton runat="server" ID="btnSendToIndex" CssClass="epiBtn" Text="Go"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>
    <asp:DataPager runat="server" ID="MediaManagerDataPager" PagedControlID="lvMediaManager" PageSize="10" OnPreRender="MediaManagerDataPager_OnPreRender">
        <Fields>
            <asp:NumericPagerField CurrentPageLabelCssClass="currentPage" />
        </Fields>
    </asp:DataPager>
    <asp:Button ID="btnSubmitAfterReorder" runat="server" ClientIDMode="Static" Text="Submit Changes"
        CssClass="reorderButton" />
</div>
<div id="SortIsNotSaved" runat="server" ClientIDMode="Static">You have unsaved changes. <asp:Button runat="server" CssClass="epiBtn reorderButton" ID="btnSaveNow" Text="Save now?"></asp:Button></div>
<script>
    jQuery(document).ready(function () {

        $("#SortIsNotSaved").hide();
        curPage = jQuery('.currentPage').html();
        jQuery('#sortableContainer').sortable({ axis: 'y', cursor: 'move', helper: 'original', items: '.mediaManagerFullWidth', containment: 'parent'});
        jQuery('#sortableContainer').bind('sortupdate', function (event, ui) {
            
            jQuery('.mediaManagerFullWidth').each(function () {
                var thisItem = jQuery(this);
                thisItem.find('.hfMediaSortIndex input').val(thisItem.index() + (pageSize * (curPage - 1)));
                thisItem.find('.PositionLabel').html((thisItem.index() + 1 + (pageSize * (curPage - 1))) + '.');
            });
            $("#SortIsNotSaved").show();
        });

    });

</script>
