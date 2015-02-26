define(['handlebars'], function(Handlebars) {

this["JST"] = this["JST"] || {};

this["JST"]["publicSyosLegend"] = Handlebars.template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, functionType="function", escapeExpression=this.escapeExpression, self=this;

function program1(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n    <li>\r\n      <span style=\"background-color:"
    + escapeExpression(((stack1 = depth0.color),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + ";\"></span>\r\n      <strong>"
    + escapeExpression(((stack1 = depth0.text),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "</strong>\r\n    </li>\r\n    ";
  return buffer;
  }

  buffer += "<div class=\"syos-key mapView\">\r\n  <ul class=\"baseLegendList\">\r\n    ";
  stack1 = helpers.each.call(depth0, depth0.baseColorCollection, {hash:{},inverse:self.noop,fn:self.program(1, program1, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n  </ul>\r\n\r\n  <ul class=\"zoneLegendList\">\r\n    ";
  stack1 = helpers.each.call(depth0, depth0.zoneColorCollection, {hash:{},inverse:self.noop,fn:self.program(1, program1, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n  </ul>\r\n</div>";
  return buffer;
  });

return this["JST"];

});