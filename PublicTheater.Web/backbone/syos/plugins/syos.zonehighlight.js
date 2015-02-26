/// <reference path="syos.zonehighlight.js" />
define([

    'syos/plugins/templates',
    'syoscore'

], function (Templates) {

    // this plugin overrides SYOSLegend's .template method on the Level Open event

    return SYOSPlugin.extend({
        enabled: true,

        pluginName: "zoneHighlight",

        initPlugin: function () {
            if (this.enabled) {
                syosDispatch.on("levelDidOpen", this.levelDidOpen, this);
                syosDispatch.on("core:didFinishInit", this.bindLegendInformation, this);
            }

        },

        deactivatePlugin: function() {

        },

        bindLegendInformation: function () {
            this.zoneColorConfigs = htmlSyos.rootData.ZoneColors;

            
        },

        resetZoneConfig: function () {
            _.each(this.zoneColorConfigs, function (config) {
                config.Active = false;
            });
        },

        buildCustomLegendCollections: function (activeZoneConfigs) {
            this.baseColorCollection = htmlSyos.display.View.legend.legendItems;

            this.zoneColorCollection = new SYOSLegendItemCollection();

            _.each(activeZoneConfigs, _.bind(function (zoneConfig) {
                if (zoneConfig.ShowInLegend) {
                    this.zoneColorCollection.add({
                        color: zoneConfig.ZoneColor,
                        text: zoneConfig.ZoneLegendText
                    });
                }
            }, this));
        },

        setCustomLegendViewModel: function () {
            this.legend.viewModel = _.bind(function () {
                return {
                    baseColorCollection: this.baseColorCollection.toJSON(),
                    zoneColorCollection: this.zoneColorCollection.toJSON()
                };
            }, this);
        },

        setCustomLegendTemplate: function () {
            this.legend.template = Handlebars.templates.publicSyosLegend;
        },

        levelDidOpen: function (param) {
            this.resetZoneConfig();

            _(function () {
                this.syos.activeLevel.seats.each(function (seat) {
                    var zoneDescription = seat.PriceCollection.first().get('ZoneDescription');

                    if (zoneDescription != null) {
                        var config = this.matchZoneDescriptionToZoneColorConfig(zoneDescription);

                        if (!!config) {
                            seat.mapCircle.set('fillOverride', config.ZoneColor);

                            //Only show in legend if one or more of the seats of that zone are available
                            if (seat.get('IsAvailable') === true)
                                config.Active = true;
                        }
                    }
                }, this);

                this.legend = htmlSyos.display.View.legend;

                var activeZoneColors = _.where(this.zoneColorConfigs, {
                    Active: true
                });

                if (activeZoneColors.length) {
                    this.buildCustomLegendCollections(activeZoneColors);
                    this.setCustomLegendTemplate();
                    this.setCustomLegendViewModel();

                    //Call re-render
                    this.legend.template = Templates.publicSyosLegend;
                    this.legend.render();
                }
            }).chain().bind(this).defer();
        },

        matchZoneDescriptionToZoneColorConfig: function (zoneDescription) {
            var zoneConfig = null;

            if (zoneDescription.indexOf('-') !== -1) {
                var lettersAtEnd = zoneDescription.substring(zoneDescription.indexOf('-') + 1).trim();
                zoneConfig = this.getZoneConfigMatching(lettersAtEnd);

                if (typeof (zoneConfig) === "undefined")
                    zoneConfig = this.getZoneConfigMatching(zoneDescription);
            }
            else {
                zoneConfig = this.getZoneConfigMatching(zoneDescription);
            }

            return zoneConfig;
        },

        getZoneConfigMatching: function (someString) {
            return _.findWhere(this.zoneColorConfigs, { ZoneDescription: someString });
        }
    });

});