define(['handlebars'], function(Handlebars) {

this["JST"] = this["JST"] || {};

this["JST"]["modal"] = Handlebars.template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  


  return "<div data-bb=\"modalInner\" class=\"fb-modal-inner\">\r\n    <div class=\"clearfix\">\r\n        <div class=\"pull-right\">\r\n            <a href=\"#\" class=\"btn\" data-bb=\"closeModal\">&times;</a>\r\n        </div>\r\n    </div>\r\n    <div data-replace=\"modalContent\"></div>\r\n</div>";
  });

this["JST"]["popover"] = Handlebars.template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  


  return "<div class=\"arrow\"></div>\r\n<div data-bb=\"popoverContent\" class=\"fb-popover-content\">\r\n</div>";
  });

return this["JST"];

});