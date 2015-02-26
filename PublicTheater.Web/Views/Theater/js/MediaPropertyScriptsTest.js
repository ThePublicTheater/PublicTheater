

function InitRelationshipEvents() {
    $(".multArtists").hide();
    $(".multClick").on('click', function () {
        $(this).parent(".addMore").find(".multArtists").slideToggle();
    });

}

function GetJsonFromPropertyManager(context) {
    var mediaParent, guid, thumbnail, thumbNailValue, performanceValue, artistGuids, artists, jsonData, artistValue, EPiMedia, Properties, Relationships;
    var PropertyCollection;
    mediaParent = context;

    EPiMedia = new Object();
    Properties = new Object();
    Relationships = new Object();
    jsonData = new Object();

    guid = mediaParent.find(".mediaImage input[type='hidden']").val();
    EPiMedia.guid = guid;

    EPiMedia.jsonData = jsonData;
    jsonData.Properties = Properties;
    jsonData.Relationships = Relationships;
    
    thumbnail = "";

    thumbNailValue = mediaParent.find(".mediaThumbnail input[type='text']").val();
    if (thumbNailValue != null) thumbnail = thumbNailValue;

    ////

    PropertyCollection = mediaParent.find("[DynamicCtrl='MediaProperty']");

    //CUSTOM PROPERTIES//
    Properties.EPiMediaCredit = mediaParent.find(".mediaCredits input[type='text']").val();
    Properties.EPiMediaDate = mediaParent.find(".mediaDate input[type='text']").val();
    Properties.EPiMediaTitle = mediaParent.find(".mediaTitle input[type='text']").val();
    Properties.EPiMediaDescription = mediaParent.find(".mediaDescription textarea").val();
    Properties.EPiMediaThumbnail = thumbnail;
    ////////////////////


    performanceValue = mediaParent.find(".performances input[type='hidden']").val();
    artistValue = mediaParent.find(".artists input[type='hidden']").val();
    artistGuids = new Array();
    if (artistValue != "")
        artistGuids.push(artistValue);
    $.each(mediaParent.find(".artists span.selectArtist input[type='checkbox']"), function () {
        if (this.checked) {
            artistGuids.push($(this).parent().attr("pageguid"));
        }
    });
    artists = artistGuids.join(",");

    //CUSTOM RELATIONSHIPS//
    Relationships.Artist = artists;
    Relationships.Performances = performanceValue;
    ////////////////////////

    return JSON.stringify(EPiMedia);
}