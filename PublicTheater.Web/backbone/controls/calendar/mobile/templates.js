define(['handlebars'], function(Handlebars) {

this["JST"] = this["JST"] || {};

this["JST"]["mobileCalendar"] = Handlebars.template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, stack2, functionType="function", escapeExpression=this.escapeExpression, self=this, helperMissing=helpers.helperMissing;

function program1(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n    <select data-bb=\"mobileVenueFilterSelect\">\r\n      ";
  stack1 = helpers.each.call(depth0, (depth0 && depth0.venueFilters), {hash:{},inverse:self.noop,fn:self.program(2, program2, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n    </select>\r\n    ";
  return buffer;
  }
function program2(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n      <option value=\"";
  if (stack1 = helpers.Venue) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = (depth0 && depth0.Venue); stack1 = typeof stack1 === functionType ? stack1.call(depth0, {hash:{},data:data}) : stack1; }
  buffer += escapeExpression(stack1)
    + "\" ";
  stack1 = helpers['if'].call(depth0, (depth0 && depth0.IsActive), {hash:{},inverse:self.noop,fn:self.program(3, program3, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += ">\r\n        ";
  if (stack1 = helpers.VenueText) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = (depth0 && depth0.VenueText); stack1 = typeof stack1 === functionType ? stack1.call(depth0, {hash:{},data:data}) : stack1; }
  buffer += escapeExpression(stack1)
    + "\r\n      </option>\r\n      ";
  return buffer;
  }
function program3(depth0,data) {
  
  
  return "selected=\"selected\"";
  }

function program5(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n  ";
  stack1 = helpers.each.call(depth0, (depth0 && depth0.displayModels), {hash:{},inverse:self.noop,fn:self.program(6, program6, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n";
  return buffer;
  }
function program6(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n    ";
  stack1 = helpers['if'].call(depth0, (depth0 && depth0.ShowVenue), {hash:{},inverse:self.noop,fn:self.program(7, program7, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n  ";
  return buffer;
  }
function program7(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n    <ul class=\"mobileCalList\">\r\n      <li class=\"venueBar\">";
  if (stack1 = helpers.Venue) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = (depth0 && depth0.Venue); stack1 = typeof stack1 === functionType ? stack1.call(depth0, {hash:{},data:data}) : stack1; }
  buffer += escapeExpression(stack1)
    + "</li>\r\n      ";
  stack1 = helpers.each.call(depth0, (depth0 && depth0.Performances), {hash:{},inverse:self.noop,fn:self.program(8, program8, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n    </ul>\r\n    ";
  return buffer;
  }
function program8(depth0,data) {
  
  var buffer = "", stack1, stack2, options;
  buffer += "\r\n      <li class=\"performanceLink\">\r\n        <a href=\"";
  if (stack1 = helpers.ReserveURL) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = (depth0 && depth0.ReserveURL); stack1 = typeof stack1 === functionType ? stack1.call(depth0, {hash:{},data:data}) : stack1; }
  buffer += escapeExpression(stack1)
    + "\">\r\n          ";
  options = {hash:{},inverse:self.noop,fn:self.program(9, program9, data),data:data};
  stack2 = ((stack1 = helpers.ifNotEqual || (depth0 && depth0.ifNotEqual)),stack1 ? stack1.call(depth0, (depth0 && depth0.ImageURL), (depth0 && depth0.undefined), options) : helperMissing.call(depth0, "ifNotEqual", (depth0 && depth0.ImageURL), (depth0 && depth0.undefined), options));
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n          \r\n          <div class=\"showInfo\">\r\n            <span>";
  if (stack2 = helpers.Name) { stack2 = stack2.call(depth0, {hash:{},data:data}); }
  else { stack2 = (depth0 && depth0.Name); stack2 = typeof stack2 === functionType ? stack2.call(depth0, {hash:{},data:data}) : stack2; }
  buffer += escapeExpression(stack2)
    + "</span>\r\n            <span>";
  if (stack2 = helpers.Time) { stack2 = stack2.call(depth0, {hash:{},data:data}); }
  else { stack2 = (depth0 && depth0.Time); stack2 = typeof stack2 === functionType ? stack2.call(depth0, {hash:{},data:data}) : stack2; }
  buffer += escapeExpression(stack2)
    + "</span>\r\n          </div>\r\n          \r\n        </a>\r\n      </li>\r\n      ";
  return buffer;
  }
function program9(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n            <img src=\"";
  if (stack1 = helpers.ImageURL) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = (depth0 && depth0.ImageURL); stack1 = typeof stack1 === functionType ? stack1.call(depth0, {hash:{},data:data}) : stack1; }
  buffer += escapeExpression(stack1)
    + "\" />\r\n          ";
  return buffer;
  }

function program11(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n  <span class=\"noCalendarItemsMessage\">There are no events for ";
  if (stack1 = helpers.currentDate) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = (depth0 && depth0.currentDate); stack1 = typeof stack1 === functionType ? stack1.call(depth0, {hash:{},data:data}) : stack1; }
  buffer += escapeExpression(stack1)
    + ".</span>\r\n";
  return buffer;
  }

  buffer += "<div class=\"filterPromoArea\">\r\n  \r\n  <div class=\"mobileVenueFilters\">\r\n    <!-- This should show/hide with an \"active\" class or something, Nichole magic -->\r\n    ";
  stack2 = helpers['if'].call(depth0, ((stack1 = (depth0 && depth0.venueFilters)),stack1 == null || stack1 === false ? stack1 : stack1.length), {hash:{},inverse:self.noop,fn:self.program(1, program1, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n  </div>\r\n  \r\n  <div class=\"calHeaderWrap\">\r\n    <div class=\"prevArrow\" data-bb=\"previousDayArrow\"></div>\r\n    <div class=\"dayTitle\" data-bb=\"daySelectDropdown\">";
  if (stack2 = helpers.currentDate) { stack2 = stack2.call(depth0, {hash:{},data:data}); }
  else { stack2 = (depth0 && depth0.currentDate); stack2 = typeof stack2 === functionType ? stack2.call(depth0, {hash:{},data:data}) : stack2; }
  buffer += escapeExpression(stack2)
    + "</div>\r\n    <div class=\"nextArrow\" data-bb=\"nextDayArrow\"></div>   \r\n  </div>\r\n\r\n  <div class=\"chooseDateArea\" style=\"display: none;\"></div>\r\n  \r\n</div>\r\n\r\n";
  stack2 = helpers['if'].call(depth0, ((stack1 = (depth0 && depth0.displayModels)),stack1 == null || stack1 === false ? stack1 : stack1.length), {hash:{},inverse:self.program(11, program11, data),fn:self.program(5, program5, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  return buffer;
  });

return this["JST"];

});