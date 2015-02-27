<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CourseDetailControl.ascx.cs" Inherits="TheaterTemplate.Web.Controls.CourseControls.CourseDetailControl" %>

<h2>
    <asp:Literal runat="server" ID="ltrHeader" />
</h2>

<div id="listOfPackages" class="packageDetail">
    <div class="leftSide">
        <div class="description">
            <asp:Literal runat="server" ID="ltrCourseDescription" />
        </div>
    </div>
    <div class="subscribeArea">
        <div id="videoArea">
            <asp:Image runat="server" ID="imgCourseImage" />
        </div>
        <div class="buttonWrapper">
            <asp:LinkButton ID="lbRegister" runat="server" Text="Register" CssClass="btn" />
        </div>
    </div>
</div>
