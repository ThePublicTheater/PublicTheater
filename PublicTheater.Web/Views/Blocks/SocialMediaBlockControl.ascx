<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SocialMediaBlockControl.ascx.cs"
    Inherits="PublicTheater.Web.Views.Blocks.SocialMediaBlockControl" %>
<div class="twitterBlock block" style="overflow-y:auto;">
  <script src="/Static/backbone/lib/underscore.js"></script>
  <script src="/Static/js/lib/moment.min.js"></script>
    <style type="text/css">
.twitterBlock i.icon-twitter {
    padding-top: 0px;
}
         
    </style>

     <script type="text/javascript">
         $(function() {
             
             _.templateSettings = {
                 evaluate: /\[\[(.+?)\]\]/g,
                 interpolate: /\{\{(.+?)\}\}/g
             };
             twitterOneHandleTemplate = _.template("" +
                 "<i class='icon-twitter' style='padding: 13px;background: #fff;float: left;width: 100%;font-size: 20px;color: #000000; padding-bottom: 8px;'><a target='_blank' href='http://twitter.com/{{Handle}}'>@{{Handle}}</a></i>");
             twitterTemplate = _.template("" +
                 "<div class='tweetContainer'>" +
                 "[[if(!OneHandle){]]<i class='icon-twitter' style='font-size: 20px;color: #000000;'><a target='_blank' href='http://twitter.com/{{Handle}}'>@{{Handle}}</a></i>[[}]]" +
                 "<div> {{Text}} </div>" +
                 "<div> Posted {{CreatedAt}} </div>" +
                 "</div><hr>");

             twitter = {

                 urlify: function (text) {
                     var urlRegex = /(https?:\/\/[^\s]+)/g;
                     return text.replace(urlRegex, function (url) {
                         return '<a href="' + url + '">' + url + '</a>';
                     });
                 },
                 handleRt: function (status) {
                     if (status.RetweettedStatus.User === null) { return status.Text; } else {
                         return 'RT @' + status.RetweettedStatus.User.Identifier.ScreenName + ': ' + status.RetweettedStatus.Text;
                     }
                 },
                 at: function (tweet) {
                     return tweet.replace(/\B[@＠]([a-zA-Z0-9_]{1,20})/g, function (m, username) {
                         return '<a target="_blank" class="twtr-atreply" href="http://twitter.com/intent/user?screen_name=' + username + '">@' + username + '</a>';
                     });
                 },
                 hash: function (tweet) {
                     return tweet.replace(/(^|\s+)#(\w+)/gi, function (m, before, hash) {
                         return before + '<a target="_blank" class="twtr-hashtag" href="http://twitter.com/search?q=%23' + hash + '">#' + hash + '</a>';
                     });
                 },
                 convert: function (tweet) { return this.hash(this.at(this.urlify(this.handleRt(tweet)))); }
             }


         });

         var userAccounts = "<%= CurrentBlock.TwitterUserNames%>";
         $.ajax({
                 url: "/Services/Twitter.asmx/GetTweetsByUserNames",
                 contentType: "application/json",
                 type: "POST",
                 dataType: 'json',
                 data: JSON.stringify({ "usernames": userAccounts })
             })
             .fail(function() {

             })
             .done(function (response) {
                 
                 var statuses = response.d.statuses;
                 var one_handle = _.every(statuses, function (status) { return status.Handle == statuses[0].Handle; });
             
                 _.each(statuses, function (stat) {
                     stat.Text = twitter.convert(stat);
                     stat.CreatedAt = moment(parseInt(stat.CreatedAt.split("(")[1].split(")")[0])).fromNow();
                     stat.OneHandle = one_handle;
                 });
                 _.each(statuses, function (stat) { $('#<%=twitterInstance.ClientID%>').append(twitterTemplate(stat)); });
                 if (one_handle) {
                     $('#<%=twitterInstance.ClientID%>').parent().parent().prepend(twitterOneHandleTemplate(statuses[0]));
                     $('#<%=twitterInstance.ClientID%>').parent().css("padding-top", "4px");
                 }
             
                 console.log("Loaded Tweets:", statuses);
             });
     </script>
    <div runat="server" id="twitterInstance" >
        


</div>
    </div>