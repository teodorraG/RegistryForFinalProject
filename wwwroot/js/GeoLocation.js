window.onload = geoFindMe();


function geoFindMe() {

    function success(position) {
        const latitude = position.coords.latitude;
        const longitude = position.coords.longitude;

    }

    function error() {
    }

    if (!navigator.geolocation) {
    } else {
        navigator.geolocation.getCurrentPosition(success, error);
    }

}