/// <reference path="syos.zonehighlight.js" />
define([

], function () {

    // this plugin adds to the SYOSLegend's

    return SYOSPlugin.extend({
        enabled: true,

        pluginName: "zoneHighlight",

        initPlugin: function () {
            if (this.enabled) {
                syosDispatch.on("levelDidOpen", this.levelDidOpen, this);
                syosDispatch.on("core:didFinishInit", this.bindLegendInformation, this);
            }

        },

        deactivatePlugin: function () {

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
            // this.baseColorCollection = htmlSyos.display.View.legend.legendItems;

            this.zoneColorCollection = new SYOSLegendItemCollection();

            
            _.each(activeZoneConfigs, _.bind(function (zoneConfig) {
                if (zoneConfig.ShowInLegend) {
                    var exist = this.syos.display.view.legend.legendItems.findWhere({ text: zoneConfig.ZoneLegendText, color: zoneConfig.ZoneColor });
                    if (typeof (exist) === 'undefined') {
                        this.syos.display.view.legend.addLegendItemWithColorAndText(zoneConfig.ZoneColor, zoneConfig.ZoneLegendText, "syos-highlighted-zone");
                    }
                }
            }, this));
            if ($(".syos-legend-end").length == 0) {
                $(".syos-key>ul>li:nth-child(2),.syos-key>ul>li:nth-child(3)").hide();
                $(".syos-key").append('<ul class="syos-legend-end"><li class=""><span style="background-color:#000000; "></span><strong>Selected</strong></li><li class=""><span style="background-color:#cdcccc; "></span><strong>Unavailable</strong></li></ul>');

            }
        },
        adaSeatID: [8, 13],

        levelDidOpen: function (param) {
            this.resetZoneConfig();

            _(function () {
                this.syos.activeLevel.seats.each(function (seat) {
                    var zoneDescription = seat.PriceCollection.first().get('ZoneDescription');

                    if (zoneDescription != null) {
                        var config = this.matchZoneDescriptionToZoneColorConfig(zoneDescription);

                        if (!!config) {

                            //leave it alone if it's ADA seats
                            if (this.adaSeatID.indexOf(seat.SeatTypeInfo.ID) === -1) {
                                seat.mapCircle.set('fillOverride', config.ZoneColor);
                            }
                            //Only show in legend if one or more of the seats of that zone are available
                            if (seat.get('IsAvailable') === true)
                                config.Active = true;
                        }
                    }
                }, this);


                var activeZoneColors = _.where(this.zoneColorConfigs, {
                    Active: true
                });

                if (activeZoneColors.length) {
                    this.buildCustomLegendCollections(activeZoneColors);
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