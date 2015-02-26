//>>built
require({cache:{"url:epi-cms-addons-blocks/OptimizingBlock/templates/VariationsEditor.html":"<div class=\"dijitInline\" data-dojo-attach-point=\"rootNode\">\r\n    <div data-dojo-attach-point=\"containerNode\" class=\"epi-content-area-itemcontainer\"></div>\r\n    <div data-dojo-attach-point=\"actionsContainer\" class=\"epi-content-area-actionscontainer\"></div>            \r\n</div>\r\n"}});define("epi-cms-addons-blocks/OptimizingBlock/VariationsEditor",["dojo/_base/array","dojo/_base/declare","dojo/_base/lang","dojo/dom-class","dojo/dom-style","dojo/on","dojo/Deferred","dojo/topic","dojo/when","dijit/_FocusMixin","dijit/_WidgetBase","dijit/_TemplatedMixin","dijit/_CssStateMixin","dijit/_WidgetsInTemplateMixin","epi/shell/dnd/Source","epi/shell/widget/ContextMenu","epi/shell/widget/_ValueRequiredMixin","epi/shell/widget/TextWithActionLinks","epi/shell/command/_CommandProviderMixin","epi-cms-addons-blocks/OptimizingBlock/VariantBlockEditor","epi-cms-addons-blocks/OptimizingBlock/viewmodel/VariationsViewModel","epi-cms/contentediting/command/BlockRemove","epi-cms/contentediting/command/BlockEdit","dojo/text!./templates/VariationsEditor.html","epi/i18n!epi/cms/nls/episerver.cms.widget.overlay.blockarea"],function(_1,_2,_3,_4,_5,on,_6,_7,_8,_9,_a,_b,_c,_d,_e,_f,_10,_11,_12,_13,_14,_15,_16,_17,_18){return _2([_a,_9,_b,_d,_c,_12,_10],{baseClass:"epi-content-area-editor",templateString:_17,dndTypes:["epi.cms.content.light"],_source:null,_updateModelDataThrottle:null,supressValueChanged:false,textWithLinks:null,postMixInProperties:function(){this.inherited(arguments);if(!this.commands||this.commands.length<=0){this.commands=[new _16(),new _15()];}this.model=this.model||new _14();this.contextMenu=this.contextMenu||new _f();this.contextMenu.addProvider(this);this.own(this.contextMenu,this.model.watch("selectedItem",_3.hitch(this,function(_19,_1a,_1b){this.updateCommandModel(_1b);})),on(this.model,"childrenChanged",_3.hitch(this,function(_1c){this._trySave(true);})),on(this.model,"childrenOrderChanged",_3.hitch(this,function(_1d){this._trySave(false);})));},buildRendering:function(){this.inherited(arguments);_4.add(this.domNode,"epi-variations-editor");this._source=new _e(this.rootNode,{accept:this.dndTypes,creator:_3.hitch(this,this._createDndSourceItem),isSource:false,singular:true,alwaysCopy:false,parent:this.containerNode});this._source.defaultCheckAcceptance=this._source.checkAcceptance;this._source.checkAcceptance=_3.hitch(this,this._checkAcceptance);var _1e=new _11({contentString:_18.emptyactions.template,namedActions:_18.emptyactions.actions});this.textWithLinks=_1e;this.own(this.textWithLinks,on(_1e,"onActionClick",_3.hitch(this,function(_1f){_7.publish("/epi/layout/pinnable/"+_1f+"/toggle",true);})));_1e.placeAt(this.actionsContainer);},postCreate:function(){this.inherited(arguments);_8(this._tryUpdateChildModelData(),_3.hitch(this,this._setupUI));},onChange:function(_20){},_checkAcceptance:function(_21,_22){return this.readOnly?false:this._source.defaultCheckAcceptance(_21,_22);},_setReadOnlyAttr:function(_23){this._set("readOnly",_23);_5.set(this.actionsContainer,"display",_23?"none":"");if(this.model){this.model.set("readOnly",_23);}},focus:function(){if(this.model.get("value").length>0){this._focusManager.focus(this.treeNode);}else{this.textWithLinks.focus();}},isValid:function(){return (!this.required||this.model.get("value").length>0);},_trySave:function(_24){if(!this._started||this.supressValueChanged){return;}_8(this._save(),_3.hitch(this,function(){if(_24){_8(this._tryUpdateChildModelData(),_3.hitch(this,this._setupUI));return;}this._setupUI();}));},_save:function(){var dfd=new _6();this.onFocus();setTimeout(_3.hitch(this,function(){var _25=this.model.get("value");this._set("value",_25);this.onChange(_25);dfd.resolve();}),1);return dfd;},_setValueAttr:function(_26){this._set("value",_26||[]);this.set("supressValueChanged",true);this.model.set("value",_26);this.set("supressValueChanged",false);if(this._started){_8(this._tryUpdateChildModelData(),_3.hitch(this,this._setupUI));}},_setupUI:function(){this._source.selectAll();this._source.deleteSelectedNodes();this._source.insertNodes(false,this.model.getChildren());},_createBlockEditor:function(_27){var _28=this.model.createModel(_27),_29=new _13({contextMenu:this.contextMenu,model:_28});return _29;},_tryUpdateChildModelData:function(){if(this._updateModelDataThrottle){clearTimeout(this._updateModelDataThrottle);this._updateModelDataThrottle=null;}var dfd=new _6();this._updateModelDataThrottle=setTimeout(_3.hitch(this,function(){if(!this.parent){this.parent=this.getParent();}if(this.parent&&this.parent.contentModel){this.model.setContentModel(this.parent.contentModel);}dfd.resolve();}),200);return dfd;},_setParentAttr:function(_2a){this._set("parent",_2a);if(this._started){_8(this._tryUpdateChildModelData(),_3.hitch(this,this._setupUI));}},_createDndSourceItem:function(_2b,_2c){if(_2c=="avatar"){return this._source.defaultCreator(_2b,_2c);}var _2d;if(typeof _2b.getNormalizedData=="function"){_2d=_2b.getNormalizedData();}_2d=_2d?_2d.data:_2b;var _2e=this._createBlockEditor(_2d);return {"node":_2e.domNode,"data":_2d,"type":this.dndTypes};}});});