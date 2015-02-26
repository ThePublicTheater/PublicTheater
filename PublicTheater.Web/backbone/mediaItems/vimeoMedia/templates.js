define(['handlebars'], function(Handlebars) {

this["JST"] = this["JST"] || {};

this["JST"]["gridTemplate"] = Handlebars.template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, functionType="function", escapeExpression=this.escapeExpression, self=this;

function program1(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n		<div class=\"topSection\">\r\n			";
  stack1 = helpers['if'].call(depth0, depth0.hasImage, {hash:{},inverse:self.noop,fn:self.program(2, program2, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n		</div>\r\n	";
  return buffer;
  }
function program2(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n				<img src=\"";
  if (stack1 = helpers.imageURL) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.imageURL; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "\" alt=\"";
  if (stack1 = helpers.title) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.title; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "\" class=\"mediaItemImage\"></img>\r\n			";
  return buffer;
  }

  buffer += "<div data-id=\"";
  if (stack1 = helpers.mediaId) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.mediaId; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "\" class=\"mediaItem ";
  if (stack1 = helpers.mediaType) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.mediaType; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "\">\r\n	";
  stack1 = helpers['if'].call(depth0, depth0.hasTopSection, {hash:{},inverse:self.noop,fn:self.program(1, program1, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n	<div class=\"bottomSection\">\r\n		<span class=\"mediaTitle\">";
  if (stack1 = helpers.title) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.title; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "</span>\r\n		<span class=\"mediaSubTitle\">";
  if (stack1 = helpers.subtitle) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.subtitle; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "</span>\r\n	</div>\r\n</div>";
  return buffer;
  });

this["JST"]["playerTemplate"] = Handlebars.template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, functionType="function", escapeExpression=this.escapeExpression;


  buffer += "<div class=\"playerWrap\">\r\n<div class=\"mediaPlayer large-12 medium-12 small-12\">\r\n	<div id=\"leftPanel\" class=\"large-8 medium-8 small-12\">\r\n		\r\n      <iframe id=\"ytPlayer\" class=\"vimeo\" src=\"";
  if (stack1 = helpers.mediaURL) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.mediaURL; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "\" frameborder=\"0\" webkitallowfullscreen=\"\" mozallowfullscreen=\"\" allowfullscreen=\"\"></iframe>\r\n		\r\n	</div>\r\n	<div id=\"rightPanel\" class=\"large-4 medium-4 small-12\">\r\n		<div class=\"rightPanelTopBar\">\r\n			<span class=\"closePlayerButton\">X</span>\r\n		</div>\r\n		<div class=\"mediaItemInformation\">\r\n			<span class=\"title\">";
  if (stack1 = helpers.title) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.title; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "</span>\r\n            <span class=\"subtitle\">";
  if (stack1 = helpers.subtitle) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.subtitle; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "</span>\r\n            <p>";
  if (stack1 = helpers.description) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.description; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "</p>\r\n		</div>\r\n	</div>\r\n</div>\r\n</div>";
  return buffer;
  });

return this["JST"];

});