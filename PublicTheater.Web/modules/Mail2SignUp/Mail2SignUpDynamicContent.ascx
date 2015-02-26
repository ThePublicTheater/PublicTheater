<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Mail2SignUpDynamicContent.ascx.cs" Inherits="PublicTheater.Web.modules.Mail2SignUp.Mail2SignUp" %>


<script type="text/javascript">
    $(document).ready(function(){
        

        window.Mail2Ajax = function() {

            $.ajax({
                type: "POST",
                <% if (MatchSubscriptions)
                   { %>
                url: "/Services/EmailSubscribeService.asmx/MatchSubscriptions",
                <% }else{ %>
                url: "/Services/EmailSubscribeService.asmx/Subscribe",
                <% } %>
                data: JSON.stringify({
                    publicList: $("#Mail2PublicOption").prop('checked'),
                    joesPubList: $("#Mail2JoesPubOption").prop('checked'),
                    shakespeareList:$("#Mail2ShakespeareOption").prop('checked'),
                    forumList: $("#Mail2ForumOptionContainer").prop('checked'),
                    thisWeekList: $("#Mail2WeeklyOptionContainer").prop('checked'), 
                    utrList: $("#Mail2UtrOptionContainer").prop('checked')
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) {
                    if (msg.d)
                        $("#subscriptionNotification").text("You've subscribed!");
                    else
                        $("#subscriptionNotification").text("");
                },
                failure: function() {
                    $("#subscriptionNotification").text("");
                }
            });
            return false;
        }
        var first = <%= FirstChoice%> ;
        switch (first) {
            case 2:
                $("#Mail2JoesPubOptionContainer").insertBefore($("#Mail2PublicOptionContainer"));
                break;
            case 3:
                $("#Mail2ShakespeareOptionContainer").insertBefore($("#Mail2PublicOptionContainer"));
                break;
            case 4:
                $("#Mail2ForumOptionContainer").insertBefore($("#Mail2PublicOptionContainer"));
                break;
            case 5:
                $("#Mail2UtrOptionContainer").insertBefore($("#Mail2PublicOptionContainer"));
                break;
            case 6:
                $("#Mail2WeeklyOptionContainer").insertBefore($("#Mail2PublicOptionContainer"));
                break;
            
        
        }
        
            
        if ($("#Mail2Public").val() == "true") {
            $("#Mail2PublicOption").attr("checked", "checked");
        }
        if ($("#Mail2JoesPub").val() == "true") {
            $("#Mail2JoesPubOption").attr("checked", "checked");
        }
        if ($("#Mail2Shakespeare").val() == "true") {
            $("#Mail2ShakespeareOption").attr("checked", "checked");
        }
        if ($("#Mail2Forum").val() == "true") {
            $("#Mail2ForumOption").attr("checked", "checked");
        }
        if ($("#Mail2Utr").val() == "true") {
            $("#Mail2UtrOption").attr("checked", "checked");
        }
        if ($("#Mail2Weekly").val() == "true") {
            $("#Mail2WeeklyOption").attr("checked", "checked");
        }
    })
</script>



<div style="display: inline" id="Mail2PublicOptionContainer">
    <input type="checkbox" id="Mail2PublicOption" <% if (PublicDefaultChecked){ %> checked="checked"<% } %>
    ><%= PublicText %><br>
</div>
<div style="display: inline" id="Mail2JoesPubOptionContainer">
    <input type="checkbox" id="Mail2JoesPubOption" <% if (JoesPubDefaultChecked){ %> checked="checked"<% } %>
    ><%= JoesPubText %><br>
</div>
<div style="display: inline" id="Mail2ShakespeareOptionContainer">
    <input type="checkbox" id="Mail2ShakespeareOption" <% if (ShakespeareDefaultChecked){ %> checked="checked"<% } %> 
    > <%= ShakespeareText %><br>
</div>
<div style="display: inline" id="Mail2ForumOptionContainer">
    <input type="checkbox" id="Mail2ForumOption" <% if (ForumDefaultChecked){ %> checked="checked"<% } %> 
    > <%= ForumText %><br>
</div>
<div style="display: inline" id="Mail2UtrOptionContainer">
    <input type="checkbox" id="Mail2UtrOption" <% if (UtrDefaultChecked){ %> checked="checked"<% } %> 
    > <%= UtrText %><br>
</div>
<div style="display: inline" id="Mail2WeeklyOptionContainer">
    <input type="checkbox" id="Mail2WeeklyOption" <% if (WeeklyDefaultChecked){ %> checked="checked"<% } %> 
    > <%= WeeklyText %><br>
</div>
<asp:Panel runat="server" Visible="<%# SubmitButton %>" >
    <asp:Button runat="server" Text="Subscribe" ID="submitEntry" OnClientClick="return window.Mail2()"></asp:Button>
    <br />
    <span id="subscriptionNotification" ></span>
</asp:Panel>
