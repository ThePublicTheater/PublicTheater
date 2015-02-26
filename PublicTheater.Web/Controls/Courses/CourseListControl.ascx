<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CourseListControl.ascx.cs" Inherits="TheaterTemplate.Web.Controls.CourseControls.CourseListControl" %>

<asp:HiddenField runat="server" ID="defaultKeyword" ClientIDMode="Static" />

<%-- BRANDON - DOES IT MAKE SENSE TO USE THE SAME CSS ID (#listOfPackages) AS http://www.milwaukeerep.com/Packages/ ? --%>
<asp:UpdatePanel ID="pnlCoursesPanel" runat="server">
    <ContentTemplate>
        <%-- BELOW BUTTON Triggers Update --%>
        <asp:Button runat="server" ID="btnCourseFilterChanged" Style="display: none" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="selectedKeyword" ClientIDMode="Static" />
        <asp:Repeater runat="server" ID="rptCourses">
            <HeaderTemplate>
                <ul id="listOfPackages" class="unstyled">
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <div class="packageListItemWrapper row-fluid">
                        <div class="span8">
                            <h3>
                                <asp:Literal runat="server" ID="ltrCourseName" />
                            </h3>
                            <ul class="fieldList unstyled">
                                <li>
                                    <asp:Label runat="server" ID="lblStartDateLabel" Text="Start Date:" AssociatedControlID="lblStartDate" />
                                    <asp:Label runat="server" ID="lblStartDate" />
                                </li>
                                <li>
                                    <asp:Label runat="server" ID="lblLocationLabel" Text="Location:" AssociatedControlID="lblLocation" />
                                    <asp:Label runat="server" ID="lblLocation" />
                                </li>
                                <li runat="server" id="tuitionRow">
                                    <asp:Label runat="server" ID="lblTuitionLabel" Text="Tuition:" AssociatedControlID="lblTuition" />
                                    <asp:Label runat="server" ID="lblTuition" />
                                </li>
                                <li runat="server" id="requirementsContainer">
                                    <asp:Label runat="server" ID="lblRequirementsLabel" Text="Grade Requirements: " AssociatedControlID="rptRequirements" />
                                    <asp:Repeater runat="server" ID="rptRequirements">
                                        <ItemTemplate><asp:Literal runat="server" ID="ltrRequirement" /></ItemTemplate>
                                        <SeparatorTemplate>, </SeparatorTemplate>
                                    </asp:Repeater>
                                </li>
                            </ul>
                            <div class="description">
                                <asp:Literal runat="server" ID="ltrCourseDescription" />
                            </div>
                            <asp:Repeater runat="server" ID="rptAdditionalCallOuts">
                                <HeaderTemplate>
                                    <div class="callOuts">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p class="callOut">
                                        <asp:Literal runat="server" ID="ltrCallOut" />
                                    </p>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="span4">                    
                            <div class="buttonWrapper">
                                <asp:HyperLink ID="lnkLearnMore" runat="server" Text="Learn More" CssClass="btn" />
                                <asp:HyperLink ID="lnkSubscribeNow" runat="server" Text="Register" CssClass="btn" />
                            </div>
                        </div>
                    </div>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel runat="server" ID="pnlNoCourses" Visible="false" CssClass="noCoursesArea">
            <asp:Literal runat="server" ID="ltrNoCourses" />
        </asp:Panel>
        <div class="loadingContainer bgOverlay"><asp:Image runat="server" ID="loadingGif" ImageUrl="~/images/ajax-loader.gif" CssClass="loadingSpinner" /></div>
    </ContentTemplate>
</asp:UpdatePanel>