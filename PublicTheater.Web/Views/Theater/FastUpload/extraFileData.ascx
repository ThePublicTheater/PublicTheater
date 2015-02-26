<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="extraFileData.ascx.cs" Inherits="PublicTheater.Web.Views.Theater.FastUpload.extraFileData" %>
<XForms:XFormControl runat="server" ID="extraFileDataControl" ClientIDMode="Static" />
<script type="text/javascript">
    function GetJsonProperties() {
        var currentData = $('.extraData :input');
        var currentValues = '';
        currentData.each(function () {
            if ($(this).val()) {

                if ($(this).is(':radio') || $(this).is(':checkbox')) {
                    if ($(this).attr('checked')) {
                        // all it to be added
                        var localId = this.id;
                        var localValue = $(this).val();
                        localId = localId.substring(0, localId.length - localValue.length);

                        currentValues += localId + '=' + encodeURI(localValue) + "&";
                    }
                }
                else {
                    currentValues += this.id + '=' + encodeURI($(this).val()) + "&";
                }
            }
        });

        return currentValues;
    }
</script>