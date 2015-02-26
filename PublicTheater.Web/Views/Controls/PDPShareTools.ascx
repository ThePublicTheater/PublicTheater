<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PDPShareTools.ascx.cs"
    Inherits="PublicTheater.Web.Views.Controls.PDPShareTools" %>
<div class="socialPdp">
    <div class="addthis_toolbox addthis_default_style">
        <ul>
            <li><a class="addthis_button_facebook_like"></a></li>
            <li><a class="addthis_button_tweet"></a></li>
        </ul>
    </div>
</div>
<script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=ra-52d580fc5f365f5b"></script>
<script>
    var addthis_config = {
        ui_click: true
    };
    var addthis_share = {
        title: "<%=CurrentPage.PageName %>"
    };
</script>
