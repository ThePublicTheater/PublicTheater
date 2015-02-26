<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master"
    AutoEventWireup="true" CodeBehind="Archive.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.Archive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    
    <div class="tabWrapper showArchive">
        
        <div class="archiveHead">
            
            <asp:Repeater ID="rptVenueHeaders" runat="server">
            <HeaderTemplate>
                <ul class="venueFilters" id="myTab">
                    <li class="active selected"><a href="#" class="all">All Venues</a></li>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <asp:HyperLink ID="lnkVenueFilter" runat="server" href="#" />
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>

            <div class="filterBlock">
            <asp:UpdatePanel runat="server" ID="pnlFilters" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="filter">
                        <asp:Label ID="Label1" runat="server" Text="Year" AssociatedControlID="ddlYear" />
                        <asp:DropDownList runat="server" ID="ddlYear" AutoPostBack="True" OnSelectedIndexChanged="FilterChangedChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="filter">
                        <asp:Label ID="Label2" runat="server" Text="Month" AssociatedControlID="ddlMonth" />
                        <asp:DropDownList runat="server" ID="ddlMonth" AutoPostBack="True" OnSelectedIndexChanged="FilterChangedChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="filter">
                        <asp:Label ID="Label3" runat="server" Text="Genre" AssociatedControlID="ddlGenre" />
                        <asp:DropDownList runat="server" ID="ddlGenre" AutoPostBack="True" OnSelectedIndexChanged="FilterChangedChanged">
                        </asp:DropDownList>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="FilterPerfInput" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="filter archSearch">
                <asp:TextBox ID="FilterPerfInput" ClientIDMode="Static" OnTextChanged="FilterChangedChanged" runat="server" AutoPostBack="True" placeholder="Search Archive"></asp:TextBox>
                <asp:LinkButton ID="submitButtonGoesHere" runat="server" CssClass="archSearchBtn" OnClick="FilterChangedChanged"/>                
            </div>
        </div>

        </div>

        <div class="archiveBody">

            <asp:UpdatePanel runat="server" ID="pnlArchive" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Repeater runat="server" ID="rptVenues">
                        <ItemTemplate>
                            <div id='<%# Container.DataItem %>' class="subPane <%# Container.DataItem %>" >
                                <div class="mainHead">
                                    <h2><asp:Literal runat="server" ID="litThemeName">The Public Theatre</asp:Literal></h2>
                                </div>
                                <section>
                                    
                                    <asp:HiddenField runat="server" ID="itemPerPage" Value="3" />
                                    <ul class="archiveItem">
                                        <asp:Repeater runat="server" ID="rptVenueProductions">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:Image runat="server" ID="imgThumbnail" ImageUrl="http://placehold.it/370x238" />
                                                    <p>
                                                        <asp:HyperLink runat="server" ID="lbTitle"></asp:HyperLink>
                                                        <asp:Label runat="server" ID="lbDate"></asp:Label>
                                                    </p>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                    
                                </section>
                             </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <script type="text/javascript">
                        Sys.Application.add_load(WireEvents);
                        function WireEvents() {
                            window.archivePage.initialize();
                        }
                    </script>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    
    </div>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="BeforeCloseBodyContent" runat="server">
</asp:Content>
