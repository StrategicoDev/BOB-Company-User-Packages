(function () {
    "use strict";
    document.addEventListener('deviceready', onDeviceReady.bind(this), false);
    function onDeviceReady() {
        document.getElementById("GetImage").addEventListener("click", GetImage);
    };
})();
function GetImage() {
    navigator.camera.getPicture(function (imageUri) {
        var CapturePhoto = document.getElementById("image");
        alert("Photo Captured");
        CapturePhoto.innerHTML = "<img class='img-circle' width='120' height='120' src='" + imageUri + "' />'";
        uploadPhoto(imageUri);
    }, null, null);

}
function uploadPhoto(imageURI) {
    var options = new FileUploadOptions();
    options.fileKey = "files";
    options.fileName = imageURI.substr(imageURI.lastIndexOf('/') + 1);
    options.mimeType = "image/jpeg";
    console.log(options.fileName);
    var ft = new FileTransfer();
    ft.upload(imageURI, "http://192.168.1.66:8084/api/image", function (result) {
        console.log(JSON.stringify(result));
        alert(JSON.stringify(result));
    }, function (error) {
        console.log(JSON.stringify(error));
        alert(JSON.stringify(result));
    }, options);
}