<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CourseListContainer.ascx.cs" Inherits="TheaterTemplate.Web.Controls.CourseControls.CourseListContainer" %>

<%@ Register TagPrefix="courses" TagName="CourseListHeader" Src="~/Controls/Courses/CourseListHeader.ascx" %>
<%@ Register TagPrefix="courses" TagName="CourseListControl" Src="~/Controls/Courses/CourseListControl.ascx" %>
<%@ Register TagPrefix="courses" TagName="CourseListFilters" Src="~/Controls/Courses/CourseListFilters.ascx" %>


<div id="courseListHeader">
    <courses:CourseListHeader runat="server" ID="coursesListHeader" />
</div>

<div class="row-fluid">
    <div id="filterContainer" class="span3">
        <courses:CourseListFilters runat="server" ID="courseListFilters" />
    </div>

    <div id="courseListControl" class="span9">
        <courses:CourseListControl runat="server" ID="courseListControl" />
    </div>
</div>
