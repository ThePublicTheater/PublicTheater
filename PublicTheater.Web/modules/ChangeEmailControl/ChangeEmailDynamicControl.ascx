<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangeEmailDynamicControl.ascx.cs" Inherits="PublicTheater.Web.modules.Change_Email_Control.ChangeEmailDynamicControl" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <div>
            Current Email:
            <asp:Label ID="lblCurrentEmail" runat="server" Text=""></asp:Label>
            <a id="change" href="#">change</a>
        </div>
        <div id="newDiv" style="display: none;">
            New Email:<asp:TextBox ID="tbNewEmail" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" CssClass="smallBtn" Text="Save" OnClientClick="handleMail2()" OnClick="Button1_Click" style="cursor: pointer; border: solid 2px #000; color: #000; padding: 3px 20px 3px 5px; font-size: 13px; position: relative;" />
        </div>
        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
    </ContentTemplate>
</asp:UpdatePanel>
        <script>
            $(document).ready(function () {
                window.handleMail2 = function() {
                    var regexMail2 = /href=.mailto:(.*)'|">/;
                    var textElm = $("#<%= tbNewEmail.ClientID%>");
                    var oldText = textElm.val();
                    if (regexMail2.test(oldText)) {
                        var newText = regexMail2.exec(oldText)[1];
                        textElm.val(newText);
                    }
                }

                $("#change").click(function () {
                    $("#newDiv").slideDown();
                });
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#change").click(function () {
                    $("#newDiv").slideDown();
                });
            });
            
        </script>