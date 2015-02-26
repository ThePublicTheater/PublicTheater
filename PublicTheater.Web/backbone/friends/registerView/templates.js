define(['handlebars'], function(Handlebars) {

this["JST"] = this["JST"] || {};

this["JST"]["registerView"] = Handlebars.template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  


  return "\r\n<p><a href=\"#\" class=\"btn\" data-action=\"register\">Login to Facebook</a></p>";
  });

return this["JST"];

});