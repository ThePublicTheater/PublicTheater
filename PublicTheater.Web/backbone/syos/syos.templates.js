(function () {
    var template = Handlebars.template, templates = Handlebars.templates = Handlebars.templates || {};
    templates['levelSwitch.html'] = template(function (Handlebars, depth0, helpers, partials, data) {
        this.compilerInfo = [3, '>= 1.0.0-rc.4'];
        helpers = helpers || Handlebars.helpers; data = data || {};
        var buffer = "", stack1, stack2, functionType = "function", escapeExpression = this.escapeExpression, self = this;

        function program1(depth0, data) {

            var buffer = "", stack1, stack2;
            buffer += "\r\n	    <select class=\"syos-level-dropdown\" id=\"changeLevelSelect\" data-bb=\"changeLevelSelect\">\r\n	    	<option value=\"\">Choose a Level</option>\r\n	        ";
            stack2 = helpers.each.call(depth0, ((stack1 = ((stack1 = depth0.syos), stack1 == null || stack1 === false ? stack1 : stack1.levels)), stack1 == null || stack1 === false ? stack1 : stack1.models), { hash: {}, inverse: self.noop, fn: self.program(2, program2, data), data: data });
            if (stack2 || stack2 === 0) { buffer += stack2; }
            buffer += "\r\n	    </select>\r\n	    ";
            stack2 = helpers['if'].call(depth0, ((stack1 = ((stack1 = ((stack1 = depth0.syos), stack1 == null || stack1 === false ? stack1 : stack1.settings)), stack1 == null || stack1 === false ? stack1 : stack1.attributes)), stack1 == null || stack1 === false ? stack1 : stack1.levelSelectMap), { hash: {}, inverse: self.noop, fn: self.program(4, program4, data), data: data });
            if (stack2 || stack2 === 0) { buffer += stack2; }
            buffer += "\r\n	";
            return buffer;
        }
        function program2(depth0, data) {

            var buffer = "", stack1;
            buffer += "\r\n	            <option value=\""
			  + escapeExpression(((stack1 = ((stack1 = depth0.attributes), stack1 == null || stack1 === false ? stack1 : stack1.LevelName)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "\">"
			  + escapeExpression(((stack1 = ((stack1 = depth0.attributes), stack1 == null || stack1 === false ? stack1 : stack1.LevelName)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "</option>\r\n	        ";
            return buffer;
        }

        function program4(depth0, data) {


            return "\r\n	    	<span data-bb-event=\"change-levels\" class=\"syos-level-map-button\">Map</span>\r\n		";
        }

        function program6(depth0, data) {

            var buffer = "", stack1;
            buffer += "\r\n	    <span>You are viewing the</span>\r\n	    <h,4>"
			  + escapeExpression(((stack1 = ((stack1 = ((stack1 = ((stack1 = depth0.syos), stack1 == null || stack1 === false ? stack1 : stack1.activeLevel)), stack1 == null || stack1 === false ? stack1 : stack1.attributes)), stack1 == null || stack1 === false ? stack1 : stack1.LevelName)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "</h4>\r\n	    <a href=\"#\" id=\"changeLevels\" data-bb-event=\"change-levels\" class=\"syos-button\">Change Levels</a>\r\n	";
            return buffer;
        }

        buffer += "<div class=\"syos-change-level-inner\">\r\n	";
        stack2 = helpers['if'].call(depth0, ((stack1 = ((stack1 = ((stack1 = depth0.syos), stack1 == null || stack1 === false ? stack1 : stack1.settings)), stack1 == null || stack1 === false ? stack1 : stack1.attributes)), stack1 == null || stack1 === false ? stack1 : stack1.levelSelectDropdown), { hash: {}, inverse: self.program(6, program6, data), fn: self.program(1, program1, data), data: data });
        if (stack2 || stack2 === 0) { buffer += stack2; }
        buffer += "\r\n</div>";
        return buffer;
    });
    templates['popup.html'] = template(function (Handlebars, depth0, helpers, partials, data) {
        this.compilerInfo = [3, '>= 1.0.0-rc.4'];
        helpers = helpers || Handlebars.helpers; data = data || {};
        var buffer = "", stack1, stack2, functionType = "function", escapeExpression = this.escapeExpression, self = this;

        function program1(depth0, data) {

            var buffer = "", stack1;
            buffer += "\r\n            <img src=\""
			  + escapeExpression(((stack1 = ((stack1 = ((stack1 = depth0.ActiveSeat), stack1 == null || stack1 === false ? stack1 : stack1.SeatTypeInfo)), stack1 == null || stack1 === false ? stack1 : stack1.CartImage)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "\" height=\"40\" title=\""
			  + escapeExpression(((stack1 = ((stack1 = ((stack1 = depth0.ActiveSeat), stack1 == null || stack1 === false ? stack1 : stack1.SeatTypeInfo)), stack1 == null || stack1 === false ? stack1 : stack1.Description)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "\" />\r\n        ";
            return buffer;
        }

        function program3(depth0, data) {

            var buffer = "", stack1;
            buffer += "\r\n        <div class=\"popup-type-message\">\r\n            "
			  + escapeExpression(((stack1 = ((stack1 = ((stack1 = depth0.ActiveSeat), stack1 == null || stack1 === false ? stack1 : stack1.SeatTypeInfo)), stack1 == null || stack1 === false ? stack1 : stack1.SmallPrompt)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "\r\n        </div>\r\n    ";
            return buffer;
        }

        function program5(depth0, data) {

            var buffer = "", stack1, stack2;
            buffer += "\r\n            <tr>\r\n                <td class=\"priceDescription\">\r\n                    "
			  + escapeExpression(((stack1 = ((stack1 = depth0.attributes), stack1 == null || stack1 === false ? stack1 : stack1.PriceTypeDescription)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "\r\n                </td>\r\n                <!-- if discounted -->\r\n                <td>\r\n                    <span class=\"syos-price syos-price-discounted\">"
			  + escapeExpression(((stack1 = ((stack1 = depth0.attributes), stack1 == null || stack1 === false ? stack1 : stack1.defaultPriceFormatted)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "</span>\r\n                </td>\r\n                <td>\r\n                    <span class=\"syos-price\">"
			  + escapeExpression(((stack1 = ((stack1 = depth0.attributes), stack1 == null || stack1 === false ? stack1 : stack1.priceFormatted)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "</span>\r\n                </td>\r\n                <td>\r\n                    ";
            stack2 = helpers.unless.call(depth0, ((stack1 = depth0.attributes), stack1 == null || stack1 === false ? stack1 : stack1.customAction), { hash: {}, inverse: self.program(8, program8, data), fn: self.program(6, program6, data), data: data });
            if (stack2 || stack2 === 0) { buffer += stack2; }
            buffer += "\r\n                </td>\r\n            </tr>\r\n        ";
            return buffer;
        }
        function program6(depth0, data) {


            return "\r\n                        <span class=\"popup-addToCart syos-button\">Add</span>\r\n                    ";
        }

        function program8(depth0, data) {


            return "\r\n                        <a href=\"/account/login.aspx?access=maycenter\" class=\"syos-button\">Login</a>\r\n                    ";
        }

        buffer += "<div class=\"popup-header\">\r\n    <div class=\"popup-seatType\">\r\n        ";
        stack2 = helpers['if'].call(depth0, ((stack1 = ((stack1 = depth0.ActiveSeat), stack1 == null || stack1 === false ? stack1 : stack1.SeatTypeInfo)), stack1 == null || stack1 === false ? stack1 : stack1.CartImage), { hash: {}, inverse: self.noop, fn: self.program(1, program1, data), data: data });
        if (stack2 || stack2 === 0) { buffer += stack2; }
        buffer += "\r\n    </div>\r\n    <div class=\"popup-title\">\r\n        <h2>"
		  + escapeExpression(((stack1 = ((stack1 = ((stack1 = depth0.ActiveSeat), stack1 == null || stack1 === false ? stack1 : stack1.attributes)), stack1 == null || stack1 === false ? stack1 : stack1.SectionDescription)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
		  + "</h2>\r\n        <h3>Row "
		  + escapeExpression(((stack1 = ((stack1 = ((stack1 = depth0.ActiveSeat), stack1 == null || stack1 === false ? stack1 : stack1.attributes)), stack1 == null || stack1 === false ? stack1 : stack1.RowText)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
		  + " , Seat "
		  + escapeExpression(((stack1 = ((stack1 = ((stack1 = depth0.ActiveSeat), stack1 == null || stack1 === false ? stack1 : stack1.attributes)), stack1 == null || stack1 === false ? stack1 : stack1.NumberText)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
		  + "</h3>\r\n    </div>\r\n    <div class=\"popup-close\">\r\n        <span class=\"popup-closeButton syos-button\">x</span>\r\n    </div>\r\n</div>\r\n<div class=\"popup-content\">\r\n    ";
        stack2 = helpers['if'].call(depth0, ((stack1 = ((stack1 = depth0.ActiveSeat), stack1 == null || stack1 === false ? stack1 : stack1.SeatTypeInfo)), stack1 == null || stack1 === false ? stack1 : stack1.SmallPrompt), { hash: {}, inverse: self.noop, fn: self.program(3, program3, data), data: data });
        if (stack2 || stack2 === 0) { buffer += stack2; }
        buffer += "\r\n    <table class=\"popup-priceTable\">\r\n        ";
        stack2 = helpers.each.call(depth0, ((stack1 = ((stack1 = depth0.ActiveSeat), stack1 == null || stack1 === false ? stack1 : stack1.PriceCollection)), stack1 == null || stack1 === false ? stack1 : stack1.models), { hash: {}, inverse: self.noop, fn: self.program(5, program5, data), data: data });
        if (stack2 || stack2 === 0) { buffer += stack2; }
        buffer += "\r\n    </table>\r\n</div>";
        return buffer;
    });
    templates['cart.html'] = template(function (Handlebars, depth0, helpers, partials, data) {
        this.compilerInfo = [3, '>= 1.0.0-rc.4'];
        helpers = helpers || Handlebars.helpers; data = data || {};
        var buffer = "", stack1, options, self = this, functionType = "function", escapeExpression = this.escapeExpression, helperMissing = helpers.helperMissing;

        function program1(depth0, data) {

            var buffer = "", stack1;
            buffer += "\r\n        ";
            stack1 = helpers['if'].call(depth0, depth0.isExpanded, { hash: {}, inverse: self.program(4, program4, data), fn: self.program(2, program2, data), data: data });
            if (stack1 || stack1 === 0) { buffer += stack1; }
            buffer += "\r\n    ";
            return buffer;
        }
        function program2(depth0, data) {


            return "\r\n        <span class=\"syos-button syos-fl-left\" data-bb=\"expandButton\">Collapse</span>\r\n        ";
        }

        function program4(depth0, data) {


            return "\r\n        <span class=\"syos-button syos-fl-left\" data-bb=\"expandButton\">Expand</span>\r\n        ";
        }

        function program6(depth0, data) {


            return "syos-cart-expanded";
        }

        function program8(depth0, data) {

            var buffer = "", stack1, stack2, options;
            buffer += "\r\n            <tr data-seat-id=\"";
            if (stack1 = helpers.SeatNumber) { stack1 = stack1.call(depth0, { hash: {}, data: data }); }
            else { stack1 = depth0.SeatNumber; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
            buffer += escapeExpression(stack1)
			  + "\">\r\n                <td><span class=\"syos-cart-remove\" data-bb-event=\"remove\" title=\"Remove this seat\">x</span></td>\r\n                <td>";
            stack2 = helpers['if'].call(depth0, ((stack1 = depth0.info), stack1 == null || stack1 === false ? stack1 : stack1.CartImage), { hash: {}, inverse: self.noop, fn: self.program(9, program9, data), data: data });
            if (stack2 || stack2 === 0) { buffer += stack2; }
            buffer += "</td>\r\n                <td> ";
            stack2 = helpers['if'].call(depth0, ((stack1 = depth0.info), stack1 == null || stack1 === false ? stack1 : stack1.FullPrompt), { hash: {}, inverse: self.noop, fn: self.program(11, program11, data), data: data });
            if (stack2 || stack2 === 0) { buffer += stack2; }
            buffer += "</td>\r\n                <td><span class=\"syos-cart-seat-level\">";
            if (stack2 = helpers.SectionDescription) { stack2 = stack2.call(depth0, { hash: {}, data: data }); }
            else { stack2 = depth0.SectionDescription; stack2 = typeof stack2 === functionType ? stack2.apply(depth0) : stack2; }
            buffer += escapeExpression(stack2)
			  + "</span></td>\r\n                <td><span class=\"syos-cart-seat-description\">Row ";
            if (stack2 = helpers.RowText) { stack2 = stack2.call(depth0, { hash: {}, data: data }); }
            else { stack2 = depth0.RowText; stack2 = typeof stack2 === functionType ? stack2.apply(depth0) : stack2; }
            buffer += escapeExpression(stack2)
			  + ", Seat ";
            if (stack2 = helpers.NumberText) { stack2 = stack2.call(depth0, { hash: {}, data: data }); }
            else { stack2 = depth0.NumberText; stack2 = typeof stack2 === functionType ? stack2.apply(depth0) : stack2; }
            buffer += escapeExpression(stack2)
			  + "</span></td>\r\n                <td class=\"syos-cart-right\">\r\n                    ";
            stack2 = helpers['if'].call(depth0, ((stack1 = depth0.pricing), stack1 == null || stack1 === false ? stack1 : stack1.IsDiscounted), { hash: {}, inverse: self.noop, fn: self.program(13, program13, data), data: data });
            if (stack2 || stack2 === 0) { buffer += stack2; }
            buffer += "\r\n                    <span class=\"syos-price\">";
            options = { hash: {}, data: data };
            buffer += escapeExpression(((stack1 = helpers.formattedPrice), stack1 ? stack1.call(depth0, ((stack1 = depth0.pricing), stack1 == null || stack1 === false ? stack1 : stack1.Price), options) : helperMissing.call(depth0, "formattedPrice", ((stack1 = depth0.pricing), stack1 == null || stack1 === false ? stack1 : stack1.Price), options)))
			  + "</span>\r\n                </td>\r\n            </tr>\r\n            ";
            return buffer;
        }
        function program9(depth0, data) {

            var buffer = "", stack1, stack2;
            buffer += "<img height=\"26\" src=\"";
            stack2 = ((stack1 = ((stack1 = depth0.info), stack1 == null || stack1 === false ? stack1 : stack1.CartImage)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1);
            if (stack2 || stack2 === 0) { buffer += stack2; }
            buffer += "\" title=\""
			  + escapeExpression(((stack1 = ((stack1 = depth0.info), stack1 == null || stack1 === false ? stack1 : stack1.Description)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "\" />";
            return buffer;
        }

        function program11(depth0, data) {


            return "<span class=\"syos-button syos-button-single\" data-bb-event=\"info\">?</span>";
        }

        function program13(depth0, data) {

            var buffer = "", stack1, options;
            buffer += "\r\n                        <span class=\"syos-price syos-price-discounted\">";
            options = { hash: {}, data: data };
            buffer += escapeExpression(((stack1 = helpers.formattedPrice), stack1 ? stack1.call(depth0, ((stack1 = depth0.pricing), stack1 == null || stack1 === false ? stack1 : stack1.DefaultPrice), options) : helperMissing.call(depth0, "formattedPrice", ((stack1 = depth0.pricing), stack1 == null || stack1 === false ? stack1 : stack1.DefaultPrice), options)))
			  + "</span>\r\n                    ";
            return buffer;
        }

        buffer += "<div class=\"syos-cart-header\">\r\n    ";
        stack1 = helpers['if'].call(depth0, depth0.showExpandButton, { hash: {}, inverse: self.noop, fn: self.program(1, program1, data), data: data });
        if (stack1 || stack1 === 0) { buffer += stack1; }
        buffer += "\r\n    <span class=\"syos-button syos-fl-right\" data-bb=\"reserveButton\">Reserve</span>\r\n</div>\r\n<div class=\"syos-cart-body ";
        stack1 = helpers['if'].call(depth0, depth0.isExpanded, { hash: {}, inverse: self.noop, fn: self.program(6, program6, data), data: data });
        if (stack1 || stack1 === 0) { buffer += stack1; }
        buffer += "\">\r\n    <table class=\"syos-cart-table\">\r\n        <tbody data-bb=\"exchangeItemTarget\">\r\n            ";
        stack1 = helpers.each.call(depth0, depth0.seats, { hash: {}, inverse: self.noop, fn: self.program(8, program8, data), data: data });
        if (stack1 || stack1 === 0) { buffer += stack1; }
        buffer += "\r\n        </tbody>\r\n    </table>\r\n</div>\r\n<div class=\"syos-cart-footer\">\r\n    <span class=\"syos-total syos-fl-right\">Total: $";
        options = { hash: {}, data: data };
        buffer += escapeExpression(((stack1 = helpers.formattedPrice), stack1 ? stack1.call(depth0, depth0.total, options) : helperMissing.call(depth0, "formattedPrice", depth0.total, options)))
		  + "</span>\r\n</div>";
        return buffer;
    });
    templates['display.html'] = template(function (Handlebars, depth0, helpers, partials, data) {
        this.compilerInfo = [3, '>= 1.0.0-rc.4'];
        helpers = helpers || Handlebars.helpers; data = data || {};
        var buffer = "", stack1, functionType = "function", escapeExpression = this.escapeExpression, self = this;

        function program1(depth0, data) {

            var buffer = "", stack1;
            buffer += "\r\n    <div id=\"chooseLevel\" class=\"syos-choose-level ";
            if (stack1 = helpers.levelClass) { stack1 = stack1.call(depth0, { hash: {}, data: data }); }
            else { stack1 = depth0.levelClass; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
            buffer += escapeExpression(stack1)
			  + "\">\r\n        <div class=\"syos-choose-level-inner\">\r\n            <div id=\"chooseCommand\" class=\"syos-choose-command\">\r\n                Please Select a Level for Your Seats\r\n            </div>\r\n            <ul id=\"levelSummary\" class=\"syos-level-summary\"></ul>\r\n            <img id=\"houseView\" usemap=\"#houseViewImgMap\" src=\"data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==\" class=\"syos-house-overlay\" />\r\n            ";
            stack1 = helpers.each.call(depth0, depth0.levelViews, { hash: {}, inverse: self.noop, fn: self.program(2, program2, data), data: data });
            if (stack1 || stack1 === 0) { buffer += stack1; }
            buffer += "\r\n            <map name=\"houseViewImgMap\" id=\"houseViewImgMap\" class=\"syos-level-image-map\">\r\n                ";
            stack1 = helpers.each.call(depth0, depth0.levelViews, { hash: {}, inverse: self.noop, fn: self.program(4, program4, data), data: data });
            if (stack1 || stack1 === 0) { buffer += stack1; }
            buffer += "\r\n            </map>\r\n        </div>\r\n    </div>\r\n";
            return buffer;
        }
        function program2(depth0, data) {

            var buffer = "", stack1;
            buffer += "\r\n                <img src=\"";
            if (stack1 = helpers.ImageUrl) { stack1 = stack1.call(depth0, { hash: {}, data: data }); }
            else { stack1 = depth0.ImageUrl; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
            buffer += escapeExpression(stack1)
			  + "\" class=\"syos-house-image houseView_level";
            if (stack1 = helpers.LevelId) { stack1 = stack1.call(depth0, { hash: {}, data: data }); }
            else { stack1 = depth0.LevelId; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
            buffer += escapeExpression(stack1)
			  + "\" />\r\n            ";
            return buffer;
        }

        function program4(depth0, data) {

            var buffer = "", stack1;
            buffer += "\r\n                    <area shape=\"poly\" coords=\"";
            if (stack1 = helpers.ImageMapCoords) { stack1 = stack1.call(depth0, { hash: {}, data: data }); }
            else { stack1 = depth0.ImageMapCoords; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
            buffer += escapeExpression(stack1)
			  + "\" href=\"#level";
            if (stack1 = helpers.LevelId) { stack1 = stack1.call(depth0, { hash: {}, data: data }); }
            else { stack1 = depth0.LevelId; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
            buffer += escapeExpression(stack1)
			  + "\">\r\n                ";
            return buffer;
        }

        function program6(depth0, data) {


            return "\r\n    <div id=\"syosTools\" class=\"mapView\">\r\n        <div id=\"syosFullScreenToggle\" data-bb-event=\"toggle-fullscreen\">\r\n            Fullscreen\r\n        </div>\r\n        <div id=\"move\">\r\n            <div id=\"moveN\"></div>\r\n            <div id=\"moveE\"></div>\r\n            <div id=\"moveS\"></div>\r\n            <div id=\"moveW\"></div>\r\n            <span>Move</span>\r\n        </div>\r\n        <div id=\"zoom\">\r\n            <span class=\"zoomIn\">+</span>\r\n            <div id=\"zoomSlider\"></div>\r\n            <span class=\"zoomOut\">-</span>\r\n            <div id=\"zoomValue\"></div>\r\n        </div>\r\n        <div id=\"toggleView\" class=\"syos-toggle-view\">\r\n            <a href=\"#\" id=\"togglePhotos\" data-bb-event=\"toggle-seat-view\"><img src=\"/syos/img/icon_photo.png\" alt=\"View photos from seats\" />View from Seats</a>\r\n        </div>\r\n    </div>\r\n";
        }

        buffer += "<div id=\"syos-modal\"></div>\r\n<div id=\"syos-modal-overlay\"></div>\r\n<div id=\"syos-full-loading\"></div>\r\n";
        stack1 = helpers['if'].call(depth0, depth0.showLevelMap, { hash: {}, inverse: self.noop, fn: self.program(1, program1, data), data: data });
        if (stack1 || stack1 === 0) { buffer += stack1; }
        buffer += "\r\n";
        stack1 = helpers['if'].call(depth0, depth0.showToolsView, { hash: {}, inverse: self.noop, fn: self.program(6, program6, data), data: data });
        if (stack1 || stack1 === 0) { buffer += stack1; }
        buffer += "\r\n<div id=\"changeLevel\" class=\"mapView syos-change-level\">\r\n</div>\r\n<div id=\"syosCanvas\" class=\"syos-canvas\">\r\n    <div id=\"seatsWrap\" class=\"syos-seats-wrap\">\r\n        <div id=\"circleMap\" class=\"syos-circle-map\"></div>\r\n        <div id=\"levelsWrap\">\r\n            <div id=\"seatPopUp\">\r\n                <div id=\"seatInfo\"></div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<div id=\"syosLoading\" class=\"syos-loading\">\r\n    <span id=\"syos-loading-text\" class=\"syos-loading-text\">Loading</span>\r\n    <img src=\"/syos/img/loading_seats.gif\" class=\"syos-loading-image\" alt=\"Loading...\" />\r\n</div>\r\n";
        return buffer;
    });
    templates['modal.html'] = template(function (Handlebars, depth0, helpers, partials, data) {
        this.compilerInfo = [3, '>= 1.0.0-rc.4'];
        helpers = helpers || Handlebars.helpers; data = data || {};
        var buffer = "", stack1, functionType = "function", escapeExpression = this.escapeExpression, self = this;

        function program1(depth0, data) {


            return "\r\n    <span class='syos-button' data-bb-event=\"close\">x</span>\r\n    ";
        }

        function program3(depth0, data) {

            var buffer = "", stack1;
            buffer += "\r\n        <span class=\"syos-button\" data-bb-event=\"action\">"
			  + escapeExpression(((stack1 = depth0.ButtonText), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "</span>\r\n    ";
            return buffer;
        }

        buffer += "<div class=\"syos-modal-header\">\r\n    <span class=\"syos-modal-title\">";
        if (stack1 = helpers.Title) { stack1 = stack1.call(depth0, { hash: {}, data: data }); }
        else { stack1 = depth0.Title; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
        buffer += escapeExpression(stack1)
		  + "</span>\r\n    ";
        stack1 = helpers['if'].call(depth0, depth0.CanDismiss, { hash: {}, inverse: self.noop, fn: self.program(1, program1, data), data: data });
        if (stack1 || stack1 === 0) { buffer += stack1; }
        buffer += "\r\n</div>\r\n<div class=\"syos-modal-content\">\r\n    ";
        if (stack1 = helpers.Content) { stack1 = stack1.call(depth0, { hash: {}, data: data }); }
        else { stack1 = depth0.Content; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
        if (stack1 || stack1 === 0) { buffer += stack1; }
        buffer += "\r\n</div>\r\n<div class=\"syos-modal-actions\">\r\n    ";
        stack1 = helpers.each.call(depth0, depth0.Actions, { hash: {}, inverse: self.noop, fn: self.program(3, program3, data), data: data });
        if (stack1 || stack1 === 0) { buffer += stack1; }
        buffer += "\r\n</div>";
        return buffer;
    });
    templates['exchangeCart.html'] = template(function (Handlebars, depth0, helpers, partials, data) {
        this.compilerInfo = [3, '>= 1.0.0-rc.4'];
        helpers = helpers || Handlebars.helpers; data = data || {};
        var buffer = "", stack1, options, functionType = "function", escapeExpression = this.escapeExpression, self = this, helperMissing = helpers.helperMissing;

        function program1(depth0, data) {


            return "\r\n        <span class=\"syos-button syos-fl-left\" data-bb=\"expandButton\">Expand</span>\r\n    ";
        }

        function program3(depth0, data) {


            return "\r\n        <span class=\"syos-button syos-fl-left\" data-bb=\"expandButton\">Collapse</span>\r\n    ";
        }

        function program5(depth0, data) {


            return "syos-cart-expanded";
        }

        function program7(depth0, data) {

            var buffer = "", stack1, stack2, options;
            buffer += "\r\n            <tr data-seat-id=\""
			  + escapeExpression(((stack1 = depth0.SeatNumber), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "\">\r\n                <td><span class=\"syos-cart-remove\" data-bb-event=\"remove\" title=\"Remove this seat\">x</span></td>\r\n                <td>";
            stack2 = helpers['if'].call(depth0, ((stack1 = depth0.info), stack1 == null || stack1 === false ? stack1 : stack1.CartImage), { hash: {}, inverse: self.noop, fn: self.program(8, program8, data), data: data });
            if (stack2 || stack2 === 0) { buffer += stack2; }
            buffer += "</td>\r\n                <td> ";
            stack2 = helpers['if'].call(depth0, ((stack1 = depth0.info), stack1 == null || stack1 === false ? stack1 : stack1.FullPrompt), { hash: {}, inverse: self.noop, fn: self.program(10, program10, data), data: data });
            if (stack2 || stack2 === 0) { buffer += stack2; }
            buffer += "</td>\r\n                <td><span class=\"syos-cart-seat-level\">";
            if (stack2 = helpers.SectionDescription) { stack2 = stack2.call(depth0, { hash: {}, data: data }); }
            else { stack2 = depth0.SectionDescription; stack2 = typeof stack2 === functionType ? stack2.apply(depth0) : stack2; }
            buffer += escapeExpression(stack2)
			  + "</span></td>\r\n                <td><span class=\"syos-cart-seat-description\">Row ";
            if (stack2 = helpers.RowText) { stack2 = stack2.call(depth0, { hash: {}, data: data }); }
            else { stack2 = depth0.RowText; stack2 = typeof stack2 === functionType ? stack2.apply(depth0) : stack2; }
            buffer += escapeExpression(stack2)
			  + ", Seat ";
            if (stack2 = helpers.NumberText) { stack2 = stack2.call(depth0, { hash: {}, data: data }); }
            else { stack2 = depth0.NumberText; stack2 = typeof stack2 === functionType ? stack2.apply(depth0) : stack2; }
            buffer += escapeExpression(stack2)
			  + "</span></td>\r\n                <td class=\"syos-cart-right\">\r\n                    <span class=\"syos-exchange-price\">";
            options = { hash: {}, data: data };
            buffer += escapeExpression(((stack1 = helpers.paraFormattedPrice), stack1 ? stack1.call(depth0, ((stack1 = depth0.pricing), stack1 == null || stack1 === false ? stack1 : stack1.Price), options) : helperMissing.call(depth0, "paraFormattedPrice", ((stack1 = depth0.pricing), stack1 == null || stack1 === false ? stack1 : stack1.Price), options)))
			  + "</span>\r\n                </td>\r\n            </tr>\r\n            ";
            return buffer;
        }
        function program8(depth0, data) {

            var buffer = "", stack1, stack2;
            buffer += "<img height=\"26\" src=\"";
            stack2 = ((stack1 = ((stack1 = depth0.info), stack1 == null || stack1 === false ? stack1 : stack1.CartImage)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1);
            if (stack2 || stack2 === 0) { buffer += stack2; }
            buffer += "\" title=\""
			  + escapeExpression(((stack1 = ((stack1 = depth0.info), stack1 == null || stack1 === false ? stack1 : stack1.Description)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "\" />";
            return buffer;
        }

        function program10(depth0, data) {


            return "<span class=\"syos-button syos-button-single\" data-bb-event=\"info\">?</span>";
        }

        function program12(depth0, data) {

            var buffer = "", stack1, stack2, options;
            buffer += "\r\n            <tr data-seat-id=\"";
            if (stack1 = helpers.SeatNumber) { stack1 = stack1.call(depth0, { hash: {}, data: data }); }
            else { stack1 = depth0.SeatNumber; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
            buffer += escapeExpression(stack1)
			  + "\">\r\n                <td><span class=\"syos-cart-remove\" data-bb-event=\"remove\" title=\"Remove this seat\">x</span></td>\r\n                <td>";
            stack2 = helpers['if'].call(depth0, ((stack1 = depth0.info), stack1 == null || stack1 === false ? stack1 : stack1.CartImage), { hash: {}, inverse: self.noop, fn: self.program(8, program8, data), data: data });
            if (stack2 || stack2 === 0) { buffer += stack2; }
            buffer += "</td>\r\n                <td> ";
            stack2 = helpers['if'].call(depth0, ((stack1 = depth0.info), stack1 == null || stack1 === false ? stack1 : stack1.FullPrompt), { hash: {}, inverse: self.noop, fn: self.program(10, program10, data), data: data });
            if (stack2 || stack2 === 0) { buffer += stack2; }
            buffer += "</td>\r\n                <td><span class=\"syos-cart-seat-level\">";
            if (stack2 = helpers.SectionDescription) { stack2 = stack2.call(depth0, { hash: {}, data: data }); }
            else { stack2 = depth0.SectionDescription; stack2 = typeof stack2 === functionType ? stack2.apply(depth0) : stack2; }
            buffer += escapeExpression(stack2)
			  + "</span></td>\r\n                <td><span class=\"syos-cart-seat-description\">Row ";
            if (stack2 = helpers.RowText) { stack2 = stack2.call(depth0, { hash: {}, data: data }); }
            else { stack2 = depth0.RowText; stack2 = typeof stack2 === functionType ? stack2.apply(depth0) : stack2; }
            buffer += escapeExpression(stack2)
			  + ", Seat ";
            if (stack2 = helpers.NumberText) { stack2 = stack2.call(depth0, { hash: {}, data: data }); }
            else { stack2 = depth0.NumberText; stack2 = typeof stack2 === functionType ? stack2.apply(depth0) : stack2; }
            buffer += escapeExpression(stack2)
			  + "</span></td>\r\n                <td class=\"syos-cart-right\">\r\n                    <span class=\"syos-price\">";
            options = { hash: {}, data: data };
            buffer += escapeExpression(((stack1 = helpers.formattedPrice), stack1 ? stack1.call(depth0, ((stack1 = depth0.pricing), stack1 == null || stack1 === false ? stack1 : stack1.Price), options) : helperMissing.call(depth0, "formattedPrice", ((stack1 = depth0.pricing), stack1 == null || stack1 === false ? stack1 : stack1.Price), options)))
			  + "</span>\r\n                </td>\r\n            </tr>\r\n            ";
            return buffer;
        }

        buffer += "<div class=\"syos-cart-header\">\r\n    ";
        stack1 = helpers['if'].call(depth0, depth0.showExpandButton, { hash: {}, inverse: self.noop, fn: self.program(1, program1, data), data: data });
        if (stack1 || stack1 === 0) { buffer += stack1; }
        buffer += "\r\n    ";
        stack1 = helpers['if'].call(depth0, depth0.isExpanded, { hash: {}, inverse: self.noop, fn: self.program(3, program3, data), data: data });
        if (stack1 || stack1 === 0) { buffer += stack1; }
        buffer += "\r\n    <span class=\"syos-button syos-fl-right\" data-bb=\"reserveButton\">Reserve</span>\r\n</div>\r\n<div class=\"syos-cart-body ";
        stack1 = helpers['if'].call(depth0, depth0.isExpanded, { hash: {}, inverse: self.noop, fn: self.program(5, program5, data), data: data });
        if (stack1 || stack1 === 0) { buffer += stack1; }
        buffer += "\">\r\n    <table class=\"syos-cart-table\">\r\n        <tbody>\r\n            ";
        stack1 = helpers.each.call(depth0, depth0.exchangeSeats, { hash: {}, inverse: self.noop, fn: self.program(7, program7, data), data: data });
        if (stack1 || stack1 === 0) { buffer += stack1; }
        buffer += "\r\n            <tr class=\"syos-exchange-divider\"><td colspan=\"6\"></td></tr>\r\n            ";
        stack1 = helpers.each.call(depth0, depth0.seats, { hash: {}, inverse: self.noop, fn: self.program(12, program12, data), data: data });
        if (stack1 || stack1 === 0) { buffer += stack1; }
        buffer += "\r\n        </tbody>\r\n    </table>\r\n</div>\r\n<div class=\"syos-cart-footer\">\r\n    <span class=\"syos-total syos-fl-right\">Total: $";
        options = { hash: {}, data: data };
        buffer += escapeExpression(((stack1 = helpers.formattedPrice), stack1 ? stack1.call(depth0, depth0.total, options) : helperMissing.call(depth0, "formattedPrice", depth0.total, options)))
		  + "</span>\r\n</div>";
        return buffer;
    });
    templates['reserveDialog.html'] = template(function (Handlebars, depth0, helpers, partials, data) {
        this.compilerInfo = [3, '>= 1.0.0-rc.4'];
        helpers = helpers || Handlebars.helpers; data = data || {};
        var buffer = "", stack1, functionType = "function", escapeExpression = this.escapeExpression, self = this;

        function program1(depth0, data) {

            var buffer = "", stack1;
            buffer += "\r\n			<div class=\"syos-reserve-confirm\">\r\n				";
            if (stack1 = helpers.confirmPrompt) { stack1 = stack1.call(depth0, { hash: {}, data: data }); }
            else { stack1 = depth0.confirmPrompt; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
            if (stack1 || stack1 === 0) { buffer += stack1; }
            buffer += "\r\n			</div>\r\n			<div class=\"syos-reserve-confirm-bottom\">\r\n				<span class=\"syos-button syos-fl-left\" data-bb=\"reserveCancel\">Cancel</span>\r\n				<span class=\"syos-button syos-fl-right\" data-bb=\"reserveConfirm\">OK</span>\r\n			</div>\r\n		";
            return buffer;
        }

        function program3(depth0, data) {

            var buffer = "", stack1, stack2;
            buffer += "\r\n			<div class=\"syos-reserve-error\">\r\n				<div class=\"syos-error-title\">"
			  + escapeExpression(((stack1 = ((stack1 = depth0.errorPrompt), stack1 == null || stack1 === false ? stack1 : stack1.title)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "</div>\r\n				<div class=\"syos-error-content\">\r\n					";
            stack2 = ((stack1 = ((stack1 = depth0.errorPrompt), stack1 == null || stack1 === false ? stack1 : stack1.content)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1);
            if (stack2 || stack2 === 0) { buffer += stack2; }
            buffer += "\r\n				</div>\r\n			</div>\r\n			<div class=\"syos-reserve-confirm-bottom\">\r\n				<span class=\"syos-button syos-fl-right\" data-bb=\"reserveCancel\">OK</span>\r\n			</div>\r\n		";
            return buffer;
        }

        function program5(depth0, data) {


            return "\r\n			<p class=\"syos-reserve-wait\">Please wait while we reserve the seats you have selected.</p>\r\n		";
        }

        buffer += escapeExpression(helpers.log.call(depth0, depth0, { hash: {}, data: data }))
		  + "\r\n<div class=\"syos-reserve-dialog\" data-bb=\"reserveDialog\">\r\n	<div class=\"syos-reserve-header\">\r\n		<span class=\"syos-reserve-header-text\">Reserve Seats</span>\r\n		<span data-bb=\"reserveAjax\" class=\"syos-reserve-header-status\"></span>\r\n	</div>\r\n	<div class=\"syos-reserve-body\">\r\n		";
        stack1 = helpers['if'].call(depth0, depth0.confirmPrompt, { hash: {}, inverse: self.noop, fn: self.program(1, program1, data), data: data });
        if (stack1 || stack1 === 0) { buffer += stack1; }
        buffer += "\r\n		";
        stack1 = helpers['if'].call(depth0, depth0.errorPrompt, { hash: {}, inverse: self.noop, fn: self.program(3, program3, data), data: data });
        if (stack1 || stack1 === 0) { buffer += stack1; }
        buffer += "\r\n		";
        stack1 = helpers['if'].call(depth0, depth0.showWait, { hash: {}, inverse: self.noop, fn: self.program(5, program5, data), data: data });
        if (stack1 || stack1 === 0) { buffer += stack1; }
        buffer += "\r\n	</div>\r\n</div>";
        return buffer;
    });
    templates['exchangeCartItem.html'] = template(function (Handlebars, depth0, helpers, partials, data) {
        this.compilerInfo = [3, '>= 1.0.0-rc.4'];
        helpers = helpers || Handlebars.helpers; data = data || {};
        var buffer = "", stack1, stack2, functionType = "function", escapeExpression = this.escapeExpression, self = this;

        function program1(depth0, data) {

            var buffer = "", stack1, stack2;
            buffer += "\r\n        <img height=\"26\" src=\"";
            stack2 = ((stack1 = ((stack1 = depth0.SeatTypeInfo), stack1 == null || stack1 === false ? stack1 : stack1.CartImage)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1);
            if (stack2 || stack2 === 0) { buffer += stack2; }
            buffer += "\" title=\""
			  + escapeExpression(((stack1 = ((stack1 = depth0.SeatTypeInfo), stack1 == null || stack1 === false ? stack1 : stack1.Description)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "\" />\r\n    ";
            return buffer;
        }

        function program3(depth0, data) {


            return "\r\n        <span class=\"syos-button syos-button-single\" data-bb-event=\"info\">?</span>\r\n    ";
        }

        buffer += "<td>\r\n    ";
        stack2 = helpers['if'].call(depth0, ((stack1 = depth0.SeatTypeInfo), stack1 == null || stack1 === false ? stack1 : stack1.CartImage), { hash: {}, inverse: self.noop, fn: self.program(1, program1, data), data: data });
        if (stack2 || stack2 === 0) { buffer += stack2; }
        buffer += "\r\n</td>\r\n<td>\r\n    ";
        stack2 = helpers['if'].call(depth0, ((stack1 = depth0.SeatTypeInfo), stack1 == null || stack1 === false ? stack1 : stack1.FullPrompt), { hash: {}, inverse: self.noop, fn: self.program(3, program3, data), data: data });
        if (stack2 || stack2 === 0) { buffer += stack2; }
        buffer += "\r\n</td>\r\n<td>\r\n    <span class=\"syos-cart-seat-level\">"
		  + escapeExpression(((stack1 = ((stack1 = depth0.attributes), stack1 == null || stack1 === false ? stack1 : stack1.SectionDescription)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
		  + "</span>\r\n</td>\r\n<td>\r\n    <span class=\"syos-cart-seat-description\">Row "
		  + escapeExpression(((stack1 = ((stack1 = depth0.attributes), stack1 == null || stack1 === false ? stack1 : stack1.RowText)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
		  + ", Seat "
		  + escapeExpression(((stack1 = ((stack1 = depth0.attributes), stack1 == null || stack1 === false ? stack1 : stack1.NumberText)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
		  + "</span>\r\n</td>\r\n<td>\r\n   	<span class=\"syos-price syos-price-discounted\">000</span>\r\n    <span class=\"syos-price\">000</span>\r\n</td>\r\n<td>\r\n    <span class=\"syos-button syos-button-single\" data-bb-event=\"remove\">x</span>\r\n</td>\r\n";
        return buffer;
    });
    templates['levelSummary.html'] = template(function (Handlebars, depth0, helpers, partials, data) {
        this.compilerInfo = [3, '>= 1.0.0-rc.4'];
        helpers = helpers || Handlebars.helpers; data = data || {};
        var stack1, functionType = "function", escapeExpression = this.escapeExpression, self = this;

        function program1(depth0, data) {

            var buffer = "", stack1, stack2;
            buffer += "\r\n    <li class=\""
			  + escapeExpression(((stack1 = depth0.levelClassName), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "\">\r\n        <div class=\"levelBox\">\r\n            <h2>"
			  + escapeExpression(((stack1 = depth0.LevelName), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "</h2>\r\n        </div>\r\n        <div class=\"levelInfo\">\r\n            <p>";
            stack2 = ((stack1 = depth0.MainBody), typeof stack1 === functionType ? stack1.apply(depth0) : stack1);
            if (stack2 || stack2 === 0) { buffer += stack2; }
            buffer += "</p>\r\n        </div>\r\n        <div class=\"levelPriceSummary\">\r\n            "
			  + escapeExpression(((stack1 = depth0.priceRangeString), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "\r\n        </div>\r\n    </li>\r\n";
            return buffer;
        }

        stack1 = helpers.each.call(depth0, depth0, { hash: {}, inverse: self.noop, fn: self.program(1, program1, data), data: data });
        if (stack1 || stack1 === 0) { return stack1; }
        else { return ''; }
    });
    templates['legend.html'] = template(function (Handlebars, depth0, helpers, partials, data) {
        this.compilerInfo = [3, '>= 1.0.0-rc.4'];
        helpers = helpers || Handlebars.helpers; data = data || {};
        var buffer = "", stack1, stack2, functionType = "function", escapeExpression = this.escapeExpression, self = this;

        function program1(depth0, data) {

            var buffer = "", stack1, stack2;
            buffer += "\r\n            ";
            stack2 = helpers['if'].call(depth0, ((stack1 = depth0.attributes), stack1 == null || stack1 === false ? stack1 : stack1.Description), { hash: {}, inverse: self.noop, fn: self.program(2, program2, data), data: data });
            if (stack2 || stack2 === 0) { buffer += stack2; }
            buffer += "\r\n        ";
            return buffer;
        }
        function program2(depth0, data) {

            var buffer = "", stack1;
            buffer += "\r\n                <li><span style=\"background-color:"
			  + escapeExpression(((stack1 = ((stack1 = depth0.attributes), stack1 == null || stack1 === false ? stack1 : stack1.Color)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + ";\"></span><strong>"
			  + escapeExpression(((stack1 = ((stack1 = depth0.attributes), stack1 == null || stack1 === false ? stack1 : stack1.Description)), typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
			  + "</strong></li>\r\n            ";
            return buffer;
        }

        buffer += "<div id=\"syoskey\" class=\"mapView\">\r\n    <ul>\r\n        ";
        stack2 = helpers.each.call(depth0, ((stack1 = depth0.model), stack1 == null || stack1 === false ? stack1 : stack1.models), { hash: {}, inverse: self.noop, fn: self.program(1, program1, data), data: data });
        if (stack2 || stack2 === 0) { buffer += stack2; }
        buffer += "\r\n    </ul>\r\n</div>";
        return buffer;
    });
})();