define(['handlebars'], function(Handlebars) {

this["JST"] = this["JST"] || {};

this["JST"]["calendarPerformanceSelector"] = Handlebars.template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, functionType="function", escapeExpression=this.escapeExpression, helperMissing=helpers.helperMissing, self=this;

function program1(depth0,data) {
  
  var buffer = "", stack1, stack2, options;
  buffer += "\r\n  <div class=\"timeSlot\">\r\n    <div class=\"selectedDateHeader\">";
  options = {hash:{},data:data};
  buffer += escapeExpression(((stack1 = helpers.formattedDate || depth0.formattedDate),stack1 ? stack1.call(depth0, depth0.currentSelectedDate, options) : helperMissing.call(depth0, "formattedDate", depth0.currentSelectedDate, options)))
    + "</div>\r\n		<ul class=\"selectedDateTime\">\r\n			";
  stack2 = helpers.each.call(depth0, depth0.arrayOfTimes, {hash:{},inverse:self.noop,fn:self.program(2, program2, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n		</ul>\r\n	";
  return buffer;
  }
function program2(depth0,data) {
  
  var buffer = "", stack1, options;
  buffer += "\r\n				<li data-bb=\"performanceTimeSlot\" data-id=\"";
  if (stack1 = helpers.PerformanceId) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.PerformanceId; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "\">\r\n					";
  options = {hash:{},data:data};
  buffer += escapeExpression(((stack1 = helpers.formattedTime || depth0.formattedTime),stack1 ? stack1.call(depth0, depth0.PerformanceDate, options) : helperMissing.call(depth0, "formattedTime", depth0.PerformanceDate, options)))
    + "\r\n				</li>\r\n			";
  return buffer;
  }

function program4(depth0,data) {
  
  
  return "\r\n		<div data-bb=\"datepickerMe\"></div>\r\n    ";
  }

  buffer += "<div data-bb=\"performanceCalendarControl\" class=\"miniCal\">\r\n	<div class=\"heading\">\r\n    <span>Select a day/time</span>\r\n    <span class=\"closeControl\">X</span>\r\n	</div>\r\n	";
  stack1 = helpers['if'].call(depth0, depth0.dateSelected, {hash:{},inverse:self.program(4, program4, data),fn:self.program(1, program1, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n  </div>\r\n</div>";
  return buffer;
  });

return this["JST"];

});