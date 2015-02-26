define(['handlebars'], function(Handlebars) {

this["JST"] = this["JST"] || {};

this["JST"]["reservationList"] = Handlebars.template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, functionType="function", escapeExpression=this.escapeExpression, self=this;

function program1(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n        <li class=\"friend\">\r\n            <div class=\"clearfix\">\r\n                <div class=\"pull-left thumb-wrap\">\r\n                    <img src=\"http://graph.facebook.com/";
  if (stack1 = helpers.friendId) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.friendId; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "/picture\" alt=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.friend),stack1 == null || stack1 === false ? stack1 : stack1.name)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\"/>\r\n                </div>\r\n                <div class=\"pull-left\">\r\n                    <p class=\"row-title\">"
    + escapeExpression(((stack1 = ((stack1 = depth0.friend),stack1 == null || stack1 === false ? stack1 : stack1.name)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "</p>\r\n                    <p class=\"row-subtitle\">"
    + escapeExpression(((stack1 = ((stack1 = depth0.seats),stack1 == null || stack1 === false ? stack1 : stack1.length)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + " seats</p>\r\n                </div>\r\n            </div>\r\n        </li>\r\n    ";
  return buffer;
  }

  buffer += "<ul>\r\n    ";
  stack1 = helpers.each.call(depth0, depth0.reservations, {hash:{},inverse:self.noop,fn:self.program(1, program1, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n</ul>";
  return buffer;
  });

return this["JST"];

});