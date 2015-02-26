(function ($) {
	SYOSLegacyMap = Backbone.Model.extend({
		initialize : function () {
			var _this = this;
			_this.View = new SYOSLegacyMapView({model: _this});

			_this.overwriteLegacy();

			syosDispatch.on('map:seatSelected', function (SeatSVG) {
				SeatSVG.attr({IsSelected: true});
				syos.highlightSeat.call(SeatSVG);
			});

			syosDispatch.on('buymode:seatInfoClosed', function(Seat) {
				if(Seat) {
					var IsInCart = HtmlSyos.BuyMode.Cart.isSeatInCart(Seat);
					if(!IsInCart) {
						Seat.get("SVG").attr("IsSelected", false);
						syos.unHighlightSeat.call(Seat.get("SVG"));					
					}
				}
			});

			syosDispatch.on('buymode:ensureMapAccuracy', function(BuyMode) {
				BuyMode.Cart.Seats.each(function (Seat) {
					var SeatSVG = Seat.get("SVG");
					SeatSVG.attr("IsSelected", true);
					syos.highlightSeat.call(SeatSVG);
				});
			});

			syosDispatch.on('buymode:addSeatToCart', function (Seat) {
				syos.highlightSeat.call(Seat.get("SVG"));
			});

			syosDispatch.on('buymode:removeSeatFromCart', function (Seat) {
				var SeatSVG = Seat.get("SVG");
				SeatSVG.attr('IsSelected', false);
				syos.unHighlightSeat.call(SeatSVG);
			});

		},

		seatSelected : function () {
			if(this.attr('IsSelected')) {
				var SeatNumber = this.attr('TessituraInfo').SeatNumber;
				syosDispatch.trigger('buymode:removeSeatWithNumber', SeatNumber);
			}
			else {
				syosDispatch.trigger('map:seatSelected', this);
				window.selectedSeat = this;
			}
		},

		overwriteLegacy : function () {
			syos.GetSeats = function (rootConfigId, levelNum, zoneIDs, configPageID, isAdminMode) {
		        return Adage.HtmlSyos.Code.SYOSService.GetLevelInformation(rootConfigId, performanceNumber, zoneIDs, configPageID, isAdminMode, isAdminMode,
			        function onSuccess(msg) {
			            syos.buildSeatMap(msg, levelNum, configPageID);
			        },
			        function onError(msg) {
			            alert('An error occurred getting seats from the server.');
			        }
		        );
		    };

	        syos.showSeatInfo = function () {
		        if (typeof selectedSeat != 'undefined' && selectedSeat != null) {
		            selectedSeat.attr({ fill: tessituraInfo.SeatColors.Main.Color, opacity: tessituraInfo.SeatColors.Main.Opacity });
		            selectedSeat.attr('IsSelected', false);
		            selectedSeat = null;
		        }
		        if (this.attr('IsSelected') == false) {
		            availableSeats.attr({ fill: tessituraInfo.SeatColors.Main.Color, opacity: tessituraInfo.SeatColors.Main.Opacity });
		            availableSeats.attr('IsSelected', false);
		            this.attr({ fill: tessituraInfo.SeatColors.Highlight.Color, opacity: tessituraInfo.SeatColors.Highlight.Opacity, r: seatRadius });
		            this.attr('IsSelected', true);
		            selectedSeat = this;
		            seatsWrap.addClass('viewingSeat');

		            // syos.GetSeats(this);
		        }
		    };

		    syos.buildSeatMap = function (msg, levelNum, configPageID) {
		    	syosDispatch.trigger('global:levelInformationLoaded', msg);

		        rId = 'level' + levelNum;

		        if (isAdminMode == "False") {
		            selectedBackgroundImageUrl = "/SYOSService/SyosBackgroundImage.ashx?performanceNumber=" + performanceNumber + "&seatRadius=" + seatRadius + "&pageid=" + configPageID + "&hideSeatOutlines=" + isAdminMode;
		        }

		        // create temporary seatmap image to calculate height/width. it gets removed at the end of this method
		        var newImage = syosWrap.prepend('<img id="seatMap" src="' + selectedBackgroundImageUrl + '" alt="" style="position:absolute;left:-9999em;" />');

		        $('#seatMap').load(function () {
		            imgHeight = $(this).height();
		            imgWidth = $(this).width();
		            canvasH = imgHeight * 2;
		            canvasW = imgWidth * 2;

		            if (r[rId] == undefined) {
		                r[rId] = Raphael('level' + levelNum, canvasW, canvasH);
		                r[rId].customAttributes.IsSelected = function (val) { return val; };
		                r[rId].customAttributes.Number = function (val) { return val; };
		                r[rId].customAttributes.ID = function (val) { return val; };
		                r[rId].customAttributes.ImageUrl = function (val) { return val; };
		                r[rId].customAttributes.IsAvailable = function (val) { return val; };
		                r[rId].customAttributes.TessituraInfo = function (val) { return val; };

		                seatmap = r[rId].image(selectedBackgroundImageUrl, 0, 0, imgWidth, imgHeight);

		                var selectedSeat = r[rId].set();
		                unavailableSeats = r[rId].set();
		                availableSeats = r[rId].set();
		                allLevelSeats[rId] = r[rId].set();

		                if (typeof cartSeats == 'undefined')
		                    cartSeats = r[rId].set();

		                tessituraInfo = msg;
		                var seatsAvailable = tessituraInfo.SeatLists[0];

		                $.each(seatsAvailable, function () {
		                	var circleFill = tessituraInfo.SeatColors.Main.Color;
		                	var circleImage;

		                	if(HtmlSyos.BuyMode.Level.SeatTypeInfos[this.SeatType].Overlay) {
		                		circleImage = HtmlSyos.BuyMode.Level.SeatTypeInfos[this.SeatType].Overlay;
		                		circleFill = 'url(' + circleImage + ')';
		                	}


	                        var seatCircle = r[rId].circle(this.Circle.X, this.Circle.Y, seatRadius).attr({
	                            ID: this.Circle.ID,
	                            fill: circleFill,
	                            opacity: tessituraInfo.SeatColors.Main.Opacity,
	                            stroke: 0,
	                            cursor: 'pointer',
	                            IsAvailable: true,
	                            IsSelected: false,
	                            TessituraInfo: this
	                        });
	                        availableSeats.push(seatCircle);
		                });

		                var seatsUnavailable = tessituraInfo.SeatLists[1];

		                if ((typeof seatsUnavailable === "undefined") == false) {
		                    $.each(seatsUnavailable, function () {
		                        var seatCircle = r[rId].circle(this.Circle.X, this.Circle.Y, seatRadius).attr({
		                            ID: this.Circle.ID,
		                            fill: tessituraInfo.SeatColors.Unavailable.Color,
		                            opacity: tessituraInfo.SeatColors.Unavailable.Opacity,
		                            stroke: 0,
		                            IsAvailable: false,
		                            IsSelected: false,
		                            TessituraInfo: this
		                        });
		                        unavailableSeats.push(seatCircle);
		                        unavailableSeatsReferences[this.SeatNumber.toString()] = seatCircle;
		                    });
		                }

		                // push all level seats into 1 set for easy showing/hiding
		                allLevelSeats[rId].push(availableSeats, unavailableSeats);

		                if(typeof(window.unavailableSeatsReady) === "function")
		                    window.unavailableSeatsReady.call();

		                syos.LoadSeatViews(tessituraInfo.SeatViews);
		            } 
		            else {
		                syos.showMap(r[rId], imgHeight, imgWidth);
		            }

		            availableSeats.click(HtmlSyos.LegacyMap.seatSelected).mouseover(syos.highlightSeat).mouseout(syos.unHighlightSeat);

		            // availableSeats.click(HtmlSyos.LegacyMap.seatSelected);

		            $(this).remove();
		        });
		    };

	        syos.highlightSeat = function () {
		        // if (!this.attr('IsSelected')) {
	                // this.attr({ fill: tessituraInfo.SeatColors.ZoneHighlight.Color, opacity: tessituraInfo.SeatColors.ZoneHighlight.Opacity });
	                // this.attr({opacity: tessituraInfo.SeatColors.ZoneHighlight.Opacity });
		            this.attr({stroke:tessituraInfo.SeatColors.ZoneHighlight.Color, "stroke-width": 3});
		        // }
		    };

		    syos.unHighlightSeat = function () {
		        if (!this.attr('IsSelected')) {
	                // this.attr({ fill: tessituraInfo.SeatColors.Main.Color, opacity: tessituraInfo.SeatColors.Main.Opacity });
	                // this.attr({opacity: tessituraInfo.SeatColors.Main.Opacity });
		            this.attr({stroke:0});
		        }
		    };

	        syos.seatRemovedFromCart = function (index) {
		        if (cartSeats.length < 1)
		            return;

		        // cartSeats[index].attr({ fill: tessituraInfo.SeatColors.Main.Color, opacity: tessituraInfo.SeatColors.Main.Opacity, stroke: 0, cursor: 'pointer' });
		        cartSeats[index].attr('IsSelected', false);
		        availableSeats.push(cartSeats[index]);
		        cartSeats.exclude(cartSeats[index]);
		        if (!$('#cartTable tbody td').length) {
		            viewCartBtn.click();
		        }
		    };

	        syos.showHouse = function () {
		        mapViewWidgets.hide();
		        syos.resetHouseView();
		        $('.level:visible').fadeOut(dur * 2);
		        chooseLevel.fadeIn(dur * 2);
		        seatPopup.hide();
				syosDispatch.trigger('map:levelSelectionOpened');

		        return false;
		    };

		    window.syosRemoveAllSeats = function () {
		    	syosDispatch.trigger('buymode:clearCart');
			};
		}
	});
})(jQuery);