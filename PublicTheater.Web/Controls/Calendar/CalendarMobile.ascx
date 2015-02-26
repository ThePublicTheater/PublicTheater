<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalendarMobile.ascx.cs" Inherits="TheaterTemplate.Web.Controls.CalendarControls.CalendarMobile" %>

<div data-bb="mobileCalendar" style="display:none">
    <div class="clearfix">
        <select class="pull-left input-medium" data-bb="fullMonthSelect">
            {{#each allCalendarMonths}}
                <option value="{{this.monthValue}}">{{this.monthText}}</option>
            {{/each}}
        </select>
    </div>
    {{#if loading}}
        <div class="alert alert-info">
            <strong>Loading performances...</strong>
        </div>
    {{else}}
        <ul class="nav nav-tabs nav-stacked">
            {{#each performances}}
                <li>
                    <a href="/reserve/index.aspx?performanceNumber={{this.id}}" class="clearfix">
                        <strong class="pull-left">{{this.title}}</strong>
                        <small class="pull-right">{{this.dateString}} {{this.timeString}}</small>
                    </a>
                </li>
            {{/each}}
        </ul>
        <p>{{performances.length}} performances found</p>
    {{/if}}
</div>