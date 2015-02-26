<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Theater/FastUpload/Temp.Master" AutoEventWireup="true" CodeBehind="SampleSubmit.aspx.cs" Inherits="AdageTheaterGroup.Web.FastUpload.SampleSubmit" %>
<%@ Register Src="~/Views/Theater/FastUpload/extraFileData.ascx" TagPrefix="adg" TagName="ExtraFileData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContentRegion" runat="server">
<script type="text/javascript">

    function postAdditionalFileInfo(currentFile) {
        var otherForm = null;
        var otherFileName = null;
        var otherOptionsButton = null;

            $(document.body).append('<iframe style="display:none" />');
            var iframe = $('iframe:last')[0];

            var iframeDocument = iframe.contentDocument || iframe.contentWindow.document;
            iframeDocument.open();
            iframeDocument.close();

            otherForm = iframeDocument.createElement('form');
            otherForm.setAttribute('action', 'submitExtraData.aspx');
            otherForm.setAttribute('method', 'POST');

            iframeDocument.body.appendChild(otherForm);

            otherForm = $(otherForm);

            var fileData = $('div.extraFileData');
            otherForm.append(fileData);

            var otherInputs = $('form>div>input');
            otherForm.append(otherInputs.html());

            otherFileName = otherForm.append('<input id=otherFileName name=otherFileName value=otherFileName />').find('#otherFileName');
            otherFileName.val(currentFile + ':' + location.search);

            otherForm.append('<input id=submitButton name=submitButton value=submitButton />');
            otherForm.append('<input id=otherFileName name=otherFileName value=otherFileName />');
        otherForm.submit();

        $('div.outerFileData').append(fileData);
        $('form').append(otherInputs);
        //$(document.body).remove('iframe:last');
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FullRegion" runat="server">
    <div class="outerFileData">
    <div class="extraFileData"><adg:ExtraFileData runat="server" id="fileData" /></div>
    </div>
    <a onclick="return void(postAdditionalFileInfo('file'));">Click Here</a>
    <asp:Button runat="server" ID="btn1" />
</asp:Content>
