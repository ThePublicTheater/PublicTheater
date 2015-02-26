

function InitRelationshipEvents () {
    $(".multArtists").hide();
    $(".multClick").on('click', function () {
        $(this).parent(".addMore").find(".multArtists").slideToggle();
    });

}

function GetJsonFromPropertyManager(context) {
    var mediaParent, guid, thumbnail, thumbNailValue, performanceValue, artistGuids, artists, jsonData, artistValue, EPiMedia, Properties, Relationships;

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
    
    //CUSTOM PROPERTIES//
    Properties.EPiMediaCredit = mediaParent.find(".mediaCredits input[type='text']").val();
    Properties.EPiMediaDate = mediaParent.find(".mediaDate input[type='text']").val();
    Properties.EPiMediaTitle = mediaParent.find(".mediaTitle input[type='text']").val();
    Properties.EPiMediaDescription = mediaParent.find(".mediaDescription textarea").val();
    Properties.EPiMediaThumbnail = thumbnail;
    ////////////////////


    performanceValue = mediaParent.find(".performances input[type='hidden']").val();
    
    var eventValue = mediaParent.find(".events input[type='hidden']").val();
    var eventGuids = new Array();
    if (eventValue != "")
        eventGuids.push(eventValue);
    $.each(mediaParent.find(".events span.selectevents input[type='checkbox']"), function () {
        if (this.checked) {
            eventGuids.push($(this).parent().attr("pageguid"));
        }
    });
    var events = eventGuids.join(",");

    var subscriptionValue = mediaParent.find(".subscriptions input[type='hidden']").val();
    var subscriptionGuids = new Array();
    if (subscriptionValue != "")
        subscriptionGuids.push(subscriptionValue);
    $.each(mediaParent.find(".subscriptions span.selectSubscriptions input[type='checkbox']"), function () {
        if (this.checked) {
            subscriptionGuids.push($(this).parent().attr("pageguid"));
        }
    });
    var subscriptions = subscriptionGuids.join(",");

    artistValue = mediaParent.find(".artists input[type='hidden']").val();
    artistGuids = new Array();
    if(artistValue != "")
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
    Relationships.Events = events;
    Relationships.Subscriptions = subscriptions;
    ////////////////////////
    
    return JSON.stringify(EPiMedia);
}