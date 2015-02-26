(function ($) {
	SYOSUtility = Backbone.Model.extend({
		cleanObject : function (Dirty) {
			var Clean = {};

			for (var key in Dirty) {
				var DirtyVal = Dirty[key];
				var CleanVal = DirtyVal;

				if(!isNaN(parseInt(DirtyVal)))
					CleanVal = parseInt(DirtyVal);

				Clean[key] = CleanVal;
			}

			return Clean;
		},

		getUrlVars: function () {
	        var vars = [], hash;
	        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).replace("#", "").split('&');
	        for (var i = 0; i < hashes.length; i++) {
	            hash = hashes[i].split('=');
	            vars.push(hash[0]);
	            vars[hash[0]] = hash[1];
	        }
	        return vars;
	    },

	    getUrlVar: function (name) {
	        var rVal = this.getUrlVars()[name];
	        if (typeof rVal != 'undefined')
	            return rVal;
	        else
	            return '';
	    },

	    isInArray: function (arry, intVal) {
	        var currentVal = intVal;
	        if ($.is_int(currentVal) == false)
	            currentVal = parseInt(currentVal);

	        if (arry.indexOf)
	            return arry.indexOf(currentVal) > -1;

	        for (arryIdx = 0; arryIdx < arry.length; arryIdx++) {
	            if (arry[arryIdx] == currentVal)
	                return true;
	        }
	        return false;
	    },

	    is_int: function (input) {
	        return typeof (input) == 'number' && parseInt(input) == input;
	    },
	    
	    getDOMVar: function (selector) {
	    	return $(selector).val();
	    }
	});
})(jQuery);