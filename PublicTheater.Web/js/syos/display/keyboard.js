(function ($) {
	SYOSKeyboard = Backbone.Model.extend({
		initialize : function () {
			var _this = this;

			$(window).on('keyup', function(event) {
				_this.sendEventForKeycode(event.keyCode);
			});

			$('#syos').bind('mousewheel', function (event, d, dx, dy) {
				event.preventDefault();
				switch(dy)
				{
					case -1:
						syosDispatch.trigger('scroll:down');
						break;
					case 1:
						syosDispatch.trigger('scroll:up');
						break;
				}
			});
		},

		sendEventForKeycode : function(KeyCode) {
			var key;

			switch(KeyCode)
			{
				case 27: 
					key = 'escape';
					break;
				case 37: 
					key = 'arrowLeft';
					break;
				case 38: 
					key = 'arrowUp';
					break;
				case 39:
					key = 'arrowRight';
					break;
				case 40:
					key = 'arrowDown';
					break;
			}

			if(key)
				syosDispatch.trigger('keyboard:' + key);
		}
	});
})(jQuery);