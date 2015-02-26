<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="download.ascx.cs" Inherits="TheaterTemplate.Web.Controls.MonitorControls.download" %>
<div>
    File paths to download:
    <div>
        <asp:TextBox runat="server" ID="imagesToDownload" TextMode="MultiLine" Columns="150" Rows="10">
        </asp:TextBox>
    </div>

    Download Folder:
    <div>
        <asp:TextBox runat="server" ID="downloadFolder" Columns="150">
        </asp:TextBox>
    </div>
    <div>
        If Modified After:
        <asp:TextBox runat="server" ID="modifiedAfter" />
    </div>
    <div>
        <asp:Button runat="server" ID="downloadImages" Text="DownloadImages" />
        <asp:Label runat="server" ID="downloadResults" EnableViewState="false" ForeColor="Red" />
    </div>
    <div>
        Upload zip of files: <asp:FileUpload runat="server" ID="uploadZip" />
    </div>
    <div>
        <asp:Button runat="server" ID="uploadImages" Text="Upload Images" />
    </div>
    <asp:Repeater runat="server" ID="uploadedFiles">
        <ItemTemplate>
            <div>
                <%= Eval("DataItem") %>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>