<%@ Page Language="c#" CodeBehind="FastUploadDialog.aspx.cs" AutoEventWireup="false"
     Inherits="MakingWaves.FastUpload.FastUpload.FastUploadDialog"
    MasterPageFile="Temp.Master" %>
<%@ Register TagPrefix="uc" TagName="MediaPropertyManagerArsht" Src="~/Views/Theater/Controls/MediaPropertyManagerArsht.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContentRegion" runat="server">
    <script type="text/javascript">
        function GetUploadDir() {
            return '<%=Request.QueryString["tofolder"]%>';
        }
    </script>
    <script type="text/javascript" src="/Views/Theater/FastUpload/js/json2.js"></script>
    <script language="JavaScript" src="js/ee.js"> </script>
    <!-- Load Queue widget CSS and jQuery -->
    <style type="text/css">
        @import url(/Views/Theater/FastUpload/js/jquery.plupload.queue/css/jquery.plupload.queue.css);
    </style>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.10.2/jquery-ui.min.js"></script>
    <!-- Third party script for BrowserPlus runtime (Google Gears included in Gears runtime now) -->
    <script type="text/javascript" src="http://bp.yahooapis.com/2.4.21/browserplus-min.js"> </script>
    <!-- Load plupload and all it's runtimes and finally the jQuery queue widget -->
    <script type="text/javascript" src="/Views/Theater/FastUpload/js/plupload.full.js"> </script>
    <script type="text/javascript" src="/Views/Theater/FastUpload/js/jquery.plupload.queue/jquery.plupload.queue.js"> </script>
    <script type="text/javascript">
    // <![CDATA[
        // Is run from startupscript in code behind
        function ToolButtonClick(returnValue) {
            EPi.GetDialog().Close(returnValue);
        }        
    // ]]>
    </script>
    <style type="text/css">
        .nosupport
        {
            padding: 20px;
        }
        .nosupport a
        {
            text-decoration: underline;
        }
    </style>
    <link href="css/custom.css" rel="Stylesheet" media="all" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FullRegion" runat="server">
    <script type="text/javascript">
        // Convert divs to queue widgets when the DOM is ready
        $(function () {
            $(function () {
                $("#uploader").pluploadQueue({
                    // General settings
                    runtimes: 'html5, flash, silverlight, gears, browserplus',
                    url: '/Views/Theater/FastUpload/FastUploadService.ashx?toDirectory=' + GetUploadDir(),
                    max_file_size: '60mb',
                    chunk_size: '1mb',
                    unique_names: true,

                    flash_swf_url: '/Views/Theater/FastUpload/js/plupload.flash.swf',
                    silverlight_xap_url: '/Views/Theater/FastUpload/js/plupload.silverlight.xap',

                    // PreInit events, bound before any internal events
                    preinit: {
                        UploadFile: function (up, file) {
                            //alert('String upload of file: ' + file.name);
                            if (up.runtime == 'html5') { $.post("/Views/Theater/FastUpload/FastUploadService.ashx", { fileName: file.name }); }
                        }
                    },

                    // Post init events, bound after the internal events
                    init: {
                        // Fires when a file has finished uploading
                        FileUploaded: function (up, file, info) {
                            postAdditionalFileInfo(file);
                            // If last file is done uploading
                            if (up.total.uploaded == up.files.length) {

                            }
                        },
                        // Fires when a error has occured
                        Error: function (up, args) {
                            //alert("ERROR!!!");
                            //alert(args);
                        }
                    }
                });
            });

            $('#saveMediaInfo').click(removeTagModal);

        });       

        var otherForm = null;
        var otherFileName = null;
        var otherOptionsButton = null;

        function postAdditionalFileInfo(currentFile) {

            var fileName = GetUploadDir() + '/' + currentFile.name;
            //var currentValues = GetJsonProperties();

            //var jsonData = "{'fileName':'" + fileName + "','properties':''}";
            var jsonData = GetJsonFromPropertyManager($("#extraData"));
            //jsonData = "{'fileName':'" + fileName + "','properties':'" + jsonData + "'}";
            var newJson = new Object();
            newJson.fileName = fileName;
            newJson.properties = jsonData;
            jsonData = JSON.stringify(newJson);
            $.ajax({
                type: "POST",
                url: 'SetFileProperties.asmx/UpdateEpiProperties',
                action: 'UpdateEpiProperties',
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != "success") {
                        alert(msg.d);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(thrownError);
                }

            });
        }

        function removeTagModal(e) {
            e.preventDefault();
            $('#overlay').fadeOut(500);
            $('#extraData').animate({ left: '-100%', opacity: 0 }, 400, 'easeInBack');
        }

    </script>
    <div id="uploadArea">
        <form method="post" action="FastUploadService.ashx">
        <div id='uploader'>
            <div class="nosupport">
                <h1>
                    Your browser can't upload multiple files.</h1>
                <br />
                Use <a href="https://www.google.com/chrome">Google Chrome</a> or <a href="http://www.mozilla.org/en-US/firefox/new/">
                    Firefox</a> to allow for multiple file uploads and drag and drop support using
                HTML5.
                <br />
                <br />
                As an alternative you can also <a href="http://get.adobe.com/flashplayer/">download
                    and install Flash</a> in your current browser to enable multiple file uploads,
                but his will not give you drag and drop support.
            </div>
        </div>
        </form>

        <div id="extraData">
            <h4>Enter Tags For Your Uploads</h4>
            <asp:ScriptManager runat="server" ID="scManager" EnablePartialRendering="true">
            </asp:ScriptManager>
            <uc:MediaPropertyManagerArsht runat="server" ID="ucPropertyManager" IsFileUploader="True"/>
            <div class="epitoolbuttonrowrightaligned">
                <span class="epi-cmsButton"><input id="saveMediaInfo" class="epi-cmsButton-text epi-cmsButton-tools epi-cmsButton-Save" type="button" value="Save and Start Uploading"></span>
            </div>            
        </div>
        
        <div id="pzl">
            <div id="puzzle" />
        </div>

        <div class="FMB-DialogContent">            
            <div class="epitoolbuttonrowrightaligned">
                <EPiServerUI:ToolButton ID="InsertButton" OnClientClick="ToolButtonClick({closeAction:'cancel', items:null}); "
                    GeneratesPostBack="false" Text="Close"
                    ToolTip="<%$ Resources: EPiServer, button.ok %>" runat="server" />
            </div>
        </div>
        
    </div>

    <div id="overlay"></div>

</asp:Content>
