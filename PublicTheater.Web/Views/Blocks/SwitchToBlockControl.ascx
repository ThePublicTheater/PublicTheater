<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SwitchToBlockControl.ascx.cs"
    Inherits="PublicTheater.Web.Views.Blocks.SwitchToBlockControl" %>
<div class="block linkImage">
    <img style="cursor: pointer;" src="<%=hlImage%>" onclick="$('.MainBody').html($('.replaceMainBody[data-replaceid=<%=CurrentBlock.LinkUrl.ID %>]').html());$('.homeWrapper.generalWrapper>h1').html($('.replaceTitle[data-replaceid=<%=CurrentBlock.LinkUrl.ID %>]').html());">
    <div style="display:none" class="replaceMainBody" data-replaceid="<%=CurrentBlock.LinkUrl.ID %>">
        <%=newbody %>
    </div>
    <div style="display:none" class="replaceTitle" data-replaceid="<%=CurrentBlock.LinkUrl.ID %>">
        <%=newtitle %>
    </div>

</div>
