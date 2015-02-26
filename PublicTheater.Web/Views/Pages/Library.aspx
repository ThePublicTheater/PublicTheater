<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/Interior.master" AutoEventWireup="true" CodeBehind="Library.aspx.cs" Inherits="PublicTheater.Web.Views.Pages.Library" %>
<%@ Register TagPrefix="uc" TagName="OpenTable" Src="~/Views/Controls/OpenTable.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <uc:OpenTable runat="server" ID="opentable1"></uc:OpenTable>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PrimaryContentBottomSection" runat="server">
    <div class="interiorPage library">

        <div class="tabWrapper">

            <ul class="nav-tabs tabs-6" id="myTab">
                  <li class="active"><a class="selected" href="#about">About</a></li>
                  <%--<li><a class="linkedtab" href="/Global/The%20Library%20Lounge/NYE.pdf">New Year’s Eve</a></li>--%>
                  <li><a href="#RestaurantWeek">Restaurant Week</a></li>
                  <li><a href="#ValentinesDay">Valentine's Day</a></li>
                  <li><a href="#menu">Menu</a></li>
                  <li><a href="#events">Events</a></li>
                  <li><a href="#press">Press</a></li>
            </ul>
 
            <div class="tab-content">

                <div class="tab-pane active" id="about">

                    <div class="aboutInfo">
                        <div class="large-3 medium-3 small-12">
                            <strong>Location</strong>
                            <EPiServer:Property ID="aboutLocation" runat="server" PropertyName="AboutLocation" />
                        </div>
                        <div class="large-3 medium-3 small-12">
                            <strong>Hours</strong>
                            <EPiServer:Property ID="aboutHours" runat="server" PropertyName="AboutHours" />
                        </div>
                        <div class="large-3 medium-3 small-12">
                            <strong>Contact</strong>
                            <EPiServer:Property ID="aboutContact" runat="server" PropertyName="AboutContact" />
                        </div>
                        <div class="large-3 medium-3 small-12">
                            <strong>Connect with The Library</strong>
                            <EPiServer:Property ID="aboutConnect" runat="server" PropertyName="AboutConnect" />
                        </div>
                    </div>

                    <div class="aboutContent">
                        <div class="large-6 medium-6 small-12">
                            <EPiServer:Property ID="Property1" runat="server" PropertyName="AboutImage"  CustomTagName="div" CssClass="aboutImage"/>
                        </div>
                        <div class="large-6 medium-6 small-12">
                            <EPiServer:Property ID="aboutInformation" runat="server" PropertyName="AboutInformation" />
                        </div>
                    </div>
                </div>
                <div class="tab-pane" id="RestaurantWeek">
                    <EPiServer:Property ID="Property3" runat="server" PropertyName="RestaurantWeek" DisplayMissingMessage="False" />
                    </div>
                <div class="tab-pane" id="ValentinesDay">
                    <EPiServer:Property ID="Property4" runat="server" PropertyName="ValentinesDay" DisplayMissingMessage="False"/>
                    </div>
                <div class="tab-pane" id="menu">
                    <div class="tabWrapper nested">
                        <div class="large-3 medium-3 small-12">
                            <ul class="nav-tabs tabs-2" id="menuTabs">
                                <li class="active"><a class="selected" href="#dinner">Dinner</a></li>
                                <li><a href="#drinks">Drinks</a></li>                
                            </ul>               
                        </div>

                        <div class="large-9 medium-9 small-12"> 
                        <div class="menuStyles">    
                            <div class="tab-content">                   
                                <div class="tab-pane active" id="dinner">
                                    <asp:Repeater runat="server" ID="menuItems">
                                        <HeaderTemplate>
                                            <div class="accordion">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <strong><%# Eval("Heading") %></strong>
                                            <div>
                                                <%# Eval("Content") %>
                                            </div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                
                                <div class="tab-pane" id="drinks">
                                    <asp:Repeater runat="server" ID="drinkItems">
                                        <HeaderTemplate>
                                            <div class="accordion">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <strong><%# Eval("Heading") %></strong>
                                            <div>
                                                <%# Eval("Content") %>
                                            </div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                    </div>
                </div>

                <div class="tab-pane" id="events">
                    <div class="large-4 medium-6 small-12">
                         <EPiServer:Property ID="Property2" runat="server" PropertyName="EventImage"  CustomTagName="div" CssClass="eventImage"/>
                    </div>

                    <div class="large-8 medium-6 small-12">
                        <div class="eventInfo">
                            <EPiServer:Property ID="eventInformation" runat="server" PropertyName="EventInformation" />
                        </div>
                    </div>
                </div>

                <div class="tab-pane" id="press">
                    <EPiServer:Property ID="pressContent" runat="server" PropertyName="PressContent" />
                </div>

            </div>

        </div>

    </div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="BeforeCloseBodyContent" runat="server">
</asp:Content>
