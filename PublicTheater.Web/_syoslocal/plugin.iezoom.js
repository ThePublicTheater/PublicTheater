(function ($) {
	syosDispatch.on("didFinishInit", function (Core) {
		Core.plugins["iezoom"] = new SYOSIEZoom();
	});

	SYOSIEZoom = Backbone.View.extend({
		id : "syos-ie-zoom",
		initialize : function () {
			var _this = this;
			this.$el.appendTo($("#syos").parent());
			this.template = _.template($("#ie-zoom-template").html());
			this.model = new Backbone.Model();
			this.checkZoom();
			setInterval(function () {
				_this.checkZoom();
			}, 500);
			this.render();
		},

		checkZoom : function () {
			this.model.set("zoom", (screen.deviceXDPI / screen.logicalXDPI));
			this.render();
		},

		events : {
			"click [data-zoom-refresh]" : "refresh"
		},

		refresh : function () {
			window.location.reload();
		},

		render : function () {
			if(this.model) {
				if(this.model.get("zoom") === 1) {
					$("#syos").show();
				}
				else {
					$("#syos").hide();
					this.$el.html(this.template());
				}
				
			}
		}
	});
})(jQuery)