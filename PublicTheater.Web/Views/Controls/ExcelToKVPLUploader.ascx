<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExcelToKVPLUploader.ascx.cs" Inherits="TheaterTemplate.Web.Views.Controls.ExcelToKVPLUploader" %>


<asp:Panel runat="server" ID="pnlUpload">
        <p>
            You may use this tool to upload an Excel 2010 spreadsheet (.xlsx file) to an EpiServer Key-Value Pair List multi-property.
            For each row, the first column will become a key and the second column its value.  All other columns will be ignored.   
            PLEASE NOTE:  The first row in the sheet is assumed to be headers and is thus ignored.  Please
            format your spreadsheet appropriately. 
        </p>
        <asp:Label runat="server" ID="lblFile" AssociatedControlID="fileUpload" Text="File:" />
        <asp:FileUpload runat="server" ID="fileUpload" />
        <asp:Label runat="server" ID="lblSheetName" AssociatedControlID="tbSheetName" Text="Sheet Name:" />
        <asp:TextBox runat="server" ID="tbSheetName" Text="Sheet1" />
        <asp:Button runat="server" ID="btnLoadFile" Text="Upload" />
            
</asp:Panel>
    
<asp:Panel runat="server" ID="pnlPreSubmit" Visible="false">
        <p>
            The following Key-Value pairs will be added.  Once submitted, you will have to use EpiServer to delete these values.
        </p>
        <asp:GridView runat="server" ID="excelPreview" >
        </asp:GridView>
        <asp:CheckBox runat="server" ID="cbConfirm" Text="Confirm values." AutoPostBack="true" />
        <asp:Button runat="server" ID="btnSubmit" Text="Submit" Enabled="false" />
</asp:Panel>
<asp:Label runat="server" ID="lblError" />