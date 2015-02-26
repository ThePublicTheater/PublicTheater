(function ($) {
	SYOSSeatPopup = Backbone.View.extend({
		initialize : function () {
			var _this = this;
			_this.$el = $('#seatPopupView');
			_this.Template = Handlebars.compile($('#popupTemplate2').html());
			_this.ActiveSeat;

			_this.on('showSeat', function (Seat) {
				_this.ActiveSeat = Seat;
				_this.render();
			});

			syosDispatch.on('keyboard:escape', function () {
				_this.closePopup(true);
			});

			syosDispatch.on('control:zoom', function (ShouldZoomIn) {
				_this.closePopup(false);
			});
		},

		events : {
			"click .popup-addToCart" : "addButtonClicked",
			"click .popup-closeButton" : "closePopup"
		},

		render : function () {
			var _this = this;
			var html = this.Template(this.ActiveSeat.templateJSON());
			this.$el.html(html);

			var SeatSVG = this.ActiveSeat.get("SVG");
			var SeatPosition = {
				top : parseInt(SeatSVG.attr('cy')) + 10,
				left : parseInt(SeatSVG.attr('cx')) + 10
			}
			var PopupPosition = {
				top		: SeatPosition.top * zoomLevel,
				left	: SeatPosition.left * zoomLevel
			}

			this.$el.css({
				'top' : PopupPosition.top,
				'left' : PopupPosition.left
			});

			if(_this.ActiveSeat.PriceTypes.length === 1)
				_this.$el.find('.popup-addToCart').click();
			else
				this.openPopup();
		},

		openPopup : function () {
			this.$el.show();
			this.bringIntoView();
		},

		closePopup : function (Animated) {
			var _this = this;

			if(Animated === "undefined")
				Animated = true;

			syosDispatch.trigger('buymode:seatInfoClosed', _this.ActiveSeat);

			if(Animated) {
				_this.$el.fadeOut( function () {
					delete _this.ActiveSeat;
				});
			}
			else {
				this.$el.hide();
				delete _this.ActiveSeat;
			}
		},

		bringIntoView : function () {
			/* legacy code */
		    var bufferSpace = 30;

		    var seatPopupW = this.$el.outerWidth();
		    var seatPopupH = this.$el.outerHeight();

		    var syosCanvasW = syosCanvas.width();
		    var syosCanvasH = syosCanvas.height();

		    var seatPopupTop = parseInt(this.$el.css('top').replace('px', ''));
		    var seatPopupLeft = parseInt(this.$el.css('left').replace('px', ''));

		    var levelsWrapTop = 0;
		    var levelsWrapLeft = levelsWrap.position().left + parseInt(levelsWrap.css('marginLeft').replace('px', ''));

		    var seatsWrapTop = seatsWrap.position().top;
		    var seatsWrapLeft = parseInt(seatsWrap.css('left').replace('px', ''));

		    var popupBottomEdgePos = seatPopupTop + levelsWrapTop + seatsWrapTop + seatPopupH;
		    var popupRightEdgePos = seatPopupLeft + levelsWrapLeft + seatsWrapLeft + seatPopupW;

		    var pxOffTop = (popupBottomEdgePos > syosCanvasH) ? popupBottomEdgePos - syosCanvasH + bufferSpace : 0;
		    var pxOffRight = (popupRightEdgePos > syosCanvasW) ? popupRightEdgePos - syosCanvasW + bufferSpace : 0;

		    seatsWrap.stop().animate({ left: '-=' + pxOffRight, top: '-=' + pxOffTop }, 350, 'easeInOutCubic');
		},

		addButtonClicked : function (event) {
			var _this = this;

			var ClickedButton = $(event.currentTarget);
			var PriceTypeIndex = ClickedButton.parents('tr').index();
			_this.ActiveSeat.trigger('setActivePrice', PriceTypeIndex);

			window.syosDispatch.trigger('buymode:addSeatToCart', _this.ActiveSeat);

			_this.closePopup();
		}

	});
})(jQuery)