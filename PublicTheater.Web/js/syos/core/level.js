(function ($) {
	SYOSLevel = Backbone.Model.extend({
		initialize : function () {
			var _this = this;
			_this.SeatTypeInfos = [];
			_this.PriceTypeCollections = {};
			_this.set("LevelName", _this.getLevelName());
		},

		getLevelName : function () {
			return $.trim($('#changeLevel h4').text());
		},

		getPriceTypeCollectionForZone : function (ZoneId) {
			return this.PriceTypeCollections[ZoneId.toString()];
		},

		loadLevel : function (LevelInfo) {
			var _this = this;
			var CurrentPriority = 0;

			$.each( LevelInfo.SeatTypes , function () {
				$.extend(this, {Priority: CurrentPriority});
				_this.SeatTypeInfos[this.ID] = this;
				CurrentPriority++;
			});

			$.each( LevelInfo.AllSeatPricing, function () {
				var ZoneId = this.Key;
				var PriceCollection = new Backbone.Collection();
				$.each(this.Value, function () {
					var Price = new SYOSPrice(this);
					PriceCollection.add(Price); 
				});
				_this.PriceTypeCollections[ZoneId.toString()] = PriceCollection;
			});
		}
	});
})(jQuery);