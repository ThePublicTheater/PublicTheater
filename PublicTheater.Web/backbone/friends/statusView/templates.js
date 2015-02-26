define(['handlebars'], function(Handlebars) {

this["JST"] = this["JST"] || {};

this["JST"]["statusView"] = Handlebars.template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, stack2, self=this;

function program1(depth0,data) {
  
  
  return "\r\n    <span>Experience enhanced with Facebook integration. <a href=\"#\" data-bb=\"logoutButton\">logout</a></span>\r\n";
  }

function program3(depth0,data) {
  
  
  return "\r\n    <span><a href=\"#\" data-bb=\"loginButton\">Login to Facebook</a> to enhance & share your experience</span>\r\n";
  }

  buffer += "\r\n";
  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.context),stack1 == null || stack1 === false ? stack1 : stack1.loggedIn), {hash:{},inverse:self.program(3, program3, data),fn:self.program(1, program1, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  return buffer;
  });

return this["JST"];

});