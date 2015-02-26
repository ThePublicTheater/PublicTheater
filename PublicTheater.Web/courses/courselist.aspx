<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="courselist.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Courses.courselist" %>
<%@ Register TagPrefix="courses" TagName="CourseListContainer" Src="~/Controls/Courses/CourseListContainer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    
<div id="courseListPage">
    <courses:CourseListContainer runat="server" ID="coursesListContainer" CourseJavaScriptFile="/js/courses/courseList.js" />
</div>

</asp:Content>
