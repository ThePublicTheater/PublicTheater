<%@ Control Language="C#" AutoEventWireup="true" Inherits="Adage.HtmlSyos.EPiServer.Code.EPiServerHtmlSyosControl" %>

<%-- SYOS Application Files --%>

   <%-- <script src="http://cdn.adagetechnologies.com/syos/firststage/152/templates/syos.templates.js"></script>
    <script src="http://cdn.adagetechnologies.com/syos/firststage/152/syos.js"></script>--%>

<%--
    <script src="http://nmartin.adage.adagetechnologies.com:4646/152/test/syos.seat.test.js"></script>
    <script src="/_syoslocal/syos.templates.js"></script>
    <script src="/_syoslocal/syos.js"></script>
--%>


<%-- Error Templates --%>
<script type="text/template" id="reserve-error-general" data-error-title="Reserve Error" data-button-text="Reload">
    <p>There was an error reserving your seats. Please try again later. </p>
</script>

<script type="text/reserve-error-template" id="reserve-error-hold" data-error-title="Reserve Hold Error" data-button-text="Refresh Seats" data-error-string="Error 50000">
    <p>One of the seats that you were trying to reserve is on hold for another customer.</p>
    <p>Click <strong>refresh seats</strong> below to get an updated seat map.</p>
</script>

<script type="text/reserve-error-template" id="reserve-error-duplicate" data-error-title="Reserve Duplicate Error" data-button-text="Reload" data-error-string="This seat is already reserved in this order">
    <p>This seat appears to already by reserved in this order...</p>
</script>

<script type="text/template" id="ie-zoom-template">
    <h2>In order to use the SYOS in Internet Explorer 8, your zoom must be at 100%.</h2>
    <h3>Please change your zoom (Control+0) to 100% or open this page in another browser.</h3>
</script>


<%-- SYOS Markup --%>
<link rel="stylesheet" href="/syos/css/syos.responsive.css" />
<link rel="stylesheet" href="/syos/css/syos.brand.css" />

<div data-bb="syos" class="syos-wrap">
    <div id="syos" data-bb="syosRoot" class="syos-root"></div>        
    <div runat="server" ID="SYOSRootDataId" class="syosRootData" style="display:none;">209</div>
    <input type="hidden" runat="server" ID="hfSyosType"   class="HfSyosType" />
</div>
