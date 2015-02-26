

function InitRelationshipEvents() {
    $(".multArtists").hide();
    $(".multClick").on('click', function () {
        $(this).parent(".addMore").find(".multArtists").slideToggle();
    });

}

function GetJsonFromPropertyManager(context) {
    var mediaParent, guid, thumbnail, thumbNailValue, performanceValue, jsonData, EPiMedia, Properties, Relationships;

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
    Properties.ImageLayout = mediaParent.find(".imgLayout select").val();
    Properties.PressKit = mediaParent.find("div#PressKitContainer input").first().is(':checked');
    Properties.PressPhoto = mediaParent.find("div#PressPhotoContainer input").first().is(':checked');
    Properties.EPiMediaThumbnail = thumbnail;
    ////////////////////


    performanceValue = mediaParent.find(".performances input[type='hidden']").val();

    //CUSTOM RELATIONSHIPS//
    Relationships.Performances = performanceValue;
    ////////////////////////

    return JSON.stringify(EPiMedia);
}