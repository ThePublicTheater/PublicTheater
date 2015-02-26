(function ($) {
	SYOSCart = Backbone.Model.extend({
		defaults : {
			SeatCount : 0,
			CartTotal : 0,
			HideCartHandle : true,
			HandleText : "Show more"
		},

		initialize : function () {
			var _this = this;
			_this.Seats = new Backbone.Collection();
			_this.View = new SYOSCartView({ model:_this });

			_this.on('change', function () {
				_this.calculateDerived();
				_this.View.render();
			});

			this.on('showSeatInfoAtIndex', function (SeatIndex) {
				_this.Seats.at(SeatIndex).trigger("showSeatInfo");
			});

			_this.on('removedSeatAtIndex', function (SeatIndex) {
				syosDispatch.trigger('buymode:removeSeatFromCart', _this.Seats.at(SeatIndex));
			});

			_this.on('reserveSeatsRequested', function () {
				_this.attemptReserve();
			});

			syosDispatch.on('buymode:removeSeatFromCart', function (Seat) {
				_this.Seats.remove(Seat);
				_this.trigger('change');
			});

			syosDispatch.on('buymode:addSeatToCart', function (Seat) {
				_this.Seats.add(Seat);
				_this.trigger('change');
			});

			syosDispatch.on('buymode:clearCart', function () {
				_this.Seats.reset();
				_this.trigger('change');
			});

			syosDispatch.on('buymode:removeSeatWithNumber', function(TargetSeatNumber) {
				_this.Seats.each(function (Seat) {
					if(Seat.get("SeatNumber") == TargetSeatNumber) {
						syosDispatch.trigger('buymode:removeSeatFromCart', Seat);
					}
				});	
			});
		},

		templateJSON : function () {
			var _this = this;
			var json = _this.toJSON();

			if(_this.Seats) {
				var SeatArray = new Array();
				_this.Seats.each( function (Seat) {
					SeatArray.push(Seat.templateJSON());
				});
				$.extend(json,{Seats: SeatArray});
			}

			return json;
		},

		calculateDerived : function () {
			var _this = this;

			// seat count
			_this.set('SeatCount', _this.Seats.length);

			// cart total
			var total = 0;
			if(_this.Seats) {
				_this.Seats.each(function (Seat) {
					total = total + parseInt(Seat.ActivePrice.get("Price"));
				});
			}
			_this.set("CartTotal",total);

			// show handle
			_this.set("HideCartHandle", !(_this.Seats.length > 2));

			// reserve string
			_this.set("ReserveString", _this.getReserveString())
		},

		getReserveString : function () {
			var _this = this;
			var ReserveString = '';

			_this.Seats.each(function (Seat) {
				ReserveString += Seat.get("SeatNumber") + ',' + Seat.ActivePrice.get("PriceTypeId") + ',' + Seat.get("SeatType") + ';';
			});

			return ReserveString;
		},

		isSeatInCart : function (TargetSeat) {
			var _this = this;
			var IsInCart = false;

			_this.Seats.each(function (Seat) {
				if(Seat.get("SeatNumber") == TargetSeat.get("SeatNumber"))
					IsInCart = true;
			});

			return IsInCart;
		},

		attemptReserve : function () {
			var _this = this;
			var ModalSeatType = null;

			var SeatTypesWithConfirm = [];

			_this.Seats.each(function (Seat) {
				if(Seat.SeatTypeInfo.ConfirmPrompt)
					SeatTypesWithConfirm[Seat.SeatTypeInfo.Priority] = Seat.get('SeatType');
			});

			$.each(SeatTypesWithConfirm, function () {
				if(this)
					ModalSeatType = this;
			});

			if(ModalSeatType)
				_this.openConfirmModal(ModalSeatType);
			else
				syosDispatch.trigger("buymode:reserveSeats");
		},

		openConfirmModal : function (ModalSeatType) {
			var _this = this;
			var SeatTypeInfo = HtmlSyos.BuyMode.Level.SeatTypeInfos[ModalSeatType];
			_this.ReserveModal = new SYOSModal({
				CanDismiss : false,
				Title : SeatTypeInfo.Description,
				Content : SeatTypeInfo.ConfirmPrompt,
				Actions : [
					{
						ButtonText : "I understand, Reserve Seats",
						Fn : function (Modal) {
							syosDispatch.trigger("buymode:reserveSeats");
						}
					},
					{
						ButtonText : "Remove " + SeatTypeInfo.Description + " Seats",
						Fn : function (Modal) {
							_this.removeSeatsOfType(ModalSeatType);
							Modal.View.close();
						}
					}
				]
			})
		},

		removeSeatsOfType: function (SeatTypeToRemove) {
			var _this = this;

			_this.Seats.each(function (Seat) {
				if(Seat.get("SeatType") == SeatTypeToRemove) {
					syosDispatch.trigger('buymode:removeSeatFromCart', Seat)
				}
			});
		}
	});
})(jQuery)
