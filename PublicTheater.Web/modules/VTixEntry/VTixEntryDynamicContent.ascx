<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VTixEntryDynamicContent.ascx.cs" Inherits="PublicTheater.Web.modules.VTixEntry.VTixEntryDynamicContent" %>
<%@ Import Namespace="PublicTheater.Web.Services" %>
<script runat="server" language="C#">
    void Submit_Entry(Object sender, EventArgs e)
   {
       try
       {


           PublicTheater.Custom.VTIX.VtixClient.insertEntry(Name.Value, Address.Value, int.Parse(LineType.Value),EmailAddressField.Value);
           
           EmailSubscribeService.Subscribe(Mail2Public.Value == "true", Mail2JoesPub.Value == "true", Mail2Shakespeare.Value == "true");
           
           Response.Redirect(Request.RawUrl);
       }
       catch (Exception)
       {
           submitEntry.Text = "Something has gone wrong";
       }
       
   }
   </script>
<script type="text/javascript">
    $(document).ready(function(){
        var nameEl = document.getElementById('<%= Name.ClientID%>');
        var AddressEl = document.getElementById('<%= Address.ClientID%>');
        var LineTypeEl = document.getElementById('<%= LineType.ClientID%>');
        var EmailAddressEl = document.getElementById('<%= EmailAddressField.ClientID%>');
        window.VTix = {
            setName: function (newVal) {
                nameEl.value = newVal;
            },
            setAddress: function (newVal) {
                AddressEl.value = newVal;
            },
            setLineType: function (newVal) {
                LineTypeEl.value = newVal;
            },
            setEmailAddress: function (newVal) {
                EmailAddressEl.value = newVal;
            }
        };
        var Mail2ShakespeareEl = document.getElementById('<%= Mail2Shakespeare.ClientID%>');
        var Mail2PublicEl = document.getElementById('<%= Mail2Public.ClientID%>');
        var Mail2JoesPubEl = document.getElementById('<%= Mail2JoesPub.ClientID%>');
        window.Mail2 = function() {
            Mail2ShakespeareEl.value = $("#Mail2ShakespeareOption").prop('checked');
            Mail2PublicEl.value = $("#Mail2PublicOption").prop('checked');
            Mail2JoesPubEl.value = $("#Mail2JoesPubOption").prop('checked');
        }

    })
</script>
<asp:HiddenField ClientIDMode="Static" ID="Name"  runat="server" />
<asp:HiddenField ClientIDMode="Static" ID="Address" runat="server" />
<asp:HiddenField ClientIDMode="Static" ID="LineType" runat="server" />
<asp:HiddenField ClientIDMode="Static" ID="EmailAddressField" runat="server" />

<asp:HiddenField ClientIDMode="Static" ID="Mail2Public" runat="server" />
<asp:HiddenField ClientIDMode="Static" ID="Mail2JoesPub" runat="server" />
<asp:HiddenField ClientIDMode="Static" ID="Mail2Shakespeare"  runat="server" />


<asp:Button runat="server" Text="Submit (only once per day)" ID="submitEntry" OnClientClick="window.Mail2()" OnClick="Submit_Entry"></asp:Button> 

