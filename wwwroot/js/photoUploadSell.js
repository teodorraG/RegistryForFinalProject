let listOfPhotos = document.getElementsByClassName("img-thumbnail");

$(document).on("click", ".browse", function () {
    var file = $(this).parents().find(".file");
    file.trigger("click");
});
$('input[type="file"]').change(function (e) {
    var fileName = e.target.files[0].name;
    $("#file").val(fileName);

    var reader = new FileReader();
    reader.onload = function (e) {
        // get loaded data and render thumbnail.
        for (var i = 0; i < listOfPhotos.length; i++) {
            if (listOfPhotos[i].src != "https://placehold.it/200x300") {
                continue;
            }
            listOfPhotos[i].src = e.target.result;
            let hiddenField = document.getElementById('Image' + (i + 1));
            console.log(hiddenField);
            hiddenField.value = e.target.result;
            break;
        }
    };
    // read the image file as a data URL.
    reader.readAsDataURL(this.files[0]);
});

document.getElementById("PictureDeleteButton").addEventListener("click", DeletePicture);

function DeletePicture() {
    for (var i = listOfPhotos.length - 1; i >= 0; i--) {
        if (listOfPhotos[i].src != "https://placehold.it/200x300") {
            listOfPhotos[i].src = "https://placehold.it/200x300";
            console.log(i);
            let hiddenField = document.getElementById('Image' + (i + 1));
            console.log(hiddenField);
            hiddenField.value = '';
            break;
        }
        document.getElementById("file").value = "";
    }
}

