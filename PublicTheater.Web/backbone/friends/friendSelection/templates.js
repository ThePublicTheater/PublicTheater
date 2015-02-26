define(['handlebars'], function(Handlebars) {

this["JST"] = this["JST"] || {};

this["JST"]["friendSelection"] = Handlebars.template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, functionType="function", escapeExpression=this.escapeExpression, self=this;

function program1(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n            <li data-id=\""
    + escapeExpression(((stack1 = depth0.id),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" class=\"friend\" title=\"Click to add "
    + escapeExpression(((stack1 = depth0.name),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\">\r\n              <img src=\"http://graph.facebook.com/"
    + escapeExpression(((stack1 = depth0.id),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "/picture\" alt=\""
    + escapeExpression(((stack1 = depth0.name),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\"/>\r\n              "
    + escapeExpression(((stack1 = depth0.name),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\r\n            </li>\r\n        ";
  return buffer;
  }

  buffer += "\r\n<div class=\"fb-friend-list\">\r\n    <div data-bb=\"selectedList\" class=\"fb-selected-friends row-fluid\">\r\n    </div>\r\n    <ul data-bb=\"availableList\" class=\"fb-available-friends\">\r\n        ";
  stack1 = helpers.each.call(depth0, depth0.friends, {hash:{},inverse:self.noop,fn:self.program(1, program1, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n    </ul>\r\n</div>";
  return buffer;
  });

this["JST"]["selectedList"] = Handlebars.template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var stack1, functionType="function", escapeExpression=this.escapeExpression, self=this;

function program1(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n    <div class=\"span6 friend\" data-id=\"";
  if (stack1 = helpers.id) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.id; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "\">\r\n        <i class=\"icon-remove\"></i> ";
  if (stack1 = helpers.name) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.name; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "\r\n    </div>\r\n";
  return buffer;
  }

  stack1 = helpers.each.call(depth0, depth0.friends, {hash:{},inverse:self.noop,fn:self.program(1, program1, data),data:data});
  if(stack1 || stack1 === 0) { return stack1; }
  else { return ''; }
  });

return this["JST"];

});