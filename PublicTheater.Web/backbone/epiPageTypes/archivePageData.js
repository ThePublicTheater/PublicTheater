define([

    "base/view"

], function (BaseView) {

    return BaseView.extend({
        archiveItemsSelector: ".archiveItem",
        venueFilters: ".venueFilters a",

        initialize: function () {
            quickPager = this.quickPager;
            $(this.archiveItemsSelector).each(function () {
                var $selector = $(this);
                var pageSize = $selector.prev().val();
                quickPager({ selector: $selector, pageSize: pageSize });
            });

            $(this.venueFilters).click(function (e) {

                e.preventDefault();
                var $selector = $(this);
                var theme = $selector.attr("class");

                if (theme == "all") {
                    $(".subPane").show();
                } else {
                    $(".subPane").hide();
                    $("#" + theme).show();
                }


                $(".venueFilters li").removeClass("active selected");
                $selector.parent().addClass("active selected");

                return false;
            });

            this.$('.venueFilters li.active a').click();

            $(".filter select").change(function () {
                $("#FilterPerfInput").val("");
            });
        },

        quickPager: function (options) {

            var defaults = {
                selector: this,
                pageSize: 9,
                currentPage: 1,
                holder: null,
                pagerLocation: "after",
                maxPageCount: 5
            };

            var getPagerHtml = function (optionsValue) {
                var pageNav = "<ul class='simplePagerNav'>";

                var visibleChildren = optionsValue.selector.children();
                var pageCounter = Math.ceil(visibleChildren.length / optionsValue.pageSize);
                if (pageCounter > 1) {
                    var start = (Math.ceil(optionsValue.currentPage / optionsValue.maxPageCount) - 1) * optionsValue.maxPageCount + 1;
                    if (start > 1) {
                        pageNav += "<li class='prev'><a rel='1' href='#' >&lt;&lt;first</a></li>";
                        pageNav += "<li class='prev'><a rel='" + (start - 1) + "' href='#' >&lt;prev</a></li>";
                    }

                    var end = Math.min(start + optionsValue.maxPageCount - 1, pageCounter);
                    for (var i = start; i <= end; i++) {
                        if (i == optionsValue.currentPage) {
                            pageNav += "<li class='currentPage'><a rel='" + i + "' href='#'>" + i + "</a></li>";
                        } else {
                            pageNav += "<li><a rel='" + i + "' href='#'>" + i + "</a></li>";
                        }
                    }

                    if (end < pageCounter) {
                        pageNav += "<li class='next'><a rel='" + (end + 1) + "' href='#' >next&gt;</a></li>";
                        pageNav += "<li class='next'><a rel='" + pageCounter + "' href='#' >last&gt;&gt;</a></li>";
                    }
                }
                pageNav += "</ul>";
                return pageNav;
            }

            options = $.extend(defaults, options);

            return $(options.selector).each(function () {
                var selector = $(this);
                var visibleChildren = selector.children();

                //Build pager navigation if page number greater than 1
                var pageNav = getPagerHtml(options);

                if (!options.holder) {
                    switch (options.pagerLocation) {
                        case "before":
                            selector.prev('.simplePagerNav').remove();
                            selector.before(pageNav);

                            break;
                        case "both":
                            selector.prev('.simplePagerNav').remove();
                            selector.before(pageNav);
                            selector.next('.simplePagerNav').remove();
                            selector.after(pageNav);
                            break;
                        default:
                            selector.next('.simplePagerNav').remove();
                            selector.after(pageNav);
                    }
                } else {
                    $(options.holder).html(pageNav);
                }

                //show items on current page
                visibleChildren.each(function (i) {
                    if (i < options.currentPage * options.pageSize && i >= (options.currentPage - 1) * options.pageSize) {
                        this.style.display = "block";
                    } else {
                        this.style.display = "none";
                    }
                });

                //pager navigation behaviour
                selector.parent().find(".simplePagerNav a").click(function () {
                    //grab the REL attribute 
                    var clickedLink = $(this).attr("rel");
                    options.currentPage = parseInt(clickedLink);
                    quickPager(options);
                    
                    return false;
                });
            });
        }
    });
});
