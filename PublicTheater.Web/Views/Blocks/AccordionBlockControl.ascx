<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccordionBlockControl.ascx.cs"
    Inherits="PublicTheater.Web.Views.Blocks.AccordionBlockControl" %>
<div class="section-auto-sample-accordion" data-section="accordion">
    <script type="text/javascript">
        function openAccordian() {
            $(".accordRow:contains('" + (function(a) {
                if (a == "") return {};
                var b = {};
                for (var i = 0; i < a.length; ++i) {
                    var p = a[i].split('=');
                    if (p.length != 2) continue;
                    b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
                }
                return b;
            })(window.location.search.substr(1).split('&')).position + "')").addClass('active');
        }

        $(openAccordian);

    </script>
    <asp:repeater runat="server" id="rpt">
        <itemtemplate>

            <section class="accordRow">

            <div class="title" data-section-title><a href="#section<%# Container.ItemIndex %>">  <%# Eval("Heading")%></a></div>
           
            <div class="content" data-slug="section<%# Container.ItemIndex %>" data-section-content>
                <%# Eval("Content")%>
            </div>
                </section>
        </itemtemplate>
    </asp:repeater>
</div>
