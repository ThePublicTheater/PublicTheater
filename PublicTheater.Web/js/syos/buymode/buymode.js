(function ($) {
	SYOSBuyMode = Backbone.Model.extend({
		initialize : function () {
			var _this = this;

			_this.Performance = new SYOSPerformance({
				PerformanceNumber: HtmlSyos.Utility.getUrlVar('performanceNumber')
			});

			_this.Cart = new SYOSCart();
			_this.SeatPopup = new SYOSSeatPopup();

			syosDispatch.on('global:levelInformationLoaded', function (LevelInfo) {
				_this.Level = new SYOSLevel();
				_this.Level.loadLevel(LevelInfo);

				_this.ActiveSeat;
				syosDispatch.trigger('buymode:ensureMapAccuracy', _this);
			});

			syosDispatch.on('map:seatSelected', function (SeatCircle) {
				// if seatpopup is open, close it properly
				if (_this.SeatPopup.ActiveSeat)
					_this.SeatPopup.closePopup();
				
				var Cleaned = HtmlSyos.Utility.cleanObject(SeatCircle.attrs.TessituraInfo);
				var SeatData = $.extend({}, Cleaned, {SVG: SeatCircle});
				_this.ActiveSeat = new SYOSSeat(SeatData);
				_this.ActiveSeat.setLevel(_this.Level);
				_this.SeatPopup.trigger("showSeat", _this.ActiveSeat);
			});

			syosDispatch.on("buymode:reserveSeats", function () {
				var ReserveString = _this.Cart.get("ReserveString");
				Adage.HtmlSyos.Code.SYOSService.ReserveSeats(ReserveString, _this.Performance.get("PerformanceNumber"), syos.ReserveSeatResults, syos.ReserveSeatError, null);
			});

		}
	});
})(jQuery)
