
$(document).ready(function () {
    $("#name").keypress(function (evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode

        if (((charCode <= 93 && charCode >= 65) || (charCode <= 122 && charCode >= 97) || charCode == 8) || charCode == 350 || charCode == 351 || charCode == 304 || charCode == 286 || charCode == 287 || charCode == 231 || charCode == 199 || charCode == 305 || charCode == 214 || charCode == 246 || charCode == 220 || charCode == 252) {
            return true;
        }
        return false;
    });
    $("#soyad").keypress(function (evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode

        if (((charCode <= 93 && charCode >= 65) || (charCode <= 122 && charCode >= 97) || charCode == 8) || charCode == 350 || charCode == 351 || charCode == 304 || charCode == 286 || charCode == 287 || charCode == 231 || charCode == 199 || charCode == 305 || charCode == 214 || charCode == 246 || charCode == 220 || charCode == 252) {
            return true;
        }
        return false;
    });
    $("#telno").keypress(function (evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode

        if ((charCode >= 48 && charCode <= 57) || (charCode >= 96 && charCode <= 105)) {
            return true;
        }
        return false;
    });
    $("#tc").keypress(function (evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode

        if ((charCode >= 48 && charCode <= 57) || (charCode >= 96 && charCode <= 105)) {
            return true;
        }
        return false;
    });
});