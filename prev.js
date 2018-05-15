var name;
$(document).ready(function(){
    var video_name = getUrlParameter("video_name");
    $("#content video source").attr("src", video_name);
    console.log(video_name);
    $("#content video")[0].load();
    name = video_name;
});
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
}
function onlike() {
    $.get( "google.com", {name: name} );
    console.log(name);
}

