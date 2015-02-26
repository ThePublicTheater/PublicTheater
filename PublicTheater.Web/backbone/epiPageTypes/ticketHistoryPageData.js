define([

    "base/view"

], function (BaseView) {

    return BaseView.extend({
        archiveItemsSelector: ".pastItems",

        initialize: function () {
            quickPager = this.quickPager;
            $(this.archiveItemsSelector).each(function () {
                var $selector = $(this);
                var pageSize = $selector.prev().val();
                quickPager({ selector: $selector, pageSize: pageSize });
            });
        },

        quickPager: function (options) {

            var defaults = {
                selector: this,
                pageSize: 3,
                currentPage: 1,
                holder: null,
                pagerLocation: "after"
            };

            var options = $.extend(defaults, options);

            return $(options.selector).each(function () {
                var selector = $(this);
                var visibleChildren = selector.children(':visible');

                var pageCounter = Math.ceil(visibleChildren.length / options.pageSize);

                visibleChildren.each(function (i) {
                    if (i < options.currentPage * options.pageSize && i >= (options.currentPage - 1) * options.pageSize) {
                        this.style.display = "block";
                    } else {
                        this.style.display = "none";
                    }
                });

                //Build pager navigation if page number greater than 1
                var pageNav = "<ul class='simplePagerNav'>";
                if (pageCounter > 1) {
                    for (i = 1; i <= pageCounter; i++) {
                        if (i == options.currentPage) {
                            pageNav += "<li class='currentPage'><a rel='" + i + "' href='#'>" + i + "</a></li>";
                        } else {
                            pageNav += "<li><a rel='" + i + "' href='#'>" + i + "</a></li>";
                        }
                    }
                }
                pageNav += "</ul>";

                if (!options.holder) {
                    switch (options.pagerLocation) {
                        case "before":
                            selector.before(pageNav);
                            break;
                        case "both":
                            selector.before(pageNav);
                            selector.after(pageNav);
                            break;
                        default:
                            selector.after(pageNav);
                    }
                } else {
                    $(options.holder).html(pageNav);
                }

                //pager navigation behaviour
                selector.parent().find(".simplePagerNav a").click(function () {

                    //grab the REL attribute 
                    var clickedLink = $(this).attr("rel");
                    options.currentPage = clickedLink;

                    if (options.holder) {
                        $(this).parent("li").parent("ul").parent(options.holder).find("li.currentPage").removeClass("currentPage");
                        $(this).parent("li").parent("ul").parent(options.holder).find("a[rel='" + clickedLink + "']").parent("li").addClass("currentPage");
                    } else {
                        //remove current current (!) page
                        $(this).parent("li").parent("ul").find("li.currentPage").removeClass("currentPage");
                        //Add current page highlighting
                        $(this).parent("li").parent("ul").find("a[rel='" + clickedLink + "']").parent("li").addClass("currentPage");
                    }

                    //hide and show relevant links
                    visibleChildren.each(function (i) {
                        if (i < options.currentPage * options.pageSize && i >= (options.currentPage - 1) * options.pageSize) {
                            this.style.display = "block";
                        } else {
                            this.style.display = "none";
                        }
                    });
                    return false;
                });
            });
        }
    });
});
