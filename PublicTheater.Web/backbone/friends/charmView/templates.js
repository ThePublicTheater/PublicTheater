define(['handlebars'], function(Handlebars) {

this["JST"] = this["JST"] || {};

this["JST"]["charmView"] = Handlebars.template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, stack2, functionType="function", escapeExpression=this.escapeExpression, self=this;

function program1(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n    <div class=\"fb-charm-thumbs\" data-bb=\"charmCount\">\r\n        ";
  stack1 = helpers.each.call(depth0, depth0.reservations, {hash:{},inverse:self.noop,fn:self.program(2, program2, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n    </div>\r\n";
  return buffer;
  }
function program2(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n            <img src=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.friend),stack1 == null || stack1 === false ? stack1 : stack1.thumb)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" />\r\n        ";
  return buffer;
  }

  buffer += "\r\n";
  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.context),stack1 == null || stack1 === false ? stack1 : stack1.loggedIn), {hash:{},inverse:self.noop,fn:self.program(1, program1, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  return buffer;
  });

return this["JST"];

});