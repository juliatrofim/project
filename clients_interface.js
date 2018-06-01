var name;
var count;
var text1 = null;
var text2 = null;

$(document).ready(function(){
    $.getJSON('https://mediacontentaf.azurewebsites.net/api/GetSequence?UserID=1', function(data) {
        name = data.Sas;
        text1 = data.Message;
        var n = text1.length;
        var i = 0;
        while (i < n && text1[i] != '#') {
            ++i;
        }
        count = 1;
        text1 = text1.substring(0, i);
        if (i < n) {
            ++count;
            text2 = data.Message;
            text2 = text2.substring(i + 1, n);
        }
        document.getElementById("likebutton").childNodes[0].nodeValue = text1;
        document.getElementById("dislikebutton").childNodes[0].nodeValue = text2;
        if (data.Button === "1") {
            document.getElementById("like").id = "like2";
        }
        if (data.Button === "1") {
            document.getElementById("dislike").id = "dislike2";
        }
        if (data.Button === "2") {
            document.getElementById("like").id = "like3";
        }
        if (data.Button === "2") {
            document.getElementById("dislike").id = "dislike3";
        }
        if (data.Button === "3") {
            document.getElementById("like").id = "like1";
        }
        if (data.Button === "3") {
            document.getElementById("dislike").id = "dislike1";
        }
    });
    $("#content video source").attr("src", name);
    $("#content video")[0].load();
});

/*
function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
                                sURLVariables = sPageURL.split('&'),
                                sParameterName,
                                i;
    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
}*/
function onlike() {
//    $.get( "google.com", {name: name} );
//    console.log(name);
}



