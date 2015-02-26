//>>built
require({cache:{"url:epi-cms-addons-blocks/OptimizingBlock/templates/StatisticInfo.html":"<div class=\"epi-optimizing-statistic\">\r\n    <ul data-dojo-attach-point=\"containerNode\"></ul>\r\n</div>\r\n"}});define("epi-cms-addons-blocks/OptimizingBlock/StatisticInfo",["dojo/_base/declare","dojo/_base/lang","dojo/dom-construct","dijit/_Widget","dijit/_TemplatedMixin","epi/datetime","epi/shell/widget/_ModelBindingMixin","dojo/text!./templates/StatisticInfo.html","epi/i18n!epi/cms/nls/episerver.cms.addons.optimizingblock.statistic"],function(_1,_2,_3,_4,_5,_6,_7,_8,_9){return _1([_4,_5,_7],{templateString:_8,statisticItemTemplate:"<li>                <div class=\"content\">                    <div class=\"header\">{title}</div>                    <div class=\"description\">{description}</div>                </div>            </li>",model:null,_setModelAttr:function(_a){_3.empty(this.containerNode);this.inherited(arguments);},modelBindingMap:{"boostValue":["boostValue"],"probability":["probability"],"viewsCount":["viewsCount"],"goalCount":["goalCount"]},_setBoostValueAttr:function(_b){if(_b===null){return;}var _c=_9.boostvalue.title.betters.replace("{0}",_b);if(_b===0){_c=_9.boostvalue.title.least;}else{if(_b===1){_c=_9.boostvalue.title.marginally;}}var _d=_2.replace(this.statisticItemTemplate,{title:_c,description:_9.boostvalue.description});_3.place(_d,this.containerNode);},_setProbabilityAttr:function(_e){if(_e===null){return;}var _f=_e,_10=_9.probability.title.replace("{0}",_f).replace("{1}","100"),_11=_2.replace(this.statisticItemTemplate,{title:_10,description:_9.probability.description});_3.place(_11,this.containerNode);},_setViewsCountAttr:function(_12){if(_12===null){return;}var _13;if(!_12){_13=_2.replace(this.statisticItemTemplate,{title:_9.never.exposure,description:""});}else{var des=_9.exposure.replace("{0}",_6.toUserFriendlyString(this.model.exposureDate));_13=_2.replace(this.statisticItemTemplate,{title:_12,description:des});}_3.place(_13,this.containerNode);},_setGoalCountAttr:function(_14){if(_14===null){return;}var _15;if(!_14){_15=_2.replace(this.statisticItemTemplate,{title:_9.never.conversion,description:""});}else{_15=_2.replace(this.statisticItemTemplate,{title:_14,description:_9.conversion});}_3.place(_15,this.containerNode);}});});