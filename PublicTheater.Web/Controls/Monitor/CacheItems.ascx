<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CacheItems.ascx.cs" Inherits="TheaterTemplate.Web.Controls.MonitorControls.CacheItems" %>

<script type="text/javascript" >
    jQuery(document).ready(function () {
        jQuery("#cbSelectAll").click(function () {
            var isChecked = jQuery(this).is(":checked");
            jQuery("tr.itemRow input:checkbox").each(function () {
                jQuery(this).attr("checked", isChecked);
            });
        });
    });
</script>

<br />
<asp:HyperLink runat="server" ID="lnkSession" CssClass="btn" Text="Session Page" NavigateUrl="~/_monitor/session.aspx" />
<br />
<hr />
<asp:Button runat="server" ID="btnClearCache" CssClass="btn" Text="Clear Checked Tessitura Items Below" />
<asp:Button runat="server" ID="btnClearEpiServer" CssClass="btn" Text="Clear EPiServer Cache" /> 
<br />
<br />
<asp:Label runat="server" ID="lblCleared" ForeColor="Green" />
<asp:ListView runat="server" ID="lvCacheItems">
	<LayoutTemplate>
        <table class="table table-condensed">
            <tr>
                <th><input type="checkbox" id="cbSelectAll" /><label for="cbSelectAll">All</label></th>
                <th>Key</th>
                <th>Type</th>
                <th>Value</th>
            </tr>
			<asp:PlaceHolder runat="server" ID="itemPlaceHolder" />
		</table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr class="itemRow">
            <td>
                <asp:Literal runat="server" ID="chkDelete"><input type="checkbox" name="keyToDelete" value="{0}" /></asp:Literal>
			</td>
            <td><asp:Literal runat="server" ID="ltrKey" /></td>
            <td><asp:Literal runat="server" ID="ltrType" /></td>
            <td><asp:Literal runat="server" ID="ltrValue" /></td>
        </tr>
    </ItemTemplate>
	<EmptyDataTemplate>
		<span>There are no items in the cache at this moment.</span>
	</EmptyDataTemplate>
</asp:ListView>
