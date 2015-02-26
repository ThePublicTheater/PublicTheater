define(['handlebars'], function(Handlebars) {

this["JST"] = this["JST"] || {};

this["JST"]["reservationPanel"] = Handlebars.template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, stack2, functionType="function", escapeExpression=this.escapeExpression, self=this;

function program1(depth0,data) {
  
  
  return "\r\n			<p><img src=\"/images/ajax-loader-squares.gif\" /> Loading Friends...</p>\r\n		";
  }

  buffer += "<div class=\"row-fluid\">\r\n    <div class=\"span6\">\r\n        <div class=\"row-fluid\">\r\n            <div class=\"span8\">\r\n                <h3>"
    + escapeExpression(((stack1 = ((stack1 = depth0.performance),stack1 == null || stack1 === false ? stack1 : stack1.title)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "</h3>\r\n                <p>"
    + escapeExpression(((stack1 = ((stack1 = depth0.performance),stack1 == null || stack1 === false ? stack1 : stack1.performanceDate)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"span6\">\r\n        <p class=\"lead\">Share my reservation with...</p>\r\n		";
  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.bag),stack1 == null || stack1 === false ? stack1 : stack1.loadingFriends), {hash:{},inverse:self.noop,fn:self.program(1, program1, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n        <div data-replace=\"friendSelection\"></div>\r\n    </div>\r\n</div>\r\n<hr />\r\n<div class=\"row-fluid\">\r\n    <div class=\"span12 clearfix\">\r\n        <a href=\"#\" class=\"btn pull-right\" data-action=\"save\">\r\n            <i class=\"icon-save\"></i>\r\n            Save\r\n        </a>\r\n        <a href=\"#\" class=\"btn btn-warning disabled pull-right\" data-bb=\"saving\">\r\n            <i class=\"icon-reload\"></i>\r\n            Saving...\r\n        </a>\r\n        <a href=\"#\" class=\"btn btn-success disabled pull-right\" data-bb=\"saved\">\r\n            <i class=\"icon-ok\"></i>\r\n            Saved\r\n        </a>\r\n    </div>\r\n</div>";
  return buffer;
  });

this["JST"]["reservationView"] = Handlebars.template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var stack1, stack2, self=this;

function program1(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n    <div class=\"button-inner\">\r\n        ";
  stack1 = helpers['if'].call(depth0, depth0.ready, {hash:{},inverse:self.program(4, program4, data),fn:self.program(2, program2, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n    </div>\r\n";
  return buffer;
  }
function program2(depth0,data) {
  
  
  return "\r\n            <a data-bb=\"shareAction\" href=\"#\">Share Reservation</a>\r\n        ";
  }

function program4(depth0,data) {
  
  
  return "\r\n            <img src=\"/images/dot_load.gif\" />\r\n        ";
  }

  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.context),stack1 == null || stack1 === false ? stack1 : stack1.loggedIn), {hash:{},inverse:self.noop,fn:self.program(1, program1, data),data:data});
  if(stack2 || stack2 === 0) { return stack2; }
  else { return ''; }
  });

return this["JST"];

});