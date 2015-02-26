<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="coursedetail.aspx.cs" Inherits="TheaterTemplate.Web.Pages.Courses.coursedetail" %>
<%@ Register TagPrefix="courses" TagName="CourseDetail" Src="~/Controls/Courses/CourseDetailControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <courses:CourseDetail runat="server" ID="coursesCourseDetail" />
</asp:Content>
