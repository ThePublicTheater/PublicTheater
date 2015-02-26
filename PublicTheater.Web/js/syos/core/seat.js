(function ($) {
	SYOSSeat = Backbone.Model.extend({
		initialize : function () {
			var _this = this;
			_this.PriceTypes = new Backbone.Collection();
			_this.SeatTypeInfo = HtmlSyos.BuyMode.Level.SeatTypeInfos[_this.get("SeatType")];
			_this.set("LevelName", HtmlSyos.BuyMode.Level.get("LevelName"));

			_this.on('seatPricingRecieved', function (PricingInfo) {
				$.each(PricingInfo, function () {
					_this.Prices.add(new SYOSPrice(this));
				});
				_this.trigger('seatPricingUpdated');
			});

			_this.on('setActivePrice', function (PriceTypeIndex) {
				_this.ActivePrice = _this.Prices.at(PriceTypeIndex);
			});

			_this.on('showSeatInfo', function () {
				_this.SeatInfoView = new SYOSModal({
					Title : _this.SeatTypeInfo.Description,
					Content : _this.SeatTypeInfo.FullPrompt
				});
				_this.SeatInfoView.on('modalViewDidClose', function () {
					delete _this.SeatInfoView;
				});
			});
		},

		loadPriceInfo : function (PricingInfo) {
			var _this = this;
			$.each(PricingInfo, function () {
				_this.Prices.add(new SYOSPrice(this));
			});
			_this.trigger('seatPricingUpdated');
		},

		setLevel : function (Level) {
			var _this = this;
			_this.Level = Level;
			_this.PriceTypes = Level.getPriceTypeCollectionForZone(_this.get("ZoneId"));
		},

		templateJSON : function () {
			var _this = this;
			var json = _this.toJSON();

			if(_this.PriceTypes)
				$.extend(json, {PriceTypes: _this.PriceTypes.toJSON()});
			if(_this.ActivePrice)
			 	$.extend(json, {ActivePrice: _this.ActivePrice.toJSON()});
			if(_this.SeatTypeInfo)
				$.extend(json, {SeatTypeInfo: _this.SeatTypeInfo});

			return json;
		}
	});
})(jQuery)