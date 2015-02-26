(function ($) {
	SYOSCartView = Backbone.View.extend({
		initialize : function () {
			var _this = this;
			_this.isExpanded = false;

			_this.$el = $('#cartView');
			_this.$el.hide();

			_this.$el.bind('mousewheel', function (event) {
				event.stopPropagation();
			});

			syosDispatch.on('map:levelSelectionOpened', function () {
				_this.hideCart();
			});
			syosDispatch.on('global:levelInformationLoaded', function () {
				_this.setCartDisplay();
			});

			_this.Template = Handlebars.compile( $('#cartTemplate').html() );
			_this.render();
		},
 
		events : {
			'click [data-bb-event="remove"]' : 'removeSeatClicked',
			'click [data-bb-event="height-toggle"]' : 'toggleHeight',
			'click [data-bb-event="info"]' : 'showSeatInfo',
			'click [data-bb-event="reserve"]' : 'reserveSeats'
		},

		render: function () {
			var html = this.Template(this.options.model.templateJSON());
			this.$el.html(html);
			this.setCartDisplay();
			this.setCartHeight();
		},

		toggleHeight : function () {
			this.isExpanded = !this.isExpanded;
			var HandleText;
			this.isExpanded ? HandleText = "Show less" : HandleText = "Show more";
			this.options.model.set("HandleText",HandleText);
			this.setCartHeight();
		},

		hideCart : function () {
			this.$el.fadeOut();
		},

		showCart : function () {
			this.$el.fadeIn();
		},

		setCartHeight : function () {
			var RowHeight = 0;
			var CartTable = this.$el.find("#cartTable");
			var SeatCount = this.options.model.get("SeatCount");
			var viewCartBtn = this.$el.find('#seatCartHandle');

		    if(CartTable.find('tr').length != 0)
		        RowHeight = CartTable.find('tr').outerHeight(true) + 1;

		    var expandedHeight = RowHeight*SeatCount;
		    var condensedHeight = RowHeight * 2;

		    if(expandedHeight > 400)
		        expandedHeight = 400;

		    var css;

		    if(this.isExpanded) {
		        css = {maxHeight:expandedHeight + 'px'};
		        viewCartBtn.html('Show less');
		    }
		    else {
		        css = {maxHeight:condensedHeight + 'px'};
		        viewCartBtn.html('Show all&nbsp;');
		    }

		    CartTable.css(css);
		},

		setCartDisplay : function () {
			var SeatCount = this.options.model.get("SeatCount");

			SeatCount == 0 ? this.hideCart(): this.showCart();
		},

		showSeatInfo : function (event) {
			var targetIndex = $(event.currentTarget).parents('tr').index();
			this.options.model.trigger('showSeatInfoAtIndex', targetIndex);
		},

		removeSeatClicked : function (event) {
			var targetIndex = $(event.currentTarget).parents('tr').index();
			this.options.model.trigger('removedSeatAtIndex', targetIndex);
		},

		reserveSeats : function () {
			this.options.model.trigger('reserveSeatsRequested');
		}
	});
})(jQuery)