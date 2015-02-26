//>>built
require({cache:{"epi-cms-addons-blocks/packaged-layer":function(){define("epi-cms-addons-blocks/packaged-layer",["epi-cms-addons-blocks/BlocksModule","epi-cms-addons-blocks/Bootstrapper","epi-cms-addons-blocks/OptimizingBlock/BlockOverlay","epi-cms-addons-blocks/OptimizingBlock/VariationsEditor","epi-cms-addons-blocks/OptimizingBlock/OptimizingBlockArea"],1);},"epi-cms-addons-blocks/BlocksModule":function(){define("epi-cms-addons-blocks/BlocksModule",["dojo/_base/declare","dojo/_base/lang","epi","epi/_Module","epi/routes"],function(_1,_2,_3,_4,_5){return _1([_4],{initialize:function(){this.inherited(arguments);this._initializeStores();},_initializeStores:function(){var _6=this.resolveDependency("epi.storeregistry"),_7=_6.create("epi.cms.addons.optimizingblock",this._getRestPath("optimizingblock"),{id:"contentLink"});},_getRestPath:function(_8){return _5.getRestPath({moduleArea:"episerver.cms.addons.blocks",storeName:_8});}});});},"epi-cms-addons-blocks/Bootstrapper":function(){define("epi-cms-addons-blocks/Bootstrapper",["dojo/_base/declare","dojo/dom-style","dijit/_WidgetBase"],function(_9,_a,_b){return _9([_b],{postCreate:function(){_a.set(this.domNode,{display:"none"});}});});},"epi-cms-addons-blocks/OptimizingBlock/BlockOverlay":function(){define(["dojo/_base/declare","dojo/dom-style","dijit/_WidgetBase","dijit/_TemplatedMixin","dojo/text!./templates/BlockOverlay.html"],function(_c,_d,_e,_f,_10){return _c([_e,_f],{templateString:_10});});},"url:epi-cms-addons-blocks/OptimizingBlock/templates/BlockOverlay.html":"<div>\r\n</div>","epi-cms-addons-blocks/OptimizingBlock/VariationsEditor":function(){define(["dojo/_base/array","dojo/_base/declare","dojo/_base/lang","dojo/dom-class","dojo/dom-style","dojo/on","dojo/Deferred","dojo/topic","dojo/when","dijit/_FocusMixin","dijit/_WidgetBase","dijit/_TemplatedMixin","dijit/_CssStateMixin","dijit/_WidgetsInTemplateMixin","epi/shell/dnd/Source","epi/shell/widget/ContextMenu","epi/shell/widget/_ValueRequiredMixin","epi/shell/widget/TextWithActionLinks","epi/shell/command/_CommandProviderMixin","epi-cms-addons-blocks/OptimizingBlock/VariantBlockEditor","epi-cms-addons-blocks/OptimizingBlock/viewmodel/VariationsViewModel","epi-cms/contentediting/command/BlockRemove","epi-cms/contentediting/command/BlockEdit","dojo/text!./templates/VariationsEditor.html","epi/i18n!epi/cms/nls/episerver.cms.widget.overlay.blockarea"],function(_11,_12,_13,_14,_15,on,_16,_17,_18,_19,_1a,_1b,_1c,_1d,_1e,_1f,_20,_21,_22,_23,_24,_25,_26,_27,_28){return _12([_1a,_19,_1b,_1d,_1c,_22,_20],{baseClass:"epi-content-area-editor",templateString:_27,dndTypes:["epi.cms.content.light"],_source:null,_updateModelDataThrottle:null,supressValueChanged:false,textWithLinks:null,postMixInProperties:function(){this.inherited(arguments);if(!this.commands||this.commands.length<=0){this.commands=[new _26(),new _25()];}this.model=this.model||new _24();this.contextMenu=this.contextMenu||new _1f();this.contextMenu.addProvider(this);this.own(this.contextMenu,this.model.watch("selectedItem",_13.hitch(this,function(_29,_2a,_2b){this.updateCommandModel(_2b);})),on(this.model,"childrenChanged",_13.hitch(this,function(_2c){this._trySave(true);})),on(this.model,"childrenOrderChanged",_13.hitch(this,function(_2d){this._trySave(false);})));},buildRendering:function(){this.inherited(arguments);_14.add(this.domNode,"epi-variations-editor");this._source=new _1e(this.rootNode,{accept:this.dndTypes,creator:_13.hitch(this,this._createDndSourceItem),isSource:false,singular:true,alwaysCopy:false,parent:this.containerNode});this._source.defaultCheckAcceptance=this._source.checkAcceptance;this._source.checkAcceptance=_13.hitch(this,this._checkAcceptance);var _2e=new _21({contentString:_28.emptyactions.template,namedActions:_28.emptyactions.actions});this.textWithLinks=_2e;this.own(this.textWithLinks,on(_2e,"onActionClick",_13.hitch(this,function(_2f){_17.publish("/epi/layout/pinnable/"+_2f+"/toggle",true);})));_2e.placeAt(this.actionsContainer);},postCreate:function(){this.inherited(arguments);_18(this._tryUpdateChildModelData(),_13.hitch(this,this._setupUI));},onChange:function(_30){},_checkAcceptance:function(_31,_32){return this.readOnly?false:this._source.defaultCheckAcceptance(_31,_32);},_setReadOnlyAttr:function(_33){this._set("readOnly",_33);_15.set(this.actionsContainer,"display",_33?"none":"");if(this.model){this.model.set("readOnly",_33);}},focus:function(){if(this.model.get("value").length>0){this._focusManager.focus(this.treeNode);}else{this.textWithLinks.focus();}},isValid:function(){return (!this.required||this.model.get("value").length>0);},_trySave:function(_34){if(!this._started||this.supressValueChanged){return;}_18(this._save(),_13.hitch(this,function(){if(_34){_18(this._tryUpdateChildModelData(),_13.hitch(this,this._setupUI));return;}this._setupUI();}));},_save:function(){var dfd=new _16();this.onFocus();setTimeout(_13.hitch(this,function(){var _35=this.model.get("value");this._set("value",_35);this.onChange(_35);dfd.resolve();}),1);return dfd;},_setValueAttr:function(_36){this._set("value",_36||[]);this.set("supressValueChanged",true);this.model.set("value",_36);this.set("supressValueChanged",false);if(this._started){_18(this._tryUpdateChildModelData(),_13.hitch(this,this._setupUI));}},_setupUI:function(){this._source.selectAll();this._source.deleteSelectedNodes();this._source.insertNodes(false,this.model.getChildren());},_createBlockEditor:function(_37){var _38=this.model.createModel(_37),_39=new _23({contextMenu:this.contextMenu,model:_38});return _39;},_tryUpdateChildModelData:function(){if(this._updateModelDataThrottle){clearTimeout(this._updateModelDataThrottle);this._updateModelDataThrottle=null;}var dfd=new _16();this._updateModelDataThrottle=setTimeout(_13.hitch(this,function(){if(!this.parent){this.parent=this.getParent();}if(this.parent&&this.parent.contentModel){this.model.setContentModel(this.parent.contentModel);}dfd.resolve();}),200);return dfd;},_setParentAttr:function(_3a){this._set("parent",_3a);if(this._started){_18(this._tryUpdateChildModelData(),_13.hitch(this,this._setupUI));}},_createDndSourceItem:function(_3b,_3c){if(_3c=="avatar"){return this._source.defaultCreator(_3b,_3c);}var _3d;if(typeof _3b.getNormalizedData=="function"){_3d=_3b.getNormalizedData();}_3d=_3d?_3d.data:_3b;var _3e=this._createBlockEditor(_3d);return {"node":_3e.domNode,"data":_3d,"type":this.dndTypes};}});});},"epi-cms-addons-blocks/OptimizingBlock/VariantBlockEditor":function(){define(["dojo/_base/declare","dojo/_base/lang","dojo/dom-class","dojo/when","dijit/_WidgetsInTemplateMixin","epi/dependency","epi-cms/contentediting/editors/ContentBlockEditor","epi-cms/widget/Breadcrumb","epi-cms-addons-blocks/OptimizingBlock/StatisticInfo","epi-cms-addons-blocks/OptimizingBlock/viewmodel/StatisticInfoViewModel","dojo/text!./templates/VariantBlockEditor.html"],function(_3f,_40,_41,_42,_43,_44,_45,_46,_47,_48,_49){return _3f([_45,_43],{templateString:_49,typeDescriptors:null,iconClass:"epi-iconObjectOptimizingBlock",postMixInProperties:function(){this.modelBindingMap=_40.mixin(this.modelBindingMap,{"typeDescriptor":["typeDescriptor"]});this.typeDescriptors=this.typeDescriptors||_44.resolve("epi.cms.Application").getTypeUIDescriptors();this.inherited(arguments);},postCreate:function(){this.inherited(arguments);if(!this.breadcrumbNode){this.breadcrumbNode=new _46();}this.breadcrumbNode.set("contentLink",this.contentLink);if(!this.statisticInfo){this.statisticInfo=new _47();this.statisticInfo.placeAt(this.statisticNode);}this.statisticInfo.set("model",new _48(this.model.statisticInfo));this.own(this.model.watch("statisticInfo",_40.hitch(this,function(){this.statisticInfo.set("model",new _48(this.model.statisticInfo));})));},_setTypeDescriptorAttr:function(){if(this.typeDescriptors){var _4a=this.typeDescriptors[this.model.typeDescriptor];if(_4a){this.iconClass=_4a.iconClass;}}_41.add(this.iconNode,this.iconClass);},_onClick:function(evt){this.inherited(arguments);this.model.set("selected",true);}});});},"epi-cms-addons-blocks/OptimizingBlock/StatisticInfo":function(){define(["dojo/_base/declare","dojo/_base/lang","dojo/dom-construct","dijit/_Widget","dijit/_TemplatedMixin","epi/datetime","epi/shell/widget/_ModelBindingMixin","dojo/text!./templates/StatisticInfo.html","epi/i18n!epi/cms/nls/episerver.cms.addons.optimizingblock.statistic"],function(_4b,_4c,_4d,_4e,_4f,_50,_51,_52,res){return _4b([_4e,_4f,_51],{templateString:_52,statisticItemTemplate:"<li>                <div class=\"content\">                    <div class=\"header\">{title}</div>                    <div class=\"description\">{description}</div>                </div>            </li>",model:null,_setModelAttr:function(_53){_4d.empty(this.containerNode);this.inherited(arguments);},modelBindingMap:{"boostValue":["boostValue"],"probability":["probability"],"viewsCount":["viewsCount"],"goalCount":["goalCount"]},_setBoostValueAttr:function(_54){if(_54===null){return;}var _55=res.boostvalue.title.betters.replace("{0}",_54);if(_54===0){_55=res.boostvalue.title.least;}else{if(_54===1){_55=res.boostvalue.title.marginally;}}var _56=_4c.replace(this.statisticItemTemplate,{title:_55,description:res.boostvalue.description});_4d.place(_56,this.containerNode);},_setProbabilityAttr:function(_57){if(_57===null){return;}var _58=_57,_59=res.probability.title.replace("{0}",_58).replace("{1}","100"),_5a=_4c.replace(this.statisticItemTemplate,{title:_59,description:res.probability.description});_4d.place(_5a,this.containerNode);},_setViewsCountAttr:function(_5b){if(_5b===null){return;}var _5c;if(!_5b){_5c=_4c.replace(this.statisticItemTemplate,{title:res.never.exposure,description:""});}else{var des=res.exposure.replace("{0}",_50.toUserFriendlyString(this.model.exposureDate));_5c=_4c.replace(this.statisticItemTemplate,{title:_5b,description:des});}_4d.place(_5c,this.containerNode);},_setGoalCountAttr:function(_5d){if(_5d===null){return;}var _5e;if(!_5d){_5e=_4c.replace(this.statisticItemTemplate,{title:res.never.conversion,description:""});}else{_5e=_4c.replace(this.statisticItemTemplate,{title:_5d,description:res.conversion});}_4d.place(_5e,this.containerNode);}});});},"url:epi-cms-addons-blocks/OptimizingBlock/templates/StatisticInfo.html":"<div class=\"epi-optimizing-statistic\">\r\n    <ul data-dojo-attach-point=\"containerNode\"></ul>\r\n</div>\r\n","epi-cms-addons-blocks/OptimizingBlock/viewmodel/StatisticInfoViewModel":function(){define(["dojo/_base/declare","dojo/Stateful"],function(_5f,_60){return _5f([_60],{boostValue:null,probability:null,viewsCount:null,goalCount:null,exposureDate:null});});},"url:epi-cms-addons-blocks/OptimizingBlock/templates/VariantBlockEditor.html":"<div class=\"epi-content-block-editor\">\r\n    <div class=\"epi-optimizing-block\">\r\n        <a href=\"#\" data-dojo-attach-point=\"itemNode\" data-dojo-attach-event=\"onclick:_onClick\">\r\n            <span data-dojo-attach-point=\"contentNode\" class=\"epi-content-block-node\" style=\"height: auto;\">\r\n                <span class=\"epi-breadCrumb\" data-dojo-attach-point=\"breadcrumbNode\" data-dojo-type=\"epi-cms/widget/Breadcrumb\"></span>\r\n                <span data-dojo-attach-point=\"iconNode\" role=\"presentation\"></span>\r\n                <span data-dojo-attach-point=\"labelNode\" class=\"epi-block-title\"></span>\r\n                <span data-dojo-attach-point=\"extraIconsContainer\" class=\"epi-floatRight epi-extraIconsContainer\" role=\"presentation\">\r\n                    <span data-dojo-attach-point=\"iconNodeMenu\" class=\"epi-iconContextMenu epi-extraIcon\" style=\"visibility: hidden;\" data-dojo-attach-event=\"onclick:_onContextMenu\"></span>\r\n                </span>\r\n            </span>\r\n        </a>\r\n    </div>\r\n    <div data-dojo-attach-point=\"statisticNode\"></div>\r\n</div>\r\n","epi-cms-addons-blocks/OptimizingBlock/viewmodel/VariationsViewModel":function(){define(["dojo/_base/array","dojo/_base/declare","dojo/_base/lang","dojo/Deferred","dojo/promise/all","dojo/when","dojo/has","epi/dependency","epi-cms/contentediting/viewmodel/ContentBlockViewModel","epi-cms/contentediting/viewmodel/PersonalizedGroupViewModel","epi-cms/contentediting/viewmodel/ContentAreaViewModel"],function(_61,_62,_63,_64,all,_65,has,_66,_67,_68,_69){return _62([_69],{dynamicDataStore:null,orderBy:{column:"boostValue",order:"desc"},constructor:function(){this.inherited(arguments);this.dynamicDataStore=this.dynamicDataStore||_66.resolve("epi.storeregistry").get("epi.cms.addons.optimizingblock");},createModel:function(_6a){var _6b=_6a;if(!(_6a instanceof _67)&&!(_6a instanceof _68)){_6b=new _67(_6a);this.addChild(_6b);}return _6b;},setContentModel:function(_6c){var dfd=new _64();this.set("contentModel",_6c);_65(this._updateStatisticInfoForModel(),_63.hitch(this,function(){this._sort();dfd.resolve();}));return dfd;},_sort:function(){if(!this.orderBy||!this.orderBy.column){return;}if(!this.orderBy.order){this.orderBy.order="desc";}var _6d=this.orderBy.column,_6e=this.orderBy.order.toString().toLowerCase()==="desc",_6f=this,map=[],_70=[],_71=false;function _72(a,b){if(has("ie")&&(a.value==b.value)){return 0;}return a.value>b.value?1:-1;};function _73(a,b){return _72(a,b);};function _74(a,b){return _72(b,a);};map=_61.map(this._data,function(_75,_76){return {value:_75.statisticInfo?parseFloat(_75.statisticInfo.boostValue):-1,index:_76};});if(_6e){map.sort(_74);}else{map.sort(_73);}_70=_61.map(map,function(_77){return _6f._data[_77.index];});_61.some(map,function(_78,_79){_71=_78.index!=_79;return _71;});if(_71){this._data=_70;this._emitChildrenOrderingChanged();}},_emitChildrenOrderingChanged:function(_7a){this.emit("childrenOrderChanged",_7a?_7a:this);},_updateStatisticInfoForModel:function(){var _7b=this;function _7c(_7d){var _7e=_7b.getChildren();if(_7e&&_7e.length>0){for(var i=0;i<_7e.length;i++){_7e[i].set("statisticInfo",_7d[i]);}}};return this._updateStatisticInfo(null,_7c);},_updateStatisticInfo:function(_7f,_80){var dfd=new _64();var _81={contentLink:this.contentModel.get("icontent_contentlink"),variantReferences:_61.map(this.getChildren(),function(_82){return _82.contentLink;}),variantContentLink:_7f?_7f.contentLink:null};_65(this.dynamicDataStore.query(_81),function(_83){_80.call(this,_83);dfd.resolve();});return dfd;}});});},"url:epi-cms-addons-blocks/OptimizingBlock/templates/VariationsEditor.html":"<div class=\"dijitInline\" data-dojo-attach-point=\"rootNode\">\r\n    <div data-dojo-attach-point=\"containerNode\" class=\"epi-content-area-itemcontainer\"></div>\r\n    <div data-dojo-attach-point=\"actionsContainer\" class=\"epi-content-area-actionscontainer\"></div>            \r\n</div>\r\n","epi-cms-addons-blocks/OptimizingBlock/OptimizingBlockArea":function(){define(["dojo/_base/array","dojo/_base/declare","dojo/_base/lang","dojo/dom-class","dojo/when","epi/shell/dnd/Target","epi-cms/widget/overlay/BlockArea","epi-cms/contentediting/viewmodel/ContentBlockViewModel","epi-cms-addons-blocks/OptimizingBlock/OptimizingBlockItem","epi-cms-addons-blocks/OptimizingBlock/viewmodel/VariationsViewModel"],function(_84,_85,_86,_87,_88,_89,_8a,_8b,_8c,_8d){return _85([_8a],{blockClass:_8c,modelClass:_8d,dndSourceClass:_85([_89],{setHorizontal:function(){}}),dndSourceSettings:{isSource:false},postMixInProperties:function(){this.inherited(arguments);this.model.setContentModel(this.contentModel);},postCreate:function(){this.inherited(arguments);_87.add(this.domNode,"epi-overlay-optimizingBlockArea");this.watch("active",_86.hitch(this,function(_8e,_8f,_90){if(_90===false){this.model.setContentModel(this.contentModel);}}));},refresh:function(){this.inherited(arguments);this.model.setContentModel(this.contentModel);},_onDrop:function(_91,_92,_93,_94){var _95=this.model,_96=this._source;if(_96.anchor===_96.targetAnchor){return;}_91=_86.isArray(_91)?_91:[_91];_95.modify(function(){_84.forEach(_91,function(_97){var _98=new _8b(_97.data);_95.addChild(_98);});});}});});},"epi-cms-addons-blocks/OptimizingBlock/OptimizingBlockItem":function(){define(["dojo/_base/declare","dojo/_base/lang","dojo/dom-geometry","dojo/dom-style","epi/shell/command/_CommandProviderMixin","epi-cms-addons-blocks/OptimizingBlock/StatisticInfo","epi-cms-addons-blocks/OptimizingBlock/viewmodel/StatisticInfoViewModel","epi-cms/contentediting/command/BlockRemove","epi-cms/contentediting/command/BlockEdit","epi-cms/widget/overlay/Block","dojo/text!./templates/OptimizingBlockItem.html"],function(_99,_9a,_9b,_9c,_9d,_9e,_9f,_a0,_a1,_a2,_a3){return _99([_a2,_9d],{templateString:_a3,postMixInProperties:function(){this.inherited(arguments);if(!this.commands||this.commands.length<=0){this.commands=[new _a1(),new _a0()];}this.commandProvider=this;this.updateCommandModel(this.viewModel);},postCreate:function(){this.inherited(arguments);this.own(this.viewModel.watch("statisticInfo",_9a.hitch(this,this._setupUI)));},updatePosition:function(_a4){this.inherited(arguments);return this._updateHeight();},_setupUI:function(){this._buildStatisticSegment();this._buildHeadingSegment();this._updateHeight();},_buildHeadingSegment:function(){this.labelNode.innerHTML=this.sourceItemNode.innerHTML;},_buildStatisticSegment:function(){if(!this.statisticInfo){this.statisticInfo=new _9e();this.statisticInfo.placeAt(this.statisticNode);}if(this.viewModel.statisticInfo){this.statisticInfo.set("model",new _9f(this.viewModel.statisticInfo));}},_updateHeight:function(){var _a5=_9b.getMarginBox(this.sourceItemNode).h,_a6=_9b.getMarginBox(this.optimizingBlockItemNode).h,_a7={"visibility":"hidden"},_a8=_a5!=_a6;if(_a8){_a7.height=_a6+"px";}_9c.set(this.sourceItemNode,_a7);return _a8;},_onContextMenuClick:function(){this.updateCommandModel(this.viewModel);this.viewModel.set("selected",true);return false;}});});},"url:epi-cms-addons-blocks/OptimizingBlock/templates/OptimizingBlockItem.html":"<div class=\"dojoDndItem epi-overlay-block\">\r\n    <div data-dojo-attach-point=\"containerDomNode\">\r\n        <div class=\"epi-content-block-editor\" data-dojo-attach-point=\"optimizingBlockItemNode\">\r\n            <div class=\"epi-optimizing-block\">\r\n                <span class=\"epi-content-block-node\" style=\"height: auto;\">\r\n                    <span data-dojo-attach-point=\"labelNode\" class=\"epi-block-title\"></span>\r\n                </span>\r\n            </div>            \r\n            <div data-dojo-attach-point=\"statisticNode\"></div>\r\n        </div>\r\n    </div>\r\n    <span class=\"epi-overlayControls epi-mediumButton epi-personalized\" data-dojo-attach-point=\"personalizedIcon\">\r\n        <span class=\"dijitReset dijitInline dijitIcon epi-iconUsers\"></span>\r\n    </span>\r\n    <button class=\"epi-chromelessButton epi-overlayControls epi-settingsButton epi-mediumButton\" data-dojo-attach-point=\"settingsButton\" data-dojo-type=\"dijit/form/DropDownButton\" data-dojo-props=\"showLabel:false, title:'${res.settingstooltip}', iconClass:'epi-iconContextMenu'\">\r\n        <span data-dojo-attach-point=\"contextMenu\" data-dojo-type=\"epi/shell/widget/ContextMenu\"></span>\r\n    </button>\r\n</div>\r\n"}});