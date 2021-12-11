function ShowImagePreview(ImageUpload, previewImage) {
    if (ImageUpload.files && ImageUpload.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(ImageUpload.files[0]);
    }
}