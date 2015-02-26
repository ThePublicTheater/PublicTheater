define(['handlebars'], function(Handlebars) {

this["JST"] = this["JST"] || {};

this["JST"]["eventPanel"] = Handlebars.template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, stack2, functionType="function", escapeExpression=this.escapeExpression, self=this;

function program1(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n                    <h3>"
    + escapeExpression(((stack1 = ((stack1 = depth0.event),stack1 == null || stack1 === false ? stack1 : stack1.name)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "</h3>\r\n                    <h5>"
    + escapeExpression(((stack1 = ((stack1 = depth0.event),stack1 == null || stack1 === false ? stack1 : stack1.location)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "</h5>\r\n                    <p>"
    + escapeExpression(((stack1 = ((stack1 = depth0.event),stack1 == null || stack1 === false ? stack1 : stack1.description)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "</p>\r\n                ";
  return buffer;
  }

function program3(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n                    <h3>Create a Facebook Event</h3>\r\n                    <br />\r\n                    <br />\r\n                    <div class=\"event-form\">\r\n                        <div class=\"event-form-field\">\r\n                            <label>Name</label>\r\n                            <input type=\"text\" data-bind=\"name\" value=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.event),stack1 == null || stack1 === false ? stack1 : stack1.name)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" />\r\n                        </div>\r\n                        <div class=\"event-form-field\">\r\n                            <label>Location</label>\r\n                            <input type=\"text\" data-bind=\"location\" value=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.event),stack1 == null || stack1 === false ? stack1 : stack1.location)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" />\r\n                        </div>\r\n                        <div class=\"event-form-field\">\r\n                            <label>Description</label>\r\n                            <textarea data-bind=\"description\">"
    + escapeExpression(((stack1 = ((stack1 = depth0.event),stack1 == null || stack1 === false ? stack1 : stack1.description)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "</textarea>\r\n                        </div>\r\n                        <div class=\"event-form-field\">\r\n                            <label>Time</label>\r\n                            <input type=\"date\" data-bind=\"start\" />\r\n                            <input type=\"date\" data-bind=\"end\" />\r\n                        </div>\r\n                        <div class=\"event-form-field\">\r\n                            <label>Privacy</label>\r\n                            <select data-bind=\"privacy\">\r\n                                <option value=\"OPEN\">Open</option>\r\n                                <option value=\"SECRET\">Secret</option>\r\n                                <option value=\"FRIENDS\">Friends</option>\r\n                            </select>\r\n                        </div>\r\n                    </div>\r\n                ";
  return buffer;
  }

function program5(depth0,data) {
  
  var buffer = "", stack1, stack2;
  buffer += "\r\n            <p class=\"lead\">Invite:</p>\r\n        		";
  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.bag),stack1 == null || stack1 === false ? stack1 : stack1.loadingFriends), {hash:{},inverse:self.noop,fn:self.program(6, program6, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n            <div data-replace=\"friendSelection\"></div>\r\n        ";
  return buffer;
  }
function program6(depth0,data) {
  
  
  return "\r\n			        <p><img src=\"/images/ajax-loader-squares.gif\" /> Loading Friends...</p>\r\n		        ";
  }

function program8(depth0,data) {
  
  
  return "\r\n            <p>To create events, you must grant this app permission to create an event on your behalf</p>\r\n            <p>\r\n                <a href=\"#\" class=\"btn\" data-action=\"authorizeApp\">Authorize</a>\r\n            </p>\r\n        ";
  }

function program10(depth0,data) {
  
  
  return "\r\n            <p class=\"pull-right\"><img src=\"/images/dot_load.gif\" /> Creating Event</p>\r\n        ";
  }

function program12(depth0,data) {
  
  var buffer = "", stack1, stack2;
  buffer += "\r\n            ";
  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.event),stack1 == null || stack1 === false ? stack1 : stack1.eventId), {hash:{},inverse:self.program(15, program15, data),fn:self.program(13, program13, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n        ";
  return buffer;
  }
function program13(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n                <a href=\"http://facebook.com/events/"
    + escapeExpression(((stack1 = ((stack1 = depth0.event),stack1 == null || stack1 === false ? stack1 : stack1.eventId)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" class=\"pull-right\" target=\"_blank\">View event on Facebook</a>\r\n            ";
  return buffer;
  }

function program15(depth0,data) {
  
  
  return "\r\n                <span class=\"btn pull-right\" data-action=\"createEvent\">Create</span>\r\n            ";
  }

  buffer += "<div class=\"row-fluid\">\r\n    <div class=\"span7\">\r\n        <div class=\"row-fluid\">\r\n            <div class=\"span10\">\r\n                ";
  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.event),stack1 == null || stack1 === false ? stack1 : stack1.onFacebook), {hash:{},inverse:self.program(3, program3, data),fn:self.program(1, program1, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"span5\">\r\n        ";
  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.context),stack1 == null || stack1 === false ? stack1 : stack1.hasEventsPermission), {hash:{},inverse:self.program(8, program8, data),fn:self.program(5, program5, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n    </div>\r\n</div>\r\n<hr />\r\n<div class=\"row-fluid\">\r\n    <div class=\"span12 clearfix\">\r\n        ";
  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.bag),stack1 == null || stack1 === false ? stack1 : stack1.creatingEvent), {hash:{},inverse:self.program(12, program12, data),fn:self.program(10, program10, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n    </div>\r\n</div>";
  return buffer;
  });

this["JST"]["eventView"] = Handlebars.template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var stack1, stack2, self=this;

function program1(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n    <div class=\"button-inner\">\r\n        ";
  stack1 = helpers['if'].call(depth0, depth0.ready, {hash:{},inverse:self.program(7, program7, data),fn:self.program(2, program2, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n    </div>\r\n";
  return buffer;
  }
function program2(depth0,data) {
  
  var buffer = "", stack1, stack2;
  buffer += "\r\n            <a href=\"#\" data-bb=\"createAction\">\r\n                ";
  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.event),stack1 == null || stack1 === false ? stack1 : stack1.onFacebook), {hash:{},inverse:self.program(5, program5, data),fn:self.program(3, program3, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n            </a>\r\n        ";
  return buffer;
  }
function program3(depth0,data) {
  
  
  return "\r\n                    View Event\r\n                ";
  }

function program5(depth0,data) {
  
  
  return "\r\n                    Create Event\r\n                ";
  }

function program7(depth0,data) {
  
  
  return "\r\n            <img src=\"/images/dot_load.gif\" />\r\n        ";
  }

  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.context),stack1 == null || stack1 === false ? stack1 : stack1.loggedIn), {hash:{},inverse:self.noop,fn:self.program(1, program1, data),data:data});
  if(stack2 || stack2 === 0) { return stack2; }
  else { return ''; }
  });

return this["JST"];

});